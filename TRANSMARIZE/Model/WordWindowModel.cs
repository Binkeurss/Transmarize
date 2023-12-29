using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRANSMARIZE.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using TRANSMARIZE.ViewModels;
using System.Security;

namespace TRANSMARIZE.Model
{
    public class Def2ex
    {
        public string Meaning { get; set; } = String.Empty;
        public string Example { get; set; } = String.Empty;
        public bool IsHasExample { get; set; } = false;

        public Def2ex() { }

        public Def2ex(string meaning, string example)
        {
            Meaning = meaning;
            Example = example;
        }
    }

    public class Definition
    {
        //Dùng để truy xuất các thuộc tính có trong noun, verb ...
        public string PartOfSpeech { get; set; } = String.Empty;
        public ObservableCollection<Def2ex> Def2exs { get; set; } = new ObservableCollection<Def2ex>();
        public ObservableCollection<string> Synonyms { get; set; } = new ObservableCollection<string>();
        public bool IsHasSynonym { get; set; } = false;
        public ObservableCollection<string> Antonyms { get; set; } = new ObservableCollection<string>();
        public bool IsHasAntonym { get; set; } = false;
    }
}
