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
        public static string currentText = string.Empty;

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
    }
}
 