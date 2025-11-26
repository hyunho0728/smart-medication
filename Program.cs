using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace smart_medication
{
    static class Program
    {
        // db 없을 시 생성 및 스키마 업데이트
        private static void InitializeDB()
        {
            string connectDB = "Server=localhost;Port=3306;Uid=root;Pwd=1234;Charset=utf8";

            using (MySqlConnection conn = new MySqlConnection(connectDB))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();

                    // 1. DB 생성
                    cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `smart_med_db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "USE `smart_med_db`;";
                    cmd.ExecuteNonQuery();

                    // 2. 테이블 생성
                    List<string> query = new List<string>();

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
                              `stock_quantity` int DEFAULT '0' COMMENT '현재 재고 수량',
                              `total_days` int DEFAULT '0' COMMENT '총 처방 일수',
                              `memo` text COLLATE utf8mb4_unicode_ci COMMENT '비고',
                              `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
                              PRIMARY KEY (`med_id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    query.Add(@"CREATE TABLE IF NOT EXISTS `schedules` (
                              `schedule_id` int NOT NULL AUTO_INCREMENT COMMENT '스케줄 고유 ID',
                              `user_id` int NOT NULL COMMENT '사용자 ID',
                              `med_id` int NOT NULL COMMENT '약품 ID',
                              `take_time` time NOT NULL COMMENT '복용 시간',
                              `daily_dosage` int DEFAULT '3' COMMENT '하루 총 복용량',
                              `dosage_per_take` int DEFAULT '1' COMMENT '1회 복용량',
                              PRIMARY KEY (`schedule_id`),
                              KEY `user_id` (`user_id`),
                              KEY `med_id` (`med_id`),
                              CONSTRAINT `schedules_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
                              CONSTRAINT `schedules_ibfk_2` FOREIGN KEY (`med_id`) REFERENCES `medications` (`med_id`) ON DELETE CASCADE
                            ) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    query.Add(@"CREATE TABLE IF NOT EXISTS `medicationlogs` (
                              `log_id` int NOT NULL AUTO_INCREMENT,
                              `schedule_id` int NOT NULL,
                              `taken_date` date NOT NULL COMMENT '복용 날짜',
                              `is_taken` tinyint(1) DEFAULT '0' COMMENT '복용 여부',
                              `taken_at` datetime DEFAULT NULL COMMENT '실제 복용 시간',
                              PRIMARY KEY (`log_id`),
                              KEY `schedule_id` (`schedule_id`),
                              CONSTRAINT `medicationlogs_ibfk_1` FOREIGN KEY (`schedule_id`) REFERENCES `schedules` (`schedule_id`) ON DELETE CASCADE
                            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;");

                    foreach (var q in query)
                    {
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                    }

                    // 3. 마이그레이션: Medications 테이블에 user_id 컬럼 추가
                    try
                    {
                        cmd.CommandText = "SHOW COLUMNS FROM `medications` LIKE 'user_id';";
                        if (cmd.ExecuteScalar() == null)
                        {
                            // 컬럼 추가
                            cmd.CommandText = "ALTER TABLE `medications` ADD COLUMN `user_id` int DEFAULT NULL COMMENT '소유자 ID';";
                            cmd.ExecuteNonQuery();

                            // (선택) 기존 데이터 처리: user_id가 NULL인 약품은 임시로 1번 유저(보통 관리자)의 것으로 설정하거나 그대로 둠
                            // cmd.CommandText = "UPDATE medications SET user_id = 1 WHERE user_id IS NULL";
                            // cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Medications 컬럼 추가 오류(무시 가능): " + ex.Message);
                    }

                    // 4. 마이그레이션: Schedules 테이블에 dosage_per_take 컬럼 추가 (이전 요청사항)
                    try
                    {
                        cmd.CommandText = "SHOW COLUMNS FROM `schedules` LIKE 'dosage_per_take';";
                        if (cmd.ExecuteScalar() == null)
                        {
                            cmd.CommandText = "ALTER TABLE `schedules` ADD COLUMN `dosage_per_take` int DEFAULT '1' COMMENT '1회 복용량';";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("Schedules 컬럼 추가 오류: " + ex.Message); }

                    // 5. 초기 유저 데이터
                    string[] insertQueries = {
                        "INSERT INTO users (user_name, password) SELECT '유현호', '1234' WHERE NOT EXISTS (SELECT * FROM users WHERE user_name = '유현호');",
                        "INSERT INTO users (user_name, password) SELECT 'admin', '0728' WHERE NOT EXISTS (SELECT * FROM users WHERE user_name = 'admin');"
                    };

                    foreach (var q in insertQueries)
                    {
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB 초기화 실패 : {ex.Message}");
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
            InitializeDB();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form1(login.LoggedInUserName));
            }
            else
            {
                Application.Exit();
            }
        }
    }
}