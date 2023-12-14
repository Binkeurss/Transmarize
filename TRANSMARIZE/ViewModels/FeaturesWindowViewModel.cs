using Avalonia.Input.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarfBuzzSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Svg;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.ViewModels
{
	public partial class FeaturesWindowViewModel : ViewModelBase
	{

        // biến Observerble được sử dụng cho các ComboBox
        [ObservableProperty]
        private static int selectedIndex = 61;

        [ObservableProperty]
        private string myFeature = String.Empty;

        [ObservableProperty]
        private string comboboxText = String.Empty;

        [ObservableProperty]
        private string sourceText = String.Empty;

        [ObservableProperty]
        private string outputText = String.Empty;

        [ObservableProperty]
        private string firstBox = "Source Text";

        [ObservableProperty]
        private string secondBox = String.Empty;

        private string convertInput = String.Empty;

        public FeaturesWindowViewModel() { }

        public void SetFeatureType(string feature)
        {
            MyFeature = feature;
            ComboboxText = MyFeature + " to:";

            string input = String.Copy(ShareData.transText);
            convertInput = input.Replace(System.Environment.NewLine, "");

            if (feature == "Translate")
            {
                FirstBox = "Source Text";
                SecondBox = "Translation";
                SourceText = convertInput;
                convertInput = convertInput.Replace("#", "");
                OutputText = TranslateText(convertInput, "auto", ShareData.langSecond);
            }
            if (feature == "Summarize")
            {
                FirstBox = "Source Text";
                SecondBox = "Summarized Text";
                SourceText = convertInput;
                OutputText = SummarizeText(convertInput);
            }
            if (feature == "Explain")
            {
                FirstBox = "Explaination";
                SecondBox = "Translation";

                string explainText = ExplainText(convertInput);
                explainText = explainText.Replace("\n", "");

                SourceText = explainText;
                explainText = explainText.Replace("#", "");
                OutputText = TranslateText(explainText, "auto", ShareData.langSecond);
            }
        }

        [RelayCommand]
        public void FeatureButton()
        {
            if (MyFeature == "Translate")
            {
                OutputText = TranslateText(convertInput, "auto", ShareData.langSecond);
            }
            if (MyFeature == "Summarize")
            {
                string sumText = String.Copy(OutputText);
                sumText = sumText.Replace("#", "");
                OutputText = TranslateText(sumText, "auto", ShareData.langSecond);
            }
            if (MyFeature == "Explain")
            {
                OutputText = TranslateText(OutputText, "auto", ShareData.langSecond);
            }
        }
       
        // Hàm gọi API google dịch
        public static string TranslateText(string input, string lang_first, string lang_second)
        {
            HttpClient httpClient = new HttpClient();
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

        // API Summarize
        public string SummarizeText(string input)
        {
            var YOUR_API_KEY = "gAAAAABlZErp8klOmJLj0tYZwK9XD3waVoUGOcpfeD94pVkNx1dmpoNTySCyrBfhlN-jaXmWcejKbAzG_pBhTObpujyqUBmMhv-CnA1IDRGCnw-pUwspGwXftUVT66bMDnOx27ctgC0K";
            var client = new RestClient("https://api.textcortex.com/v1");
            var request = new RestRequest("texts/summarizations", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
            //request.AddParameter("application/json", $"{{\n  \"target_lang\": \"en\", \n  \"text\": \"{input}\"\n}}", ParameterType.RequestBody);
            request.AddJsonBody(new { model = "gpt-3.5-turbo-16k", target_lang = "en", text = input });
            //request.AddParameter("application/json", new { target_lang = "en", text = input }, ParameterType.RequestBody);
            var response = client.Execute(request);
            var responseData = JObject.Parse(response.Content);
            string output = responseData["data"]["outputs"][0]["text"].ToString();
            return output;
        }
        // API Explain
        public string ExplainText(string input)
        {
            var YOUR_API_KEY = "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";
            string userInput = "What is " + input;
            var client = new RestClient("https://api.openai.com/v1");
            var request = new RestRequest("engines/text-davinci-003/completions", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
            request.AddJsonBody(new { prompt = userInput, max_tokens = 4000, temperature = 0 });
            var response = client.Execute(request);
            var responseData = JObject.Parse(response.Content);
            string output = responseData["choices"][0]["text"].ToString();
            return output;
        }

        [ObservableProperty]
        public static ObservableCollection<string> languageList = new ObservableCollection<string>
        {
            "Afrikaans",
            "Albanian",
            "Amharic",
            "Arabic",
            "Azerbaijani",
            "Bengali",
            "Bosnian",
            "Catalan",
            "Chinese (Simplified)",
            "Chinese (Traditional)",
            "Croatian",
            "Czech",
            "Danish",
            "Dutch",
            "English",
            "Esperanto",
            "Estonian",
            "Finnish",
            "French",
            "Galician",
            "German",
            "Greek",
            "Gujarati",
            "Hebrew",
            "Hindi",
            "Hungarian",
            "Icelandic",
            "Indonesian",
            "Irish",
            "Italian",
            "Japanese",
            "Kannada",
            "Kazakh",
            "Korean",
            "Latvian",
            "Lithuanian",
            "Macedonian",
            "Malay",
            "Malayalam",
            "Maltese",
            "Marathi",
            "Nepali",
            "Norwegian",
            "Persian",
            "Polish",
            "Portuguese",
            "Punjabi",
            "Romanian",
            "Russian",
            "Serbian",
            "Slovak",
            "Slovenian",
            "Spanish",
            "Swahili",
            "Swedish",
            "Tamil",
            "Telugu",
            "Thai",
            "Turkish",
            "Ukrainian",
            "Urdu",
            "Vietnamese",
            "Welsh",
            "Yiddish",
            "Zulu",
        };
    }
}