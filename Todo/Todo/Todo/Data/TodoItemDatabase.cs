using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using SQLite;
using Todo.Models;

namespace Todo.Data
{
    public class TodoItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () =>
        {
            var instance = new TodoItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<TodoItem>();
            return instance;
        });

        public TodoItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            try
            {
                if(item.Notes.Contains("0"))
                {
                    throw new Exception("Notes field contains a 0. An exception will be thrown here.");
                }
                if (item.Notes.Contains("1"))
                {
                    throw new Exception("Notes field contains a 1. An exception will be thrown here and a crash will also be generated.");
                }
                if (item.ID != 0)
                {
                    return Database.UpdateAsync(item);
                }
                else
                {
                    return Database.InsertAsync(item);
                }
            }
            catch(Exception exception)
            {
                var properties = new Dictionary<string, string> {
                    { "Notes", item.Notes }
                  };
                Crashes.TrackError(exception, properties);
                if (item.Notes.Contains("1"))
                {
                    Crashes.GenerateTestCrash();
                }
                //throw exception;
            }
            return new Task<int>(() => 0);
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
