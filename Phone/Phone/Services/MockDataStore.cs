using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phone.Models;

namespace Phone.Services
{
    public class MockDataStore : IDataStore<AlertModel>
    {
        List<AlertModel> items;

        public MockDataStore()
        {
            items = new List<AlertModel>();
            var mockItems = new List<AlertModel>
            {
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "1-2 Feet", Message="Range Changed to 1-2 Feet." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "2-3 Feet", Message="Range Changed to 2-3 Feet." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "3-4 Feet", Message="Range Changed to 3-4 Feet." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "4-5 Feet", Message="Range Changed to 4-5 Feet." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "5-6 Feet", Message="Range Changed to 5-6 Feet." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "6-7 Feet", Message="Range Changed to 6-7 Feet." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(AlertModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(AlertModel item)
        {
            var oldItem = items.Where((AlertModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((AlertModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<AlertModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<AlertModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}