using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TRANSMARIZE.Model
{
    public partial class ShareData : ObservableObject
    {
        // các biến được sử dụng chung bởi các views
        public static string transText = string.Empty;
        public static string langSecond = "vi";

        public static Dictionary<string, string> languageDictionary = new Dictionary<string, string>
        {
            {"Afrikaans", "af"},
            {"Albanian", "sq"},
            {"Amharic", "am"},
            {"Arabic", "ar"},
            {"Azerbaijani", "az"},
            {"Bengali", "bn"},
            {"Bosnian", "bs"},
            {"Catalan", "ca"},
            {"Chinese (Simplified)", "zh-CN"},
            {"Chinese (Traditional)", "zh-TW"},
            {"Croatian", "hr"},
            {"Czech", "cs"},
            {"Danish", "da"},
            {"Dutch", "nl"},
            {"English", "en"},
            {"Esperanto", "eo"},
            {"Estonian", "et"},
            {"Finnish", "fi"},
            {"French", "fr"},
            {"Galician", "gl"},
            {"German", "de"},
            {"Greek", "el"},
            {"Gujarati", "gu"},
            {"Haitian Creole", "ht"},
            {"Hebrew", "he"},
            {"Hindi", "hi"},
            {"Hungarian", "hu"},
            {"Icelandic", "is"},
            {"Indonesian", "id"},
            {"Irish", "ga"},
            {"Italian", "it"},
            {"Japanese", "ja"},
            {"Kannada", "kn"},
            {"Kazakh", "kk"},
            {"Korean", "ko"},
            {"Latvian", "lv"},
            {"Lithuanian", "lt"},
            {"Macedonian", "mk"},
            {"Malay", "ms"},
            {"Malayalam", "ml"},
            {"Maltese", "mt"},
            {"Marathi", "mr"},
            {"Nepali", "ne"},
            {"Norwegian", "no"},
            {"Persian", "fa"},
            {"Polish", "pl"},
            {"Portuguese", "pt"},
            {"Punjabi", "pa"},
            {"Romanian", "ro"},
            {"Russian", "ru"},
            {"Serbian", "sr"},
            {"Slovak", "sk"},
            {"Slovenian", "sl"},
            {"Spanish", "es"},
            {"Swahili", "sw"},
            {"Swedish", "sv"},
            {"Tamil", "ta"},
            {"Telugu", "te"},
            {"Thai", "th"},
            {"Turkish", "tr"},
            {"Ukrainian", "uk"},
            {"Urdu", "ur"},
            {"Vietnamese", "vi"},
            {"Welsh", "cy"},
            {"Yiddish", "yi"},
            {"Zulu", "zu"}
        };

        // 2 biến Observerble được sử dụng cho các ComboBox
        [ObservableProperty]
        private int selectedIndex = 61;

        [ObservableProperty]
        private ObservableCollection<string> languageList = new ObservableCollection<string>
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
    }

}
 