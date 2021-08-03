using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAsync();
        Task<Item> GetItemsAsync(Guid id);

        Task CreateItemAsync(Item item);

        Task UpdateItemAsync(Item item); 

        Task DeleteItemAsync(Guid id); 
    }
}