using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Models.Data;
using RestAPI.Models.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly PersonService personService;

        public PersonController(ApplicationContext _db)
        {
            this.db = _db;
            personService = new PersonService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            return Ok(await personService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetById(int id)
        {
            var person = personService.Get(id).Result;
            return person == null? NotFound(new { message = "пользователь не найден"}) : Ok(person); 
        }
        [HttpPost]
        public async Task<ActionResult<Person>> Create([FromBody] Person person)
        {
            if (personService.Create(person))
                return CreatedAtAction(nameof(GetById), new { Id = person.IdPerson}, person);
            return BadRequest();
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<Person>> Update(int id, [FromBody] Person person)
        {
            if (id != person.IdPerson) return BadRequest();
            if(personService.Update(id,person))
                return Ok(person);
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delite(int id)
        {
            if (personService.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
