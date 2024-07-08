using ItemWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly List<Item> items =
        [
            new Item { ItemId = 0010, ItemName = "item1" },
            new Item { ItemId = 0011, ItemName = "item2" },
        ];

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Item>>> GetOrders()
        {
            return items;
        }
        [HttpGet("{itemId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Item>> GetById(int itemId)
        {
            /*Consulta a la base de datos de los items*/
            var item = items.Where(x => x.ItemId == itemId).FirstOrDefault();
            return item;
        }
    }
}
