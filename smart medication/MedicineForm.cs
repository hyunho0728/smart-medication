using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    // [변경] 클래스 이름이 MedicineForm으로 바뀜
    public partial class MedicineForm : Form
    {
        string connectDB = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";

        // [변경] 생성자 이름도 MedicineForm으로 바뀜
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
                    string query = "SELECT med_id, med_name, stock_quantity FROM Medications";
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
                    txtMedName.Clear();
                    numStock.Value = 0;
                    LoadDrugList();
                }
                catch (Exception ex) { MessageBox.Show("등록 에러: " + ex.Message); }
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
                    LoadDrugList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("삭제 에러: " + ex.Message);
                }
            }
        }
    }
}