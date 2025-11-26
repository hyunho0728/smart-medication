using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    public partial class MedicineForm : Form
    {
        string connectDB = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";

        private int selectedMedId = -1;
        private string currentUserName;
        private int currentUserId = -1;

        // 생성자 변경: 사용자 이름을 받음
        public MedicineForm(string userName)
        {
            InitializeComponent();
            this.currentUserName = userName;
            GetUserId(); // 사용자 ID 조회
            LoadDrugList();
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
                }
                catch (Exception ex) { MessageBox.Show("사용자 정보 로드 실패: " + ex.Message); }
            }
        }

        private void LoadDrugList()
        {
            if (currentUserId == -1) return;

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    // [수정] 내 아이디(user_id)에 해당하는 약품만 조회
                    string query = "SELECT med_id, med_name, stock_quantity FROM Medications WHERE user_id = @uid ORDER BY med_name";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@uid", this.currentUserId);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvDrugs.DataSource = dt;

                    if (dgvDrugs.Columns["med_id"] != null) dgvDrugs.Columns["med_id"].Visible = false;
                    if (dgvDrugs.Columns["med_name"] != null) dgvDrugs.Columns["med_name"].HeaderText = "약품명";
                    if (dgvDrugs.Columns["stock_quantity"] != null) dgvDrugs.Columns["stock_quantity"].HeaderText = "현재 재고";
                }
                catch (Exception ex) { MessageBox.Show("목록 로드 에러: " + ex.Message); }
            }
        }

        private void dgvDrugs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDrugs.Rows[e.RowIndex];
                selectedMedId = Convert.ToInt32(row.Cells["med_id"].Value);
                txtMedName.Text = row.Cells["med_name"].Value.ToString();
                numStock.Value = Convert.ToInt32(row.Cells["stock_quantity"].Value);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedName.Text))
            {
                MessageBox.Show("약품 이름을 입력해주세요.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    // [수정] user_id 포함하여 저장
                    string query = "INSERT INTO Medications (med_name, stock_quantity, user_id) VALUES (@name, @stock, @uid)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@stock", (int)numStock.Value);
                    cmd.Parameters.AddWithValue("@uid", this.currentUserId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("등록되었습니다.");
                    ResetInput();
                    LoadDrugList();
                }
                catch (Exception ex) { MessageBox.Show("등록 에러: " + ex.Message); }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMedId == -1)
            {
                MessageBox.Show("수정할 약품을 선택해주세요.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMedName.Text))
            {
                MessageBox.Show("약품 이름은 비어있을 수 없습니다.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    // [수정] 내 약품인지 확인하는 조건(@uid) 추가 (혹시 모를 오류 방지)
                    string query = "UPDATE Medications SET med_name = @name, stock_quantity = @stock WHERE med_id = @id AND user_id = @uid";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@stock", (int)numStock.Value);
                    cmd.Parameters.AddWithValue("@id", selectedMedId);
                    cmd.Parameters.AddWithValue("@uid", this.currentUserId);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("수정되었습니다.");
                        ResetInput();
                        LoadDrugList();
                    }
                    else
                    {
                        MessageBox.Show("수정 권한이 없거나 약품을 찾을 수 없습니다.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("수정 에러: " + ex.Message); }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDrugs.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 약품을 선택해주세요.");
                return;
            }

            if (MessageBox.Show("정말 삭제하시겠습니까?\n(관련된 스케줄도 모두 삭제됩니다)", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int medId = Convert.ToInt32(dgvDrugs.SelectedRows[0].Cells["med_id"].Value);

                    using (MySqlConnection conn = new MySqlConnection(connectDB))
                    {
                        conn.Open();
                        // [수정] 내 약품만 삭제
                        string query = "DELETE FROM Medications WHERE med_id = @id AND user_id = @uid";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", medId);
                        cmd.Parameters.AddWithValue("@uid", this.currentUserId);
                        cmd.ExecuteNonQuery();
                    }

                    ResetInput();
                    LoadDrugList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("삭제 에러: " + ex.Message);
                }
            }
        }

        private void ResetInput()
        {
            txtMedName.Clear();
            numStock.Value = 0;
            selectedMedId = -1;
        }
    }
}