using System;
using System.Collections.Generic; // Dictionary 사용을 위해 추가
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smart_medication
{
    public static class KakaoHelper
    {
        private static string kakaoAccessToken = "";
        private static readonly string apiUrl = "https://kapi.kakao.com/v2/api/talk/memo/default/send";

        public static void LoadToken()
        {
            string path = Path.Combine(Application.StartupPath, "kakao_token.txt");

            if (!File.Exists(path))
            {
                string currentDir = Application.StartupPath;
                for (int i = 0; i < 3; i++)
                {
                    DirectoryInfo parent = Directory.GetParent(currentDir);
                    if (parent == null) break;

                    string checkPath = Path.Combine(parent.FullName, "kakao_token.txt");
                    if (File.Exists(checkPath))
                    {
                        path = checkPath;
                        break;
                    }
                    currentDir = parent.FullName;
                }
            }

            if (File.Exists(path))
            {
                try
                {
                    kakaoAccessToken = File.ReadAllText(path).Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"토큰 파일 읽기 실패: {ex.Message}");
                }
            }
        }

        public static bool IsTokenLoaded => !string.IsNullOrEmpty(kakaoAccessToken);

        public static async Task SendMessageAsync(string message)
        {
            if (string.IsNullOrEmpty(kakaoAccessToken))
            {
                MessageBox.Show("[Kakao] 토큰이 로드되지 않았습니다. kakao_token.txt 파일을 확인해주세요.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", kakaoAccessToken);

                    // [수정 포인트]
                    // 기존 JSON Body 방식 -> FormUrlEncodedContent 방식으로 변경
                    // 카카오 API는 'template_object'라는 키 값에 JSON 문자열을 담아 보내는 것을 권장합니다.

                    string templateJson = $@"
                    {{
                        ""object_type"": ""text"",
                        ""text"": ""{message}"",
                        ""link"": {{
                            ""web_url"": ""https://www.google.com"",
                            ""mobile_web_url"": ""https://www.google.com""
                        }},
                        ""button_title"": ""앱 열기""
                    }}";

                    var parameters = new Dictionary<string, string>
                    {
                        { "template_object", templateJson }
                    };

                    var content = new FormUrlEncodedContent(parameters);

                    var response = await client.PostAsync(apiUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        // 오류 발생 시 메시지 박스로 상세 내용을 보여줍니다.
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"[카톡 전송 실패]\n상태코드: {response.StatusCode}\n에러내용: {errorMsg}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"[시스템 오류] {ex.Message}");
                }
            }
        }
    }
}