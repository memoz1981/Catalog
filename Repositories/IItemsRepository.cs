using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
        IEnumerable<Item> Get();
        Item GetItems(Guid id);

        void CreateItem(Item item);

        void UpdateItem(Item item); 

        void DeleteItem(Guid id); 
    }
}