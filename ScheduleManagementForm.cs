using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing; // 버튼 색상/위치 설정을 위해 추가
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    public partial class ScheduleManagementForm : Form
    {
        string connectDB = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";
        private string currentUserName;
        private int currentUserId = -1;
        private int selectedScheduleId = -1;

        public ScheduleManagementForm(string userName)
        {
            InitializeComponent();
            this.currentUserName = userName;

            // [추가] 카톡 테스트 버튼 생성 (메인화면에서 이동됨)
            Button btnTestKakao = new Button();
            btnTestKakao.Text = "카톡 테스트";
            btnTestKakao.BackColor = Color.Yellow;
            btnTestKakao.ForeColor = Color.Black;
            btnTestKakao.Size = new Size(100, 30);
            // 위치 조정: 제목(복용 시간 관리) 오른쪽 여백에 배치
            btnTestKakao.Location = new Point(480, 15);
            btnTestKakao.Click += BtnTestKakao_Click;
            this.Controls.Add(btnTestKakao);
            btnTestKakao.BringToFront();

            GetUserId();
            LoadMedicationCombo();
            LoadScheduleList();

            cboMeds.SelectedIndexChanged += CboMeds_SelectedIndexChanged;
            if (cboMeds.Items.Count > 0) CboMeds_SelectedIndexChanged(null, null);

            ResetInputs(false);
        }

        // [추가] 테스트 버튼 클릭 이벤트
        private async void BtnTestKakao_Click(object sender, EventArgs e)
        {
            // 토큰이 로드되었는지 확인 (보통 Form1에서 로드됨)
            if (!KakaoHelper.IsTokenLoaded)
            {
                // 혹시 로드가 안 되어 있으면 여기서 시도
                KakaoHelper.LoadToken();
            }

            if (!KakaoHelper.IsTokenLoaded)
            {
                MessageBox.Show("토큰이 로드되지 않았습니다.\n실행 폴더에 'kakao_token.txt'가 있는지 확인하세요.");
                return;
            }

            MessageBox.Show("전송을 시도합니다...");
            await KakaoHelper.SendMessageAsync("테스트 메시지입니다. 잘 도착했나요?");
            MessageBox.Show("전송 요청 완료! 카톡을 확인해보세요.");
        }

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
                    string query = "SELECT med_id, med_name, stock_quantity FROM Medications ORDER BY med_name";
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

        private void CboMeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMeds.SelectedItem is DataRowView row)
            {
                int stock = Convert.ToInt32(row["stock_quantity"]);
                lblCurrentStock.Text = $"현재 재고: {stock}정";
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
                            S.daily_dosage,
                            S.dosage_per_take,
                            S.med_id
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
                    if (dgvSchedules.Columns["med_id"] != null) dgvSchedules.Columns["med_id"].Visible = false;

                    dgvSchedules.Columns["med_name"].HeaderText = "약품명";
                    dgvSchedules.Columns["take_time"].HeaderText = "복용 시간";
                    dgvSchedules.Columns["daily_dosage"].HeaderText = "하루 총 복용량";
                    if (dgvSchedules.Columns["dosage_per_take"] != null) dgvSchedules.Columns["dosage_per_take"].HeaderText = "1회 복용량";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void dgvSchedules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSchedules.Rows[e.RowIndex];
                selectedScheduleId = Convert.ToInt32(row.Cells["schedule_id"].Value);

                if (row.Cells["med_id"].Value != null)
                {
                    cboMeds.SelectedValue = row.Cells["med_id"].Value;
                }

                ResetInputs(false);

                string timeStr = row.Cells["take_time"].Value.ToString();
                DateTime time = DateTime.Parse(timeStr);
                int dosage = Convert.ToInt32(row.Cells["dosage_per_take"].Value);

                int hour = time.Hour;

                if (hour < 12)
                {
                    chkMorning.Checked = true;
                    dtpMorning.Value = DateTime.Today.Add(time.TimeOfDay);
                    numMorning.Value = dosage;
                }
                else if (hour >= 12 && hour < 17)
                {
                    chkLunch.Checked = true;
                    dtpLunch.Value = DateTime.Today.Add(time.TimeOfDay);
                    numLunch.Value = dosage;
                }
                else
                {
                    chkDinner.Checked = true;
                    dtpDinner.Value = DateTime.Today.Add(time.TimeOfDay);
                    numDinner.Value = dosage;
                }
            }
        }

        private void ResetInputs(bool resetSelection)
        {
            if (resetSelection) selectedScheduleId = -1;

            chkMorning.Checked = false;
            chkLunch.Checked = false;
            chkDinner.Checked = false;

            numMorning.Value = 1;
            numLunch.Value = 1;
            numDinner.Value = 1;

            DateTime today = DateTime.Today;
            dtpMorning.Value = today.AddHours(8);
            dtpLunch.Value = today.AddHours(13);
            dtpDinner.Value = today.AddHours(19);
        }

        private void RecalculateDailyDosage(int medId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    string sumQuery = @"
                        SELECT COALESCE(SUM(dosage_per_take), 0) 
                        FROM Schedules 
                        WHERE user_id = @uid AND med_id = @mid";

                    MySqlCommand sumCmd = new MySqlCommand(sumQuery, conn);
                    sumCmd.Parameters.AddWithValue("@uid", this.currentUserId);
                    sumCmd.Parameters.AddWithValue("@mid", medId);

                    int newTotalDosage = Convert.ToInt32(sumCmd.ExecuteScalar());

                    string updateQuery = @"
                        UPDATE Schedules 
                        SET daily_dosage = @total 
                        WHERE user_id = @uid AND med_id = @mid";

                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@total", newTotalDosage);
                    updateCmd.Parameters.AddWithValue("@uid", this.currentUserId);
                    updateCmd.Parameters.AddWithValue("@mid", medId);

                    updateCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("총 복용량 재계산 오류: " + ex.Message);
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (cboMeds.SelectedValue == null) { MessageBox.Show("약품을 선택하세요."); return; }
            if (!chkMorning.Checked && !chkLunch.Checked && !chkDinner.Checked) { MessageBox.Show("시간을 적어도 하나 체크하세요."); return; }

            int medId = Convert.ToInt32(cboMeds.SelectedValue);

            List<(string Time, int Dosage)> schedulesToAdd = new List<(string, int)>();

            if (chkMorning.Checked)
                schedulesToAdd.Add((dtpMorning.Value.ToString("HH:mm:ss"), (int)numMorning.Value));

            if (chkLunch.Checked)
                schedulesToAdd.Add((dtpLunch.Value.ToString("HH:mm:ss"), (int)numLunch.Value));

            if (chkDinner.Checked)
                schedulesToAdd.Add((dtpDinner.Value.ToString("HH:mm:ss"), (int)numDinner.Value));

            int totalDailyDosage = 0;
            foreach (var item in schedulesToAdd) totalDailyDosage += item.Dosage;

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    foreach (var schedule in schedulesToAdd)
                    {
                        string query = "INSERT INTO Schedules (user_id, med_id, take_time, daily_dosage, dosage_per_take) VALUES (@uid, @mid, @time, @total, @perTake)";
                        MySqlCommand cmd = new MySqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@uid", this.currentUserId);
                        cmd.Parameters.AddWithValue("@mid", medId);
                        cmd.Parameters.AddWithValue("@time", schedule.Time);
                        cmd.Parameters.AddWithValue("@total", totalDailyDosage);
                        cmd.Parameters.AddWithValue("@perTake", schedule.Dosage);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    RecalculateDailyDosage(medId);

                    MessageBox.Show("등록되었습니다.");
                    ResetInputs(true);
                    LoadScheduleList();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("등록 실패: " + ex.Message);
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedScheduleId == -1)
            {
                MessageBox.Show("목록에서 수정할 스케줄을 선택해주세요.");
                return;
            }

            int checkCount = 0;
            if (chkMorning.Checked) checkCount++;
            if (chkLunch.Checked) checkCount++;
            if (chkDinner.Checked) checkCount++;

            if (checkCount != 1)
            {
                MessageBox.Show("수정 시에는 하나의 시간대만 선택해야 합니다.");
                return;
            }

            string newTime = "";
            int newDosage = 0;

            if (chkMorning.Checked)
            {
                newTime = dtpMorning.Value.ToString("HH:mm:ss");
                newDosage = (int)numMorning.Value;
            }
            else if (chkLunch.Checked)
            {
                newTime = dtpLunch.Value.ToString("HH:mm:ss");
                newDosage = (int)numLunch.Value;
            }
            else if (chkDinner.Checked)
            {
                newTime = dtpDinner.Value.ToString("HH:mm:ss");
                newDosage = (int)numDinner.Value;
            }

            int medId = Convert.ToInt32(cboMeds.SelectedValue);

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        UPDATE Schedules 
                        SET med_id = @mid, take_time = @time, dosage_per_take = @perTake
                        WHERE schedule_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mid", medId);
                    cmd.Parameters.AddWithValue("@time", newTime);
                    cmd.Parameters.AddWithValue("@perTake", newDosage);
                    cmd.Parameters.AddWithValue("@id", selectedScheduleId);
                    cmd.ExecuteNonQuery();

                    RecalculateDailyDosage(medId);

                    MessageBox.Show("수정되었습니다.");
                    ResetInputs(true);
                    LoadScheduleList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("수정 실패: " + ex.Message);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSchedules.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 스케줄을 선택해주세요.");
                return;
            }

            if (MessageBox.Show("선택한 스케줄을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int scheduleId = Convert.ToInt32(dgvSchedules.SelectedRows[0].Cells["schedule_id"].Value);
                    int medId = Convert.ToInt32(dgvSchedules.SelectedRows[0].Cells["med_id"].Value);

                    using (MySqlConnection conn = new MySqlConnection(connectDB))
                    {
                        conn.Open();
                        string query = "DELETE FROM Schedules WHERE schedule_id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", scheduleId);
                        cmd.ExecuteNonQuery();
                    }

                    RecalculateDailyDosage(medId);

                    ResetInputs(true);
                    LoadScheduleList();
                }
                catch (Exception ex) { MessageBox.Show("삭제 실패: " + ex.Message); }
            }
        }
    }
}