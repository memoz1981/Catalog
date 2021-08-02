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

        [HttpPost]
        public ActionResult<Item> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new {id=item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem=repository.GetItems(id);

            if(existingItem is null)
            {
                return NotFound(); 
            }

            Item itemToUpdate = existingItem with
            {
                Name=itemDto.Name,
                Price=itemDto.Price
            };

            repository.UpdateItem(itemToUpdate);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem=repository.GetItems(id);

            if(existingItem is null)
            {
                return NotFound(); 
            }

            repository.DeleteItem(id);
            return NoContent(); 
        }


    }
}