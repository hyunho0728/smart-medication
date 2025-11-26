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

            this.currentUserName = userName;
            lblWelcom.Text = $"안녕하세요 {userName}님!";

            updateCurrentTime(); // 켜자마자 시간 한 번 표시

            // 데이터 불러오기
            LoadData();

            // 오늘 약물 개수 갱신
            updateTodayMedicineCount();

            // 체크박스 클릭을 위해 읽기 전용 해제 설정

            dgvMedicineList.ReadOnly = false;

            dgvMedicineList.Columns[0].ReadOnly = true; // 시간
            dgvMedicineList.Columns[1].ReadOnly = true; // 약품
            dgvMedicineList.Columns[2].ReadOnly = false; // 체크박스 (얘만 수정 가능)
            dgvMedicineList.Columns[3].ReadOnly = false; // 비고
            dgvMedicineList.Columns[4].ReadOnly = true; // 남은 약

            // 체크박스 즉시 반응을 위한 이벤트 연결
            dgvMedicineList.CurrentCellDirtyStateChanged += dgvMedicineList_CurrentCellDirtyStateChanged;
            dgvMedicineList.CellValueChanged += dgvMedicineList_CellValueChanged;

            // 선택 시 파란색으로 변하는 것 방지
            dgvMedicineList.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvMedicineList.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMedicineList.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvMedicineList.ColumnHeadersDefaultCellStyle.BackColor;
        }

        private void updateCurrentTime()
        {
            lblTimeInfo.Text = $"현재 시간 : {DateTime.Now.ToString("HH:mm:ss")}";
        }

        // 오늘 먹어야하는 약 개수 갱신
        private void updateTodayMedicineCount()
        {
            int totalCount = dgvMedicineList.Rows.Count; // 전체 약 개수
            int notTakenCount = 0; // 미복용 개수 변수

            // 리스트의 모든 줄을 하나씩 꺼내서 검사
            foreach (DataGridViewRow row in dgvMedicineList.Rows)
            {
                // "colCheck"는 디자인 파일에서 만든 체크박스 컬럼의 이름입니다.
                // 만약 이름을 모르면 인덱스인 row.Cells[2].Value 를 써도 됩니다.
                object value = row.Cells["colCheck"].Value;

                // 값이 null이거나 false이면 미복용으로 카운트
                // (Convert.ToBoolean은 null을 false로 처리해줘서 안전합니다)
                if (Convert.ToBoolean(value) == false)
                {
                    notTakenCount++;
                }
            }

            // 라벨 업데이트
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
                            S.daily_dosage
                        FROM Schedules S
                        JOIN Medications M ON S.med_id = M.med_id
                        JOIN Users U ON S.user_id = U.user_id
                        WHERE U.user_name = @userName 
                        ORDER BY S.take_time ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", this.currentUserName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string timeStr = reader["take_time"].ToString();
                            if (timeStr.Length > 5) timeStr = timeStr.Substring(0, 5);
                            string medName = reader["med_name"].ToString();

                            // 재고는 약품 테이블(M)에서 가져옴 (공통 재고니까)
                            int stock = Convert.ToInt32(reader["stock_quantity"]);

                            // 하루 복용량은 스케줄 테이블(S)에서 가져옴
                            int dailyDosage = Convert.ToInt32(reader["daily_dosage"]);

                            // 일분 계산 (재고 / 하루복용량)
                            double days = (dailyDosage > 0) ? ((double)stock / dailyDosage) : 0;

                            string remainText = $"{stock}정 ({days:0.#}일분)";

                            // 행 추가
                            int rowIndex = dgvMedicineList.Rows.Add(timeStr, medName, false, "", remainText);

                            // 나중에 체크박스 계산을 위해 Tag에 저장해둠
                            dgvMedicineList.Rows[rowIndex].Tag = dailyDosage;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터 로드 실패: " + ex.Message);
                }
            }
        }

        // 체크박스를 누르는 순간 "값이 바뀌었다"고 강제로 알림 (Commit)
        private void dgvMedicineList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMedicineList.IsCurrentCellDirty)
            {
                // 즉시 값을 저장합니다.
                dgvMedicineList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // 값이 저장된 직후에 개수 세는 함수 실행
        private void dgvMedicineList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 1. 체크박스 컬럼(인덱스 2)인지 확인
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                DataGridViewRow row = dgvMedicineList.Rows[e.RowIndex];

                // 체크 여부 확인
                bool isChecked = Convert.ToBoolean(row.Cells[2].Value);
                string medName = row.Cells[1].Value.ToString();

                // 2. 현재 화면에 적힌 "15정 (5일분)" 텍스트 가져오기
                string currentText = row.Cells[4].Value.ToString();

                // '정'이라는 글자 앞의 숫자만 잘라냄 (예: 15)
                int currentStock = 0;
                try
                {
                    // "15정" 에서 15만 추출
                    currentStock = int.Parse(currentText.Split('정')[0]);
                }
                catch
                {
                    return; // 에러나면 중단
                }

                // 3. 아까 저장해둔 하루 복용량(Tag) 꺼내기
                int dailyDosage = 0;
                if (row.Tag != null) dailyDosage = Convert.ToInt32(row.Tag);

                // 4. 재고 계산 (체크하면 -1, 해제하면 +1)
                int newStock = isChecked ? currentStock - 1 : currentStock + 1;
                if (newStock < 0) newStock = 0; // 0보다 작아질 순 없음

                // 5. 남은 일수 다시 계산
                double newDays = (dailyDosage > 0) ? ((double)newStock / dailyDosage) : 0;

                // 6. 화면 업데이트 ("14정 (4일분)" 으로 변경)
                // 이벤트를 잠시 끊어야 무한루프 안 생김
                dgvMedicineList.CellValueChanged -= dgvMedicineList_CellValueChanged;
                row.Cells[4].Value = $"{newStock}정 ({newDays:0.#}일분)";
                dgvMedicineList.CellValueChanged += dgvMedicineList_CellValueChanged;

                // 7. 미복용 개수 라벨 갱신
                updateTodayMedicineCount();

                // 8. 실제 DB에 저장 (업데이트)
                UpdateStockToDB(medName, newStock);
            }
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
                    //너무 자주 뜨면 불편하니 주석 처리해도 됨
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

            LoadData(); // 창 닫으면 메인화면 새로고침
        }

        private void btnScheduleMan_Click(object sender, EventArgs e)
        {
            ScheduleManagementForm scheduleForm = new ScheduleManagementForm(this.currentUserName);
            scheduleForm.ShowDialog();

            // 창을 닫고 나면 메인 화면 데이터도 갱신
            LoadData();
        }
    }
}