using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace smart_medication
{
    public partial class LoginForm : Form
    {
        string connectionString = "Server=localhost;Port=3306;Database=smart_med_db;Uid=root;Pwd=1234;Charset=utf8";

        public string LoggedInUserName { get; private set; }

        // 개발용 자동 로그인 (테스트 할 때는 true, 실제 사용 시 false로 변경 권장)
        bool autoLogin = false;

        public LoginForm()
        {
            InitializeComponent();
            this.Text = "로그인";
            this.StartPosition = FormStartPosition.CenterScreen;

            if (autoLogin)
            {
                txtUserId.Text = "유현호";
                txtPassword.Text = "1234";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputName = txtUserId.Text.Trim();
            string inputPwd = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(inputName) || string.IsNullOrEmpty(inputPwd))
            {
                MessageBox.Show("아이디와 비밀번호를 모두 입력해주세요.");
                return;
            }

            if (TryLogin(inputName, inputPwd))
            {
                MessageBox.Show($"{inputName}님 환영합니다!");
                this.LoggedInUserName = inputName;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.");
            }
        }

        // [추가] 회원가입 버튼 클릭 이벤트
        private void btnGoRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog(); // 모달 창으로 띄움
        }

        private bool TryLogin(string name, string pwd)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE user_name = @name AND password = @pwd";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@pwd", pwd);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("DB 에러: " + ex.Message);
                    return false;
                }
            }
        }
    }
}