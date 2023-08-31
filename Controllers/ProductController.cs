using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProduct()
        {
            var products = _context.Products.ToList();
            return products;
        }



        [HttpPost]
        public List<Product> PostProduct([FromBody] Product products)
        {
            _context.Products.Add(products);
            _context.SaveChanges();
            return _context.Products.ToList();
        }


        //[HttpDelete("{id}")]
        //public List<Product> DeleteProduct(int id)
        //{
        //    var products = _context.Products.Find(id);

        //    if (products == null)
        //    {
        //        return _context.Products.ToList();
        //    }

        //    _context.Products.Remove(products);
        //    _context.SaveChanges();
        //    return _context.Products.ToList();
        //}


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct2(int id)
        {
            var products = _context.Products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            _context.SaveChanges();
            return NoContent();
        }



        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var products = _context.Products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProduct(int id, [FromBody] Product updatedProduct)
        {
            var products = _context.Products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            products.Name = updatedProduct.Name;
            products.Price = updatedProduct.Price;
            products.Image = updatedProduct.Image;
            products.Active = updatedProduct.Active;
            products.Stock = updatedProduct.Stock;
            products.CategoryId = updatedProduct.CategoryId;



            _context.Products.Update(products);
            _context.SaveChanges();

            return Ok(_context.Products);
        }
    }
}
