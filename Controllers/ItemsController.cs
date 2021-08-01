using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using Catalog.Dtos;
using System.Linq;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository; 

        public ItemsController(IItemsRepository repository)
        {
            this.repository=repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        => repository.Get().Select(item=>item.AsDto()); 

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            ItemDto itemDto=repository.GetItems(id).AsDto();

            if(itemDto is null)
            {
                return NotFound();
            }
            return itemDto; 
        }
    }
}