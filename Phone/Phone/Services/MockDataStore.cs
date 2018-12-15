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
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "First item", Message="This is an item description." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "Second item", Message="This is an item description." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "Third item", Message="This is an item description." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Message="This is an item description." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Message="This is an item description." },
                new AlertModel { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Message="This is an item description." },
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