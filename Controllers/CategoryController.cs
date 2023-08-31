using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }



        [HttpPost]
        public List<Category> PostCategories([FromBody] Category categories) //добавление категории в таблицу
        {
            _context.Categories.Add(categories);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }



        [HttpDelete("{id}")] //удаление категорию из таблицы по ид
        public IActionResult DeleteCategories(int id)
        {
            var categories = _context.Categories.Find(id);

            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            _context.SaveChanges();
            return NoContent();
        }



        [HttpGet("{id}")] //нахождение категории по ид
        public ActionResult<Category> GetCategories(int id)
        {
            var categories = _context.Categories.Find(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        [HttpPut("{id}")] //обновление данных категории в таблице 
        public ActionResult<List<Category>> PutCategories(int id, [FromBody] Category updatedCategories)
        {
            var categories = _context.Categories.Find(id);

            if (categories == null)
            {
                return NotFound();
            }

            categories.Name = updatedCategories.Name;


            _context.Categories.Update(categories);
            _context.SaveChanges();

            return Ok(_context.Categories);
        }
    }
}
