using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class TranslateWindow : Window
    {
        private HttpClient httpClient;
        public TranslateWindow()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            string input = ConvertString(ShareData.transText);
            SourceText.Text = input;
            TransText.Text = TranslateText(input, "auto", ShareData.langSecond);
        }

        //Call API
        public string TranslateText(string input, string lang_first, string lang_second)
        {
            // tạo link để gọi API
            string url = String.Format
           ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
            lang_first, lang_second, Uri.EscapeUriString(input));
            // gọi API và lấy kết quả trả về
            string result = httpClient.GetStringAsync(url).Result;
            // trích xuất thông tin từ kiểu dữ liệu json được trả về
            var jsonData = JsonConvert.DeserializeObject<List<dynamic>>(result);
            var translationItems = jsonData[0];
            string translation = "";
            foreach (object item in translationItems)
            {
                IEnumerable<dynamic> translationLineObject = item as IEnumerable<dynamic>;
                IEnumerator<dynamic> translationLineString = translationLineObject.GetEnumerator();
                translationLineString.MoveNext();
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }
            if (translation.Length > 1) { translation = translation.Substring(1); };
            return translation;
        }

        //Bỏ đi những dấu xuống dòng tránh bị lỗi
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }

        public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //Environment.Exit(0);
            this.Close();
        }

        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
