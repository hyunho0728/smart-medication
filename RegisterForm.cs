using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    public partial class RegisterForm : Form
    {
        string connectionString = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string userName = txtRegId.Text.Trim();
            string pwd = txtRegPwd.Text.Trim();
            string pwdConfirm = txtRegPwdConfirm.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("아이디와 비밀번호를 모두 입력해주세요.");
                return;
            }

            if (pwd != pwdConfirm)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. 중복 아이디 체크
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE user_name = @name";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@name", userName);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("이미 존재하는 아이디입니다.");
                        return;
                    }

                    // 2. 회원가입 진행
                    string insertQuery = "INSERT INTO Users (user_name, password) VALUES (@name, @pwd)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@name", userName);
                    insertCmd.Parameters.AddWithValue("@pwd", pwd);
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("회원가입이 완료되었습니다!\n로그인 해주세요.");
                    this.Close(); // 창 닫기
                }
                catch (Exception ex)
                {
                    MessageBox.Show("회원가입 실패: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}