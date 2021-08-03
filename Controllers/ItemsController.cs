using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using Catalog.Dtos;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<ItemDto>> GetAsync()
        => (await repository.GetAsync()).Select(item=>item.AsDto()); 

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var itemDto=await repository.GetItemsAsync(id);

            if(itemDto is null)
            {
                return NotFound();
            }
            return itemDto.AsDto(); 
        }

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new {id=item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem=await repository.GetItemsAsync(id);

            if(existingItem is null)
            {
                return NotFound(); 
            }

            Item itemToUpdate = existingItem with
            {
                Name=itemDto.Name,
                Price=itemDto.Price
            };

            await repository.UpdateItemAsync(itemToUpdate);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem=await repository.GetItemsAsync(id);

            if(existingItem is null)
            {
                return NotFound(); 
            }

            await repository.DeleteItemAsync(id);
            return NoContent(); 
        }


    }
}