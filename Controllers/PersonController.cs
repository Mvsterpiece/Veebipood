using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Person> GetPersons()
        {
            var persons = _context.Persons.ToList();
            return persons;
        }



        [HttpPost]
        public List<Person> PostPersons([FromBody] Person inimest) //добавление человека в таблицу
        {
            _context.Persons.Add(inimest);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }


        //[HttpDelete("{id}")] //удаление человка из таблицы по ид
        //public List<Person> DeletePersons(int id)
        //{
        //    var inimest = _context.Persons.Find(id);

        //    if (inimest == null)
        //    {
        //        return _context.Persons.ToList();
        //    }

        //    _context.Persons.Remove(inimest);
        //    _context.SaveChanges();
        //    return _context.Persons.ToList();
        //}


        [HttpDelete("{id}")] //удаление человка из таблицы по ид
        public IActionResult DeletePersons(int id)
        {
            var inimest = _context.Persons.Find(id);

            if (inimest == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(inimest);
            _context.SaveChanges();
            return NoContent();
        }



        [HttpGet("{id}")] //нахождение человка по ид
        public ActionResult<Person> GetPersons(int id)
        {
            var inimest = _context.Persons.Find(id);

            if (inimest == null)
            {
                return NotFound();
            }

            return inimest;
        }

        [HttpPut("{id}")] //обновление данных человека в таблице 
        public ActionResult<List<Person>> PutPersons(int id, [FromBody] Person updatedInimest)
        {
            var inimest = _context.Persons.Find(id);

            if (inimest == null)
            {
                return NotFound();
            }

            inimest.PersonCode = updatedInimest.PersonCode;
            inimest.FirstName = updatedInimest.FirstName;
            inimest.LastName = updatedInimest.LastName;
            inimest.Phone = updatedInimest.Phone;
            inimest.Address = updatedInimest.Address;
            inimest.Password = updatedInimest.Password;
            inimest.Admin = updatedInimest.Admin;


            _context.Persons.Update(inimest);
            _context.SaveChanges();

            return Ok(_context.Persons);
        }
    }
}
