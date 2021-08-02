using System;
using System.Collections.Generic;
using System.Linq;
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

        
        public IEnumerable<Item> Get()
        {
            return items;
        }

        public Item GetItems(Guid id)
        {
            return items.FirstOrDefault(m => m.Id == id);
        }


        public void CreateItem(Item item)
        {
            items.Add(item); 
        }

        public void DeleteItem(Guid id)
        {
            int index=items.FindIndex(existingItem=>existingItem.Id==id);
            items.RemoveAt(index);
        }


        public void UpdateItem(Item item)
        {
            int index=items.FindIndex(existingItem=>existingItem.Id==item.Id);
            items[index]=item; 
        }
    }
}