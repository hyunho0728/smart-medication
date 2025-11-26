using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    public partial class MedicineForm : Form
    {
        string connectDB = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";

        // 현재 선택된 약품의 ID를 저장 (-1이면 선택 안됨)
        private int selectedMedId = -1;

        public MedicineForm()
        {
            InitializeComponent();
            LoadDrugList();
        }

        private void LoadDrugList()
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

                    dgvDrugs.DataSource = dt;

                    if (dgvDrugs.Columns["med_id"] != null) dgvDrugs.Columns["med_id"].Visible = false;
                    if (dgvDrugs.Columns["med_name"] != null) dgvDrugs.Columns["med_name"].HeaderText = "약품명";
                    if (dgvDrugs.Columns["stock_quantity"] != null) dgvDrugs.Columns["stock_quantity"].HeaderText = "현재 재고";
                }
                catch (Exception ex) { MessageBox.Show("목록 로드 에러: " + ex.Message); }
            }
        }

        // [추가] 리스트의 셀을 클릭하면 데이터를 입력칸으로 가져오기
        private void dgvDrugs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDrugs.Rows[e.RowIndex];

                // 선택된 ID 저장
                selectedMedId = Convert.ToInt32(row.Cells["med_id"].Value);

                // 입력칸에 값 채우기
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
                    string query = "INSERT INTO Medications (med_name, stock_quantity) VALUES (@name, @stock)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@stock", (int)numStock.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("등록되었습니다.");
                    ResetInput(); // 입력칸 초기화
                    LoadDrugList();
                }
                catch (Exception ex) { MessageBox.Show("등록 에러: " + ex.Message); }
            }
        }

        // [추가] 수정 버튼 로직
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedMedId == -1)
            {
                MessageBox.Show("수정할 약품을 목록에서 선택해주세요.");
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
                    // 이름과 재고 모두 수정 가능
                    string query = "UPDATE Medications SET med_name = @name, stock_quantity = @stock WHERE med_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@stock", (int)numStock.Value);
                    cmd.Parameters.AddWithValue("@id", selectedMedId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("수정되었습니다.");
                    ResetInput();
                    LoadDrugList();
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
                        string query = "DELETE FROM Medications WHERE med_id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", medId);
                        cmd.ExecuteNonQuery();
                    }

                    ResetInput(); // 삭제 후 입력칸도 비움
                    LoadDrugList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("삭제 에러: " + ex.Message);
                }
            }
        }

        // 입력칸 초기화 헬퍼 함수
        private void ResetInput()
        {
            txtMedName.Clear();
            numStock.Value = 0;
            selectedMedId = -1; // 선택 상태 해제
        }
    }
}