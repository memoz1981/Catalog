using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        
        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemsAsync(Guid id)
        {
            
            return await Task.FromResult(items.FirstOrDefault(m => m.Id == id));
        }


        public async Task CreateItemAsync(Item item)
        {
            items.Add(item); 
            await Task.CompletedTask; 
        }

        public async Task DeleteItemAsync(Guid id)
        {
            int index=items.FindIndex(existingItem=>existingItem.Id==id);
            items.RemoveAt(index);
            await Task.CompletedTask; 
        }


        public async Task UpdateItemAsync(Item item)
        {
            int index=items.FindIndex(existingItem=>existingItem.Id==item.Id);
            items[index]=item; 
            await Task.CompletedTask; 
        }
    }
}