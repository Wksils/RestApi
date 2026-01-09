using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RestAPI.Models;
using RestAPI.Models.Data;
using RestAPI.Models.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendingMachineController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly VendingMachineService machineService;

        public VendingMachineController(ApplicationContext _db)
        {
            this.db = _db;
            machineService = new VendingMachineService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendingMachine>>> GetAll()
        {
            return Ok(await machineService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<VendingMachine>> GetById(int id)
        {
            var vendingMachine = machineService.Get(id).Result;
            return vendingMachine == null? NotFound(new {message = "Вендинговый аппарат не найден"}) : Ok(vendingMachine);
        }
        [HttpPost]
        public async Task<ActionResult<VendingMachine>> Create([FromBody] VendingMachine vendingMachine)
        {
            if (machineService.Create(vendingMachine))
                return CreatedAtAction(nameof(GetById), new { Id = vendingMachine.IdVm }, vendingMachine);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<VendingMachine>> Update(int id, [FromBody] VendingMachine vendingMachine)
        {
            if (id != vendingMachine.IdVm) return BadRequest();
            if(machineService.Update(id, vendingMachine)) return Ok(vendingMachine);
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delite(int id)
        {
            if (machineService.Delete(id)) return NoContent();
            return NotFound();
        }
    }
}
