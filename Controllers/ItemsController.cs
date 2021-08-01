using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repository; 

        public ItemsController()
        {
            repository=new InMemItemsRepository(); 
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        => repository.Get(); 

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            Item item=repository.GetItems(id);

            if(item is null)
            {
                return NotFound();
            }
            return item; 
        }
    }
}