using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CartProduct> GetCartProducts()
        {
            var cartproducts = _context.CartProducts.ToList();
            return cartproducts;
        }



        [HttpPost]
        public List<CartProduct> PostArtikkel([FromBody] CartProduct cartproducts)
        {
            _context.CartProducts.Add(cartproducts);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }


        //[HttpDelete("{id}")]
        //public List<CartProduct> DeleteCartProduct(int id)
        //{
        //    var cartproducts = _context.CartProducts.Find(id);

        //    if (cartproducts == null)
        //    {
        //        return _context.CartProducts.ToList();
        //    }

        //    _context.CartProducts.Remove(cartproducts);
        //    _context.SaveChanges();
        //    return _context.CartProducts.ToList();
        //}


        [HttpDelete("{id}")]
        public IActionResult DeleteCartProduct2(int id)
        {
            var cartproducts = _context.CartProducts.Find(id);

            if (cartproducts == null)
            {
                return NotFound();
            }

            _context.CartProducts.Remove(cartproducts);
            _context.SaveChanges();
            return NoContent();
        }



        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var cartproducts = _context.CartProducts.Find(id);

            if (cartproducts == null)
            {
                return NotFound();
            }

            return cartproducts;
        }

        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCartProduct(int id, [FromBody] CartProduct updatedCartProduct)
        {
            var cartproducts = _context.CartProducts.Find(id);

            if (cartproducts == null)
            {
                return NotFound();
            }

            cartproducts.ProductId = cartproducts.ProductId;
            cartproducts.Quantity = cartproducts.Quantity;

            _context.CartProducts.Update(cartproducts);
            _context.SaveChanges();

            return Ok(_context.CartProducts);
        }
    }
}
