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
//using System.Speech.Synthesis;

namespace TRANSMARIZE.ViewModels
{
    public partial class WordTransViewModel : ViewModelBase
    {
        private HttpClient httpClient = new HttpClient();
        //private SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        // biến savedword có giá trị khi từ đang tra đã được lưu vào WordBook
        public SavedWord findedWord = new SavedWord();

        [ObservableProperty]
        public string buttonContent = "+ Add"; // Nội dung của Button thay đổi tùy tình huống

        [ObservableProperty]
        public string buttonColor = "#00A9FF"; // Màu sắc của Button thay đổi tùy tình huống

        [ObservableProperty]
        public string word = "empty";

        [ObservableProperty]
        public string sound = "default";

        [ObservableProperty]
        public bool ishassound = false;

        [ObservableProperty]
        public string phonetic = "Loading";

        [ObservableProperty]
        public bool isComplete = false;

        [ObservableProperty]
        public ObservableCollection<Definition> definitions = new ObservableCollection<Definition>();
    
        public WordTransViewModel() 
        {
            string input = ConvertString(ShareData.transText);
            Word = input;
            TranslateWord(input);
            // Tra cứu từ đang dịch đã có trong WordBook chưa
            Task<List<SavedWord>> findedList = App.WordBookDatabase.GetaWord(input);
            // Nếu có thì đổi trạng thái của Button và gán findedWord bằng từ đã tìm thấy
            if (findedList.Result.Count > 0)
            {
                findedWord = findedList.Result[0];
                ButtonColor = "#65B741";
                ButtonContent = "Added";
            }
        }
        // Phương thức khởi tạo này được gọi từ SavedWordWindow
        // Vì từ muốn tra đã có trong WordBook nên ko cần tra lại nữa
        // Giá trị của findedWord sẽ được truyền vào từ SavedWordWindow
        public WordTransViewModel(SavedWord selectedWord)
        {
            findedWord = selectedWord;
            Word = selectedWord.Content;
            TranslateWord(selectedWord.Content);
            ButtonContent = "Added";
            ButtonColor = "#65B741";
        }

        //Dùng để bỏ dấu trắng
        string ConvertString(string input)
        {
            string convertInput = input.Replace(" ", "");
            convertInput = convertInput.ToLower();
            return convertInput;
        }

        public async void TranslateWord(string input)
        {
            //Tạo link để gọi API
            string url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + input;
            //Gọi API là lấy kết quả trả về là file json dạng string
            //string result = httpClient.GetStringAsync(url).Result;
            string result = String.Empty;
            Task<HttpResponseMessage> task = httpClient.GetAsync(url);
            HttpResponseMessage response = await task;
            if (task.IsCompleted == true)
            {
                IsComplete = true;
            }
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                Word = input;
                Phonetic = "This word doesn't exist in database";
                return;
            }
            JArray jsonArray = JArray.Parse(result);
            foreach (JToken entry in jsonArray)
            {
                // lấy word 
                Word = entry["word"].ToString();
                // lấy cách đọc
                if (entry["phonetic"] != null)
                {
                    Phonetic = entry["phonetic"].ToString();                  
                }
                // lấy file audio
                JToken phonetics = entry["phonetics"];
                // ishassound luôn luôn true do có API đọc text rồi
                Ishassound = true;
                foreach (JToken phonetic in phonetics)
                {
                    // lấy thêm cách đọc
                    if (phonetic["text"] != null && phonetic["text"].ToString() != "")
                    {
                        Phonetic = phonetic["text"].ToString();
                    }
                    // lấy đường dẫn audio
                    if (phonetic["audio"] != null && phonetic["audio"].ToString() != "")
                    {
                        Sound = phonetic["audio"].ToString();
                        // Ishassound = true;
                        // break;
                    }
                }

                // lấy phần dịch nghĩa
                JToken meanings = entry["meanings"]; 
                foreach(JToken meaning in meanings)
                {
                    // mỗi item là 1 loại từ
                    Definition item = new Definition();

                    // lấy loại từ
                    item.PartOfSpeech = meaning["partOfSpeech"].ToString();

                    // lấy danh sách các định nghĩa và ví dụ
                    JToken definitions = meaning["definitions"];
                    foreach(JToken definition in definitions)
                    {
                        Def2ex def2ex = new Def2ex();
                        def2ex.Meaning = "Definition: " + definition["definition"].ToString();
                        if (definition["example"] != null)
                        {
                            def2ex.Example = "Example: " + definition["example"].ToString();
                            def2ex.IsHasExample = true;
                        }
                        else
                        {
                            def2ex.Example = "Example: " + "nan";
                        }
                        item.Def2exs.Add(def2ex);
                    }

                    // lấy từ đồng nghĩa và trái nghĩa
                    JToken synonyms = meaning["synonyms"];
                    foreach (JToken synonym in synonyms)
                    {
                        item.IsHasSynonym = true;
                        item.Synonyms.Add("• " + synonym.ToString());
                    }
                    JToken antonyms = meaning["antonyms"];
                    foreach (JToken antonym in antonyms)
                    {
                        item.IsHasAntonym = true;
                        item.Antonyms.Add("• " + antonym.ToString());
                    }

                    Definitions.Add(item);
                }
            }
            if (Phonetic == "Loading")
            {
                Phonetic = "nan";
            }
        }

        [RelayCommand]
        public void AudioPlay()
        {
            /*            var libvlc = new LibVLC(enableDebugLogs: true);
                        var media = new Media(libvlc, new Uri(Sound));
                        var mediaplayer = new MediaPlayer(media);
                        mediaplayer.Play();*/

            //synthesizer.SpeakAsync(Word);
        }

        [RelayCommand]
        public async void AddRemoveWord()
        {
            // Nếu từ đang tra chưa có trong WordBook thì Button này sẽ là lưu từ
            if (findedWord.Content == String.Empty)
            {
                SavedWord newWord = new SavedWord(Word, Phonetic);
                var r = App.WordBookDatabase.SaveWordAsync(newWord);
                findedWord = newWord;
                ButtonContent = "Added";
                ButtonColor = "#65B741";
            }
            else // Nếu từ đang tra đã có trong WordBook thì Button này là bỏ từ
            {
                await App.WordBookDatabase.DeleteTask(findedWord);
                findedWord.Content = String.Empty;
                ButtonContent = "+ Add";
                ButtonColor = "#00A9FF";
            }
        }
    }
}
