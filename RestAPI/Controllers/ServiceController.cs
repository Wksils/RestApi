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
    public class ServiceController:ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly ServiceService serviceService;

        public ServiceController(ApplicationContext _db)
        {
            this.db = _db;
            serviceService = new ServiceService(db);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            return Ok(await serviceService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetById(int id)
        {
            var service = serviceService.Get(id).Result;
            return service == null ? NotFound(new {message = "нет данных об обслуживании"}) : Ok(service);
        }
        [HttpPost]
        public async Task<ActionResult<Service>> Create([FromBody] Service service)
        {
            if (serviceService.Create(service)) 
                return CreatedAtAction(nameof(GetById), new { Id = service.IdService }, service);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> Update(int id, [FromBody] Service service)
        {
            if (id != service.IdService) return BadRequest();
            if (serviceService.Update(id, service)) return Ok(service);
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delite(int id)
        {
            if (serviceService.Delete(id)) return NoContent();
            return NotFound();
        }

    }
}
