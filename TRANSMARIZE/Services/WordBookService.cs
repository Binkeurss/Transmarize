﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Services
{
    public class WordBookService
    {
        readonly SQLiteAsyncConnection database;

        // Phương thức khởi tạo CSDL
        // Đầu vào là đường dẫn tới database muốn tạo
        public WordBookService(string dbPath)
        {
            // Tạo một file database .db tại đường dẫn được đưa vào 
            database = new SQLiteAsyncConnection(dbPath);
            // Trong database đó, tạo một table dựa trên class SavedWord
            database.CreateTableAsync<SavedWord>().Wait();
        }
        public Task<List<SavedWord>> GetWordsAsync()
        {
            return database.Table<SavedWord>().ToListAsync();
        }
        public Task<int> SaveWordAsync(SavedWord word)
        {
            return database.InsertAsync(word);
        }
        public async Task DeleteTask(SavedWord savedWord)
        {
            await database.DeleteAsync(savedWord);
        }
        public Task<List<SavedWord>> GetaWord(string word)
        {
            var query = database.Table<SavedWord>().Where(s => s.Content == word);
            return query.ToListAsync();
        }
    }
}
