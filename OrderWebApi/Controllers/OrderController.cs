using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PruebaContext _context;
        public OrderController(PruebaContext pruebaContext)
        {
            _context = pruebaContext;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrders()
        {
            return _context.Ordens.ToList();
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Orden>> GetById(int orderId)
        {
            /*Consulta a la base de datos de las órdenes*/
            var order = _context.Ordens.ToList().Where(x=> x.IdOrden == orderId).FirstOrDefault();
            return order;
        }
    }
}
