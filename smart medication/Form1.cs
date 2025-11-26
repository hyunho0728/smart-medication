using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace smart_medication
{
    public partial class Form1 : Form
    {
        string connectDB = "Server=localhost;Port=3306;Uid=root;Pwd=1234;Charset=utf8";
        private string currentUserName;

        public Form1(string userName)
        {
            // 디자인 파일(MainForm.Designer.cs)에 정의된 UI 생성 함수 호출
            InitializeComponent();

            btnMedicineRegi.Text = "약물 관리";

            this.currentUserName = userName;
            lblWelcom.Text = $"안녕하세요 {userName}님!";

            if (!dgvMedicineList.Columns.Contains("colDosage"))
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "colDosage";
                col.HeaderText = "1회 복용량";
                dgvMedicineList.Columns.Add(col);
            }

            updateCurrentTime();
            LoadData();
            updateTodayMedicineCount();

            dgvMedicineList.ReadOnly = false;

            dgvMedicineList.Columns[0].ReadOnly = true; // 시간
            dgvMedicineList.Columns[1].ReadOnly = true; // 약품
            dgvMedicineList.Columns[2].ReadOnly = false; // 체크박스
            dgvMedicineList.Columns[3].ReadOnly = false; // 비고
            dgvMedicineList.Columns[4].ReadOnly = true; // 남은 약

            if (dgvMedicineList.Columns["colDosage"] != null)
            {
                dgvMedicineList.Columns["colDosage"].ReadOnly = true;
                dgvMedicineList.Columns["colDosage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvMedicineList.Columns["colDosage"].DisplayIndex = 2;
            }

            dgvMedicineList.CurrentCellDirtyStateChanged += dgvMedicineList_CurrentCellDirtyStateChanged;
            dgvMedicineList.CellValueChanged += dgvMedicineList_CellValueChanged;

            // [추가] 셀 스타일링(취소선)을 위한 이벤트 연결
            dgvMedicineList.CellFormatting += dgvMedicineList_CellFormatting;

            dgvMedicineList.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvMedicineList.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMedicineList.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvMedicineList.ColumnHeadersDefaultCellStyle.BackColor;

            SetButtonToolTips();
        }

        private void SetButtonToolTips()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(btnTakeMorning, "아침 시간대: 00:00 ~ 11:00 사이의 약을 일괄 처리합니다.");
            toolTip.SetToolTip(btnTakeLunch, "점심 시간대: 11:00 ~ 16:00 사이의 약을 일괄 처리합니다.");
            toolTip.SetToolTip(btnTakeDinner, "저녁 시간대: 16:00 ~ 24:00 사이의 약을 일괄 처리합니다.");
        }

        private void updateCurrentTime()
        {
            lblTimeInfo.Text = $"현재 시간 : {DateTime.Now.ToString("HH:mm:ss")}";
        }

        private void updateTodayMedicineCount()
        {
            int totalCount = dgvMedicineList.Rows.Count;
            int notTakenCount = 0;

            foreach (DataGridViewRow row in dgvMedicineList.Rows)
            {
                object value = row.Cells["colCheck"].Value;
                if (Convert.ToBoolean(value) == false)
                {
                    notTakenCount++;
                }
            }

            lblTodayMedicineCount.Text = $"오늘 복용할 약 {notTakenCount}개";
            lblMedicationSummary.Text = $"오늘 복용해야하는 약이 {notTakenCount}가지 있어요";
        }

        private void LoadData()
        {
            dgvMedicineList.Rows.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            S.take_time, 
                            M.med_name, 
                            M.stock_quantity, 
                            S.daily_dosage,
                            S.dosage_per_take
                        FROM Schedules S
                        JOIN Medications M ON S.med_id = M.med_id
                        JOIN Users U ON S.user_id = U.user_id
                        WHERE U.user_name = @userName 
                        ORDER BY S.take_time ASC"; // 초기 정렬은 시간순

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", this.currentUserName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string timeStr = reader["take_time"].ToString();
                            if (timeStr.Length > 5) timeStr = timeStr.Substring(0, 5);
                            string medName = reader["med_name"].ToString();

                            int stock = Convert.ToInt32(reader["stock_quantity"]);
                            int dailyDosage = Convert.ToInt32(reader["daily_dosage"]);
                            int dosagePerTake = Convert.ToInt32(reader["dosage_per_take"]);

                            double days = (dailyDosage > 0) ? ((double)stock / dailyDosage) : 0;

                            string remainText = $"{stock}정 ({days:0.#}일분)";
                            string dosageText = $"{dosagePerTake}정";

                            int rowIndex = dgvMedicineList.Rows.Add(timeStr, medName, false, "", remainText, dosageText);
                            dgvMedicineList.Rows[rowIndex].Tag = new int[] { dailyDosage, dosagePerTake };
                        }
                    }

                    // [추가] 데이터 로드 후 정렬 적용 (체크된 건 아래로)
                    SortMedicineList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터 로드 실패: " + ex.Message);
                }
            }
        }

        // [추가] 리스트 정렬 함수: 1순위 복용여부(False가 위), 2순위 시간(오름차순)
        private void SortMedicineList()
        {
            dgvMedicineList.Sort(new RowComparer());
        }

        // [추가] 커스텀 정렬 클래스
        private class RowComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = (DataGridViewRow)x;
                DataGridViewRow row2 = (DataGridViewRow)y;

                bool check1 = Convert.ToBoolean(row1.Cells["colCheck"].Value);
                bool check2 = Convert.ToBoolean(row2.Cells["colCheck"].Value);

                // 1. 체크 여부 비교 (체크 안 된 것이 위로)
                // false(0) < true(1) 이므로, 오름차순 정렬하면 false가 먼저 옴
                int result = check1.CompareTo(check2);

                // 2. 체크 여부가 같다면 시간순 정렬
                if (result == 0)
                {
                    string time1 = row1.Cells[0].Value.ToString();
                    string time2 = row2.Cells[0].Value.ToString();
                    result = time1.CompareTo(time2);
                }

                return result;
            }
        }

        // [추가] 셀 스타일링 (취소선 및 회색 처리)
        private void dgvMedicineList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMedicineList.Rows[e.RowIndex];
                bool isChecked = Convert.ToBoolean(row.Cells["colCheck"].Value);

                if (isChecked)
                {
                    // 복용 완료: 취소선 + 회색 텍스트
                    e.CellStyle.Font = new Font(dgvMedicineList.Font, FontStyle.Strikeout);
                    e.CellStyle.ForeColor = Color.Gray;
                    e.CellStyle.SelectionForeColor = Color.Gray;
                }
                else
                {
                    // 미복용: 기본 폰트 + 검정 텍스트
                    e.CellStyle.Font = new Font(dgvMedicineList.Font, FontStyle.Regular); // 또는 Bold
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.SelectionForeColor = Color.Black;
                }
            }
        }

        private void dgvMedicineList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMedicineList.IsCurrentCellDirty)
            {
                dgvMedicineList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMedicineList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                DataGridViewRow row = dgvMedicineList.Rows[e.RowIndex];

                bool isChecked = Convert.ToBoolean(row.Cells[2].Value);
                string medName = row.Cells[1].Value.ToString();

                string currentText = row.Cells[4].Value.ToString();
                int currentStock = 0;
                try
                {
                    currentStock = int.Parse(currentText.Split('정')[0]);
                }
                catch { return; }

                int[] dosageInfo = (int[])row.Tag;
                int dailyDosage = dosageInfo[0];
                int dosagePerTake = dosageInfo[1];

                int newStock = isChecked ? currentStock - dosagePerTake : currentStock + dosagePerTake;
                if (newStock < 0) newStock = 0;

                UpdateStockToDB(medName, newStock);
                SyncAllRowsStock(medName, newStock);
                updateTodayMedicineCount();

                // [추가] 값 변경 후 즉시 재정렬 (체크하면 아래로 이동)
                // Invoke를 사용하여 UI 갱신 충돌 방지
                this.BeginInvoke(new Action(() => {
                    SortMedicineList();
                    dgvMedicineList.Refresh(); // 스타일 적용 갱신
                }));
            }
        }

        private void SyncAllRowsStock(string targetMedName, int newStock)
        {
            // 재정렬 시 이벤트가 발생할 수 있으므로 핸들러 관리가 중요
            dgvMedicineList.CellValueChanged -= dgvMedicineList_CellValueChanged;

            foreach (DataGridViewRow r in dgvMedicineList.Rows)
            {
                if (r.Cells[1].Value.ToString() == targetMedName)
                {
                    int[] info = (int[])r.Tag;
                    int rDailyDosage = info[0];

                    double newDays = (rDailyDosage > 0) ? ((double)newStock / rDailyDosage) : 0;
                    r.Cells[4].Value = $"{newStock}정 ({newDays:0.#}일분)";
                }
            }

            dgvMedicineList.CellValueChanged += dgvMedicineList_CellValueChanged;
        }

        private void UpdateStockToDB(string medName, int newStock)
        {
            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Medications SET stock_quantity = @stock WHERE med_name = @name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@stock", newStock);
                    cmd.Parameters.AddWithValue("@name", medName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("저장 오류: " + ex.Message);
                }
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            updateCurrentTime();
        }

        private void btnMedicineRegi_Click(object sender, EventArgs e)
        {
            MedicineForm medForm = new MedicineForm();
            medForm.ShowDialog();
            LoadData();
        }

        private void btnScheduleMan_Click(object sender, EventArgs e)
        {
            ScheduleManagementForm scheduleForm = new ScheduleManagementForm(this.currentUserName);
            scheduleForm.ShowDialog();
            LoadData();
        }

        // [추가] 로그아웃 버튼
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("로그아웃 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 애플리케이션을 재시작하여 로그인 화면으로 돌아갑니다.
                Application.Restart();
            }
        }

        private void btnTakeMorning_Click(object sender, EventArgs e)
        {
            TakeMedsBySlot("Morning");
        }

        private void btnTakeLunch_Click(object sender, EventArgs e)
        {
            TakeMedsBySlot("Lunch");
        }

        private void btnTakeDinner_Click(object sender, EventArgs e)
        {
            TakeMedsBySlot("Dinner");
        }

        private void TakeMedsBySlot(string targetSlot)
        {
            List<DataGridViewRow> targetRows = new List<DataGridViewRow>();
            bool isAllChecked = true;

            foreach (DataGridViewRow row in dgvMedicineList.Rows)
            {
                string timeStr = row.Cells[0].Value.ToString();
                int medHour = int.Parse(timeStr.Split(':')[0]);

                string medSlot = "";
                if (medHour < 11) medSlot = "Morning";
                else if (medHour < 16) medSlot = "Lunch";
                else medSlot = "Dinner";

                if (targetSlot == medSlot)
                {
                    targetRows.Add(row);
                    if (!Convert.ToBoolean(row.Cells["colCheck"].Value))
                    {
                        isAllChecked = false;
                    }
                }
            }

            if (targetRows.Count == 0) return;

            bool newState = !isAllChecked;

            // 일괄 변경 시에는 정렬을 마지막에 한 번만 하기 위해 이벤트 잠시 해제 고려 가능하나,
            // 간단하게 루프 돌면서 처리
            foreach (DataGridViewRow row in targetRows)
            {
                if (Convert.ToBoolean(row.Cells["colCheck"].Value) != newState)
                {
                    row.Cells["colCheck"].Value = newState;
                }
            }
        }

        private void btnTakeAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("현재 목록에 있는 모든 약을 복용 처리하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvMedicineList.Rows)
                {
                    if (!Convert.ToBoolean(row.Cells["colCheck"].Value))
                    {
                        row.Cells["colCheck"].Value = true;
                    }
                }
            }
        }
    }
}