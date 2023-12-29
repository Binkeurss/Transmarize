using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;


namespace TRANSMARIZE.Model
{
    public class SavedWord
    {
        public SavedWord() { }
        public SavedWord(string word, string phonetic)
        {
            Content = word;
            Phonetic = phonetic;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Phonetic { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
