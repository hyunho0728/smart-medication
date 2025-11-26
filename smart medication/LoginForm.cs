using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // MySQL 라이브러리

namespace smart_medication
{
    public partial class LoginForm : Form
    {
        // DB 연결 문자열 (본인 비밀번호로 수정 필수!)
        string connectionString = "Server=localhost;Database=smart_med_db;Uid=root;Pwd=1234;";

        // 로그인 성공한 사용자 이름을 저장할 변수
        public string LoggedInUserName { get; private set; }

        // 개발용 자동 로그인
        bool autoLogin = true;

        public LoginForm()
        {
            InitializeComponent();
            this.Text = "로그인";
            this.StartPosition = FormStartPosition.CenterScreen;

            if (autoLogin)
            {
                txtUserId.Text = "hyunho0728";
                txtPassword.Text = "HyunHo0728@";
            }
        }

        // 로그인 버튼 클릭 이벤트 (디자이너에서 더블클릭해서 연결 필요)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputName = txtUserId.Text;
            string inputPwd = txtPassword.Text;

            if (string.IsNullOrEmpty(inputName) || string.IsNullOrEmpty(inputPwd))
            {
                MessageBox.Show("아이디와 비밀번호를 모두 입력해주세요.");
                return;
            }

            if (TryLogin(inputName, inputPwd))
            {
                // 로그인 성공 시
                MessageBox.Show($"{inputName}님 환영합니다!");
                this.LoggedInUserName = inputName;
                this.DialogResult = DialogResult.OK; // 성공 신호 보냄
                this.Close(); // 로그인 창 닫기
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 틀렸습니다.");
            }
        }

        private bool TryLogin(string name, string pwd)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // DB에서 아이디와 비번이 일치하는지 확인 (COUNT 이용)
                    string query = "SELECT COUNT(*) FROM Users WHERE user_name = @name AND password = @pwd";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@pwd", pwd);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0; // 1명 이상 있으면 true (성공)
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