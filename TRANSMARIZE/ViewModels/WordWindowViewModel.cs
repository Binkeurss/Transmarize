using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRANSMARIZE.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;

namespace TRANSMARIZE.ViewModels
{
    public partial class WordWindowViewModel : ViewModelBase
    {
        private HttpClient httpClient = new HttpClient();

        [ObservableProperty]
        public string sound = "default";

        [ObservableProperty]
        public string word = "empty";

        [ObservableProperty]
        public ObservableCollection<Definition> definitions = new ObservableCollection<Definition>();
    
        public WordWindowViewModel() 
        {
            string input = ConvertString(ShareData.transText);
        }

        //Dùng để bỏ dấu xuống dòng
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }

        public void TranslateWord(string input)
        {
            //Tạo link để gọi API
            string url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + input;
            //Gọi API là lấy kết quả trả về là file json dạng string
            string result = httpClient.GetStringAsync(url).Result;
            JArray jsonArray = JArray.Parse(result);
            foreach (JToken entry in jsonArray)
            {
                Word = entry["word"].ToString();
                JToken meanings = entry["meanings"]; 
                foreach(JToken meaning in meanings)
                {
                    Definition item = new Definition();
                    item.PartOfSpeech = meaning["partOfSpeech"].ToString();

                    JToken definitions = meaning["definitions"].ToString();
                    foreach(JToken definition in definitions)
                    {
                        Def2ex def2ex = new Def2ex();
                        def2ex.Meaning = "Definition: " + definition["definition"].ToString();
                        if (definition["example"] != null)
                        {
                            def2ex.Example = "Example: " + definition["definition"].ToString() + "\n";
                            def2ex.IsHasExample = true;
                        }
                        else
                        {
                            def2ex.Example = "Example: " + "nan";
                        }
                        item.Def2exs.Add(def2ex);
                    }
                    Definitions.Add(item);
                }

                JToken phonetics = entry["phonetics"];
                foreach (JToken phonetic in phonetics)
                {
                    if (phonetic["audio"].ToString() != "")
                    {
                        Sound = phonetic["audio"].ToString();
                        break;
                    }
                }
            }
        }

        [RelayCommand]
        public void AudioPlay()
        {
            var libvlc = new LibVLC(enableDebugLogs: true);
            var media = new Media(libvlc, new Uri(Sound));
            var mediaplayer = new MediaPlayer(media);
            mediaplayer.Play();
        }
    }
}
