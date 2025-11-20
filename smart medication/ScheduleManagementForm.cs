using System;
using System.Collections.Generic; // 리스트 사용을 위해 추가
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    public partial class ScheduleManagementForm : Form
    {
        string connectDB = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";
        private string currentUserName;
        private int currentUserId = -1;

        public ScheduleManagementForm(string userName)
        {
            InitializeComponent();
            this.currentUserName = userName;
            GetUserId();
            LoadMedicationCombo();
            LoadScheduleList();
        }

        // ... (GetUserId, LoadMedicationCombo 함수는 기존과 동일하므로 생략 가능하지만, 안전하게 복사해서 쓰세요) ...
        private void GetUserId()
        {
            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT user_id FROM Users WHERE user_name = @name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", this.currentUserName);
                    object result = cmd.ExecuteScalar();
                    if (result != null) this.currentUserId = Convert.ToInt32(result);
                    else { MessageBox.Show("사용자 오류"); this.Close(); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void LoadMedicationCombo()
        {
            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT med_id, med_name FROM Medications ORDER BY med_name";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    cboMeds.DataSource = dt;
                    cboMeds.DisplayMember = "med_name";
                    cboMeds.ValueMember = "med_id";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void LoadScheduleList()
        {
            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            S.schedule_id, 
                            M.med_name, 
                            S.take_time,
                            S.daily_dosage
                        FROM Schedules S
                        JOIN Medications M ON S.med_id = M.med_id
                        WHERE S.user_id = @uid
                        ORDER BY S.take_time ASC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@uid", this.currentUserId);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSchedules.DataSource = dt;

                    if (dgvSchedules.Columns["schedule_id"] != null) dgvSchedules.Columns["schedule_id"].Visible = false;
                    dgvSchedules.Columns["med_name"].HeaderText = "약품명";
                    dgvSchedules.Columns["take_time"].HeaderText = "복용 시간";
                    dgvSchedules.Columns["daily_dosage"].HeaderText = "하루 총 복용량";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        // =================================================================
        // [핵심 수정] 일괄 등록 버튼 로직
        // =================================================================
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (cboMeds.SelectedValue == null) { MessageBox.Show("약품을 선택하세요."); return; }
            if (!chkMorning.Checked && !chkLunch.Checked && !chkDinner.Checked) { MessageBox.Show("시간을 적어도 하나 체크하세요."); return; }

            int medId = Convert.ToInt32(cboMeds.SelectedValue);
            int pillsPerDose = (int)numOneTimeDosage.Value; // 1회 복용량 (예: 1알)

            // 1. 체크된 시간들을 리스트에 담기
            List<string> timesToAdd = new List<string>();
            if (chkMorning.Checked) timesToAdd.Add("08:00:00"); // 아침 8시
            if (chkLunch.Checked) timesToAdd.Add("13:00:00");   // 점심 1시
            if (chkDinner.Checked) timesToAdd.Add("19:00:00");  // 저녁 7시

            // 2. 하루 총 복용량 자동 계산 (횟수 * 1회량)
            int totalDailyDosage = timesToAdd.Count * pillsPerDose;

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction(); // 여러 개 넣으니까 트랜잭션 사용

                try
                {
                    foreach (string timeStr in timesToAdd)
                    {
                        string query = "INSERT INTO Schedules (user_id, med_id, take_time, daily_dosage) VALUES (@uid, @mid, @time, @dosage)";
                        MySqlCommand cmd = new MySqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@uid", this.currentUserId);
                        cmd.Parameters.AddWithValue("@mid", medId);
                        cmd.Parameters.AddWithValue("@time", timeStr);

                        // [중요] 계산된 하루 총 복용량을 저장함
                        cmd.Parameters.AddWithValue("@dosage", totalDailyDosage);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit(); // 모두 성공하면 저장
                    MessageBox.Show($"{timesToAdd.Count}건의 스케줄이 등록되었습니다.\n(하루 총 {totalDailyDosage}알)");
                    LoadScheduleList();
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // 에러나면 취소
                    MessageBox.Show("등록 실패: " + ex.Message);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.SelectedRows.Count == 0) return;

            if (MessageBox.Show("선택한 스케줄을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int scheduleId = Convert.ToInt32(dgvSchedules.SelectedRows[0].Cells["schedule_id"].Value);
                using (MySqlConnection conn = new MySqlConnection(connectDB))
                {
                    conn.Open();
                    string query = "DELETE FROM Schedules WHERE schedule_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", scheduleId);
                    cmd.ExecuteNonQuery();
                }
                LoadScheduleList();
            }
        }
    }
}