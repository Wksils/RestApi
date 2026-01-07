using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Models.Data;
using RestAPI.Models.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodController: ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly GoodService service;

        public GoodController(ApplicationContext _db)
        {
            db = _db;
            service = new GoodService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Good>>> GetAll()
        {
            return Ok(await service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Good>> Get(int id)
        {
            var good=service.Get(id);
            return good == null ?NotFound(new { message = "товар не найден" }) : Ok(good);
        }
        [HttpPost]
        public async Task<ActionResult<Good>> Create([FromBody] Good good)
        {
            if(service.Create(good))
                return CreatedAtAction(nameof(Get), new { Id = good.IdProduct }, good);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Good>> Update(int id,[FromBody] Good good)
        {
            if (good.IdProduct != id) return BadRequest();
            if (service.Update(id,good))
                return Ok(good);
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(service.Delete(id))
                 return NoContent();
            return BadRequest();
        }
    }
}
