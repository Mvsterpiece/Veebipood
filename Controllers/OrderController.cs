using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Order> GetOrder()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }



        [HttpPost]
        public List<Order> PostOrder([FromBody] Order orders)
        {
            _context.Orders.Add(orders);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }


        //[HttpDelete("{id}")]
        //public List<Order> DeleteOrder(int id)
        //{
        //    var orders = _context.Orders.Find(id);

        //    if (orders == null)
        //    {
        //        return _context.Orders.ToList();
        //    }

        //    _context.Orders.Remove(orders);
        //    _context.SaveChanges();
        //    return _context.Orders.ToList();
        //}


        [HttpDelete("/wasd/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            _context.SaveChanges();
            return NoContent();
        }



        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            orders.created = updatedOrder.created;
            orders.TotalSum = updatedOrder.TotalSum;
            orders.Paid = updatedOrder.Paid;
            orders.CartProduct = updatedOrder.CartProduct;
            orders.Person = updatedOrder.Person;

            _context.Orders.Update(orders);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }

    }
}
