using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace smart_medication
{
    static class Program
    {
        // db 없을 시 생성
        private static void InitializeDB()
        {
            string connectDB = "Server=localhost;Port=3306;Uid=root;Pwd=1234;Charset=utf8";

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();

                    List<string> query = new List<string>();
                    query.Add("CREATE DATABASE IF NOT EXISTS `smart_med_db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
                    query.Add("USE `smart_med_db`;");
                    query.Add(@"CREATE TABLE IF NOT EXISTS `users` (
                              `user_id` int NOT NULL AUTO_INCREMENT COMMENT '사용자 고유 ID',
                              `user_name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '사용자 이름 (예: 유현호)',
                              `created_at` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '등록일자',
                              `password` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '0000',
                              PRIMARY KEY (`user_id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    query.Add(@"CREATE TABLE IF NOT EXISTS `medications` (
                              `med_id` int NOT NULL AUTO_INCREMENT COMMENT '약품 고유 ID',
                              `med_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '약품명 (예: 타이레놀)',
                              `stock_quantity` int DEFAULT '0' COMMENT '현재 재고 수량 (남은 약)',
                              `total_days` int DEFAULT '0' COMMENT '총 처방 일수 (예: 5일분)',
                              `memo` text COLLATE utf8mb4_unicode_ci COMMENT '비고/메모',
                              `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
                              PRIMARY KEY (`med_id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    query.Add(@"CREATE TABLE IF NOT EXISTS `schedules` (
                              `schedule_id` int NOT NULL AUTO_INCREMENT COMMENT '스케줄 고유 ID',
                              `user_id` int NOT NULL COMMENT '사용자 ID (Users 테이블 참조)',
                              `med_id` int NOT NULL COMMENT '약품 ID (Medications 테이블 참조)',
                              `take_time` time NOT NULL COMMENT '복용 시간 (예: 08:00:00)',
                              `daily_dosage` int DEFAULT '3' COMMENT '이 스케줄의 하루 총 복용량',
                              PRIMARY KEY (`schedule_id`),
                              KEY `user_id` (`user_id`),
                              KEY `med_id` (`med_id`),
                              CONSTRAINT `schedules_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
                              CONSTRAINT `schedules_ibfk_2` FOREIGN KEY (`med_id`) REFERENCES `medications` (`med_id`) ON DELETE CASCADE
                            ) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    query.Add(@"CREATE TABLE IF NOT EXISTS `medicationlogs` (
                              `log_id` int NOT NULL AUTO_INCREMENT,
                              `schedule_id` int NOT NULL,
                              `taken_date` date NOT NULL COMMENT '복용 날짜 (2023-10-25)',
                              `is_taken` tinyint(1) DEFAULT '0' COMMENT '복용 여부 (True/False)',
                              `taken_at` datetime DEFAULT NULL COMMENT '실제 복용 버튼 누른 시간',
                              PRIMARY KEY (`log_id`),
                              KEY `schedule_id` (`schedule_id`),
                              CONSTRAINT `medicationlogs_ibfk_1` FOREIGN KEY (`schedule_id`) REFERENCES `schedules` (`schedule_id`) ON DELETE CASCADE
                            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    query.Add(@"
                        INSERT INTO users (user_name, password) 
                        SELECT '유현호', '1234' 
                        WHERE NOT EXISTS (SELECT * FROM users WHERE user_name = '유현호');
                    ");

                    // 관리자 계정도 필요하면 추가
                    query.Add(@"
                        INSERT INTO users (user_name, password) 
                        SELECT 'admin', '0728' 
                        WHERE NOT EXISTS (SELECT * FROM users WHERE user_name = 'admin');
                    ");

                    MySqlCommand cmd = conn.CreateCommand();
                    foreach (var q in query)
                    {
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB 연결 실패 : {ex}");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [STAThread]
        static void Main()
        {
            bool debugMode = false;

            //
            InitializeDB();

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