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
        string connectDB = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";
        private string currentUserName;
        private string lastCheckedMinute = "";
        private bool isLoading = false;

        public Form1(string userName)
        {
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

            if (!dgvMedicineList.Columns.Contains("colScheduleId"))
            {
                DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
                colId.Name = "colScheduleId";
                colId.Visible = false;
                dgvMedicineList.Columns.Add(colId);
            }

            KakaoHelper.LoadToken();
            if (!KakaoHelper.IsTokenLoaded)
            {
                Console.WriteLine("kakao_token.txt 파일이 없습니다.");
            }

            updateCurrentTime();
            LoadData();
            updateTodayMedicineCount();

            dgvMedicineList.ReadOnly = false;
            dgvMedicineList.Columns[0].ReadOnly = true;
            dgvMedicineList.Columns[1].ReadOnly = true;
            dgvMedicineList.Columns[2].ReadOnly = false;
            dgvMedicineList.Columns[3].ReadOnly = false;
            dgvMedicineList.Columns[4].ReadOnly = true;

            if (dgvMedicineList.Columns["colDosage"] != null)
            {
                dgvMedicineList.Columns["colDosage"].ReadOnly = true;
                dgvMedicineList.Columns["colDosage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvMedicineList.Columns["colDosage"].DisplayIndex = 2;
            }

            dgvMedicineList.CurrentCellDirtyStateChanged += dgvMedicineList_CurrentCellDirtyStateChanged;
            dgvMedicineList.CellValueChanged += dgvMedicineList_CellValueChanged;
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
            isLoading = true;
            dgvMedicineList.Rows.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            S.schedule_id,
                            S.take_time, 
                            M.med_name, 
                            M.stock_quantity, 
                            S.daily_dosage,
                            S.dosage_per_take,
                            IFNULL(L.is_taken, 0) AS is_taken
                        FROM Schedules S
                        JOIN Medications M ON S.med_id = M.med_id
                        JOIN Users U ON S.user_id = U.user_id
                        LEFT JOIN MedicationLogs L 
                            ON S.schedule_id = L.schedule_id 
                            AND L.taken_date = CURDATE()
                        WHERE U.user_name = @userName 
                        ORDER BY S.take_time ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", this.currentUserName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int scheduleId = Convert.ToInt32(reader["schedule_id"]);
                            string timeStr = reader["take_time"].ToString();
                            if (timeStr.Length > 5) timeStr = timeStr.Substring(0, 5);
                            string medName = reader["med_name"].ToString();

                            bool isTaken = Convert.ToBoolean(reader["is_taken"]);

                            int stock = Convert.ToInt32(reader["stock_quantity"]);
                            int dailyDosage = Convert.ToInt32(reader["daily_dosage"]);
                            int dosagePerTake = Convert.ToInt32(reader["dosage_per_take"]);

                            double days = (dailyDosage > 0) ? ((double)stock / dailyDosage) : 0;

                            string remainText = $"{stock}정 ({days:0.#}일분)";
                            string dosageText = $"{dosagePerTake}정";

                            int rowIndex = dgvMedicineList.Rows.Add(timeStr, medName, isTaken, "", remainText, dosageText);

                            dgvMedicineList.Rows[rowIndex].Tag = new int[] { dailyDosage, dosagePerTake };
                            dgvMedicineList.Rows[rowIndex].Cells["colScheduleId"].Value = scheduleId;
                        }
                    }

                    SortMedicineList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터 로드 실패: " + ex.Message);
                }
                finally
                {
                    isLoading = false;
                }
            }
        }

        private void SortMedicineList()
        {
            dgvMedicineList.Sort(new RowComparer());
        }

        private class RowComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = (DataGridViewRow)x;
                DataGridViewRow row2 = (DataGridViewRow)y;

                bool check1 = Convert.ToBoolean(row1.Cells["colCheck"].Value);
                bool check2 = Convert.ToBoolean(row2.Cells["colCheck"].Value);

                int result = check1.CompareTo(check2);

                if (result == 0)
                {
                    string time1 = row1.Cells[0].Value.ToString();
                    string time2 = row2.Cells[0].Value.ToString();
                    result = time1.CompareTo(time2);
                }

                return result;
            }
        }

        private void dgvMedicineList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMedicineList.Rows[e.RowIndex];
                bool isChecked = Convert.ToBoolean(row.Cells["colCheck"].Value);

                if (isChecked)
                {
                    e.CellStyle.Font = new Font(dgvMedicineList.Font, FontStyle.Strikeout);
                    e.CellStyle.ForeColor = Color.Gray;
                    e.CellStyle.SelectionForeColor = Color.Gray;
                }
                else
                {
                    e.CellStyle.Font = new Font(dgvMedicineList.Font, FontStyle.Regular);
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
            if (isLoading) return;

            if (e.RowIndex >= 0 && e.ColumnIndex == 2) // colCheck 인덱스
            {
                DataGridViewRow row = dgvMedicineList.Rows[e.RowIndex];

                bool isChecked = Convert.ToBoolean(row.Cells[2].Value);
                string medName = row.Cells[1].Value.ToString();

                int scheduleId = Convert.ToInt32(row.Cells["colScheduleId"].Value);

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

                SaveLogStatus(scheduleId, isChecked);

                SyncAllRowsStock(medName, newStock);
                updateTodayMedicineCount();

                this.BeginInvoke(new Action(() => {
                    SortMedicineList();
                    dgvMedicineList.Refresh();
                }));
            }
        }

        private void SaveLogStatus(int scheduleId, bool isChecked)
        {
            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT log_id FROM MedicationLogs WHERE schedule_id = @sid AND taken_date = CURDATE()";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@sid", scheduleId);
                    object result = checkCmd.ExecuteScalar();

                    if (result != null)
                    {
                        string updateQuery = "UPDATE MedicationLogs SET is_taken = @taken, taken_at = NOW() WHERE log_id = @lid";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@taken", isChecked ? 1 : 0);
                        updateCmd.Parameters.AddWithValue("@lid", Convert.ToInt32(result));
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO MedicationLogs (schedule_id, taken_date, is_taken, taken_at) VALUES (@sid, CURDATE(), @taken, NOW())";
                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@sid", scheduleId);
                        insertCmd.Parameters.AddWithValue("@taken", isChecked ? 1 : 0);
                        insertCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("로그 저장 실패: " + ex.Message);
                }
            }
        }

        private void SyncAllRowsStock(string targetMedName, int newStock)
        {
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
            CheckAndSendKakaoAlert();
        }

        private void CheckAndSendKakaoAlert()
        {
            string currentMinute = DateTime.Now.ToString("HH:mm");

            if (lastCheckedMinute == currentMinute) return;
            lastCheckedMinute = currentMinute;

            if (!KakaoHelper.IsTokenLoaded) return;

            foreach (DataGridViewRow row in dgvMedicineList.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                string timeStr = row.Cells[0].Value.ToString();
                bool isTaken = Convert.ToBoolean(row.Cells["colCheck"].Value);
                string medName = row.Cells[1].Value.ToString();

                if (timeStr.StartsWith(currentMinute) && !isTaken)
                {
                    string message = $"{currentUserName}님, {medName} 복용 시간입니다 ({timeStr})";
                    KakaoHelper.SendMessageAsync(message);

                    System.Media.SystemSounds.Asterisk.Play();

                }
            }
        }

        // [수정] 약물 관리 버튼: 생성자에 this.currentUserName 전달
        private void btnMedicineRegi_Click(object sender, EventArgs e)
        {
            MedicineForm medForm = new MedicineForm(this.currentUserName);
            medForm.ShowDialog();
            LoadData();
        }

        private void btnScheduleMan_Click(object sender, EventArgs e)
        {
            ScheduleManagementForm scheduleForm = new ScheduleManagementForm(this.currentUserName);
            scheduleForm.ShowDialog();
            LoadData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("로그아웃 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
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