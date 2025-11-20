using System;
using System.Windows.Forms;

namespace smart_medication
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            bool debugMode = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!debugMode)
            {
                // 1. 로그인 폼을 먼저 생성
                LoginForm login = new LoginForm();

                // 2. 로그인 창을 모달(Dialog)로 띄움
                // 사용자가 로그인을 성공하면 DialogResult.OK를 반환함
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // 3. 로그인 성공 시에만 메인 폼(Form1) 실행
                    // 로그인한 사용자 이름을 Form1에 전달하고 싶다면 생성자를 수정해야 함 (선택사항)
                    Application.Run(new Form1(login.LoggedInUserName));
                }
                else
                {
                    // 로그인을 안 하고 닫거나 실패하면 프로그램 종료
                    Application.Exit();
                }
            }
            else Application.Run(new Form1("유현호"));
        }
    }
}