using Catalog.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catlog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> _items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze Sword", "Deals a small amount of damage", 5, DateTimeOffset.UtcNow),
        };

        // GET /items
        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return _items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = _items.SingleOrDefault(item => item.Id == id);

            return item ?? (ActionResult<ItemDto>)NotFound();
        }

        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            _items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = _items.SingleOrDefault(item => item.Id == id);

            if (existingItem is null) return NotFound();

            var updateItem = existingItem with {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = _items.FindIndex(existingItem => existingItem.Id == id);
            _items[index] = updateItem;

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == id);

            if (index < 0) return NotFound();

            _items.RemoveAt(index);

            return NoContent();
        }
    }
}