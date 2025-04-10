﻿using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarfBuzzSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using TRANSMARIZE.Model;
using System.Threading.Tasks;
using Avalonia.Controls.Templates;
//using System.Speech.Synthesis;

namespace TRANSMARIZE.ViewModels
{
	public partial class FeaturesViewModel : ViewModelBase
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

        [ObservableProperty]
        private bool isComplete1 = false;

        [ObservableProperty]
        private bool isComplete2 = false;


        private string convertInput = String.Empty;

        public FeaturesViewModel() { }

        public async void SetFeatureType(string feature)
        {
            IsComplete1 = true;
            IsComplete2 = false;
            MyFeature = feature;
            ComboboxText = MyFeature + " to:";
            OutputText = " ";

            string input = String.Copy(ShareData.transText);
            convertInput = input.Replace(System.Environment.NewLine, "");

            if (feature == "Translate")
            {
                FirstBox = "Source Text";
                SecondBox = "Translation";
                SourceText = convertInput;
                convertInput = convertInput.Replace("#", "").Replace("&"," ");
                Task<string> task = TranslateText(convertInput, "auto", ShareData.langSecond);
                OutputText = await task;
                if (task.IsCompleted == true)
                {
                    IsComplete2 = true;
                }
            }
            if (feature == "Summarize")
            {
                FirstBox = "Source Text";
                SecondBox = "Summarized Text";
                SourceText = convertInput;
                Task<string> task = SummarizeText(convertInput);
                OutputText = await task;
                if (task.IsCompleted == true)
                {
                    IsComplete2 = true;
                }
            }
            if (feature == "Explain")
            {
                IsComplete1 = false;
                FirstBox = "Explaination";
                SecondBox = "Translation";
                SourceText = " ";

                Task<string> task1 = ExplainText(convertInput);
                string explainText = await task1;
                if (task1.IsCompleted == true)
                {
                    IsComplete1 = true;
                }
                explainText = explainText.Replace("\n", "");
                SourceText = explainText;

                explainText = explainText.Replace("#", "").Replace("&"," ");
                Task<string> task2 = TranslateText(explainText, "auto", ShareData.langSecond);
                OutputText = await task2;
                if (task2.IsCompleted == true)
                {
                    IsComplete2 = true;
                }
            }
        }

        [RelayCommand]
        public async void FeatureButton()
        {
            if (MyFeature == "Translate")
            {
                Task<string> task = TranslateText(SourceText, "auto", ShareData.langSecond);
                OutputText = await task;
            }
            if (MyFeature == "Summarize")
            {
                string sumText = String.Copy(OutputText);
                sumText = sumText.Replace("#", "");
                Task<string> task = TranslateText(sumText, "auto", ShareData.langSecond);
                OutputText = await task;
            }
            if (MyFeature == "Explain")
            {
                Task<string> task = TranslateText(SourceText, "auto", ShareData.langSecond);
                OutputText = await task;
            }
        }

        // Hàm gọi API google dịch
        public async Task<string> TranslateText(string input, string lang_first, string lang_second)
        {
            HttpClient httpClient = new HttpClient();
            // tạo link để gọi API
            string url = String.Format
           ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
            lang_first, lang_second, Uri.EscapeUriString(input));
            // gọi API và lấy kết quả trả về
            string result = await httpClient.GetStringAsync(url);
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
        public async Task<string> SummarizeText(string input)
        {
            var YOUR_API_KEY = "gAAAAABlgVumaDZSd20dwtQdCzzxe-6i_a_6z4jyWl533NgMjeMgWpQlnqJQP_HVx_9K0xSw4ZBYKUWdm8MlJVh5up8pTrNAyk07YNCi7blu9ThB_xww1KQWsPPavNdNX7Ki-8kiJCKv";
            var client = new RestClient("https://api.textcortex.com/v1");
            var request = new RestRequest("texts/summarizations", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
            //request.AddParameter("application/json", $"{{\n  \"target_lang\": \"en\", \n  \"text\": \"{input}\"\n}}", ParameterType.RequestBody);
            request.AddJsonBody(new { model = "gpt-3.5-turbo-16k", target_lang = "en", text = input });
            //request.AddParameter("application/json", new { target_lang = "en", text = input }, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);
            var responseData = JObject.Parse(response.Content);
            string output = responseData["data"]["outputs"][0]["text"].ToString();
            return output;
        }
        // API Explain
        public async Task<string> ExplainText(string input)
        {
            var YOUR_API_KEY = "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";
            string userInput = "What is " + input;
            var client = new RestClient("https://api.openai.com/v1");
            var request = new RestRequest("engines/text-davinci-003/completions", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
            request.AddJsonBody(new { prompt = userInput, max_tokens = 4000, temperature = 0 });
            RestResponse response = await client.ExecuteAsync(request);
            var responseData = JObject.Parse(response.Content);
            string output = responseData["choices"][0]["text"].ToString();
            return output;
        }

        [ObservableProperty]
        private bool isStartUp = ShareData.GetBool(ShareData.settingPath);

        [RelayCommand]
        public void StartUp()
        {
/*            if (IsStartUp == true)
            {
                ShareData.SetToStartup(true);
                ShareData.SaveBool(ShareData.settingPath, true);
            }
            if (IsStartUp == false)
            {
                ShareData.SetToStartup(false);
                ShareData.SaveBool(ShareData.settingPath, false);
            }*/
        }

        //SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        [RelayCommand]
        public void ReadWord1()
        {
/*            if (synthesizer.State == SynthesizerState.Speaking)
            {
                synthesizer.Pause();
            }
            else
            {
                synthesizer = new SpeechSynthesizer();
                synthesizer.SpeakAsync(SourceText);
            }*/
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