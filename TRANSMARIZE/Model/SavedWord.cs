using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TRANSMARIZE.Model
{
    public class SavedWord
    {
        public SavedWord() { }
        public SavedWord(string word)
        {
            Content = word;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
