using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phone.Services
{
    public interface IDataStore<UserLoginSettings>
    {
        Task<bool> AddItemAsync(UserLoginSettings item);
        Task<bool> UpdateItemAsync(UserLoginSettings item);
        Task<bool> DeleteItemAsync(string id);
        Task<UserLoginSettings> GetItemAsync(string id);
        Task<IEnumerable<UserLoginSettings>> GetItemsAsync(bool forceRefresh = false);
    }
}
