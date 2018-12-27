using Phone.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Services
{
    public class DeviceLocalDbService
    {
        readonly SQLiteAsyncConnection database;

        public DeviceLocalDbService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserLoginSettings>().Wait();
        }

        public DeviceLocalDbService()
        {
        }

        public Task<List<UserLoginSettings>> GetItemsAsync()
        {
            return database.Table<UserLoginSettings>().ToListAsync();
        }

        public Task<List<UserLoginSettings>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<UserLoginSettings>("SELECT * FROM [UserLoginSettings] WHERE [Email] = ''");
        }

        public Task<UserLoginSettings> GetItemAsync(int id)
        {
            return database.Table<UserLoginSettings>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public bool GetFitsItemAsync()
        {
            bool retVal = true;
            //var result = database.Table<UserLoginSettings>().ToListAsync();
            // if (result == null)
            //   retVal = false;
            var s = database.ExecuteAsync("SELECT count(*) FROM [UserLoginSettings]");
            


            return retVal;
        }

        public Task<int> SaveItemAsync(UserLoginSettings item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(UserLoginSettings item)
        {
            return database.DeleteAsync(item);
        }
    }
}
