
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
    public class SaleController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly SaleService saleService;

        public SaleController(ApplicationContext _db)
        {
            this.db = _db;
            saleService = new SaleService(db);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
        {
            return Ok(await saleService.GetAll());
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {
            var sale = saleService.Get(id).Result;
            return sale == null ? NotFound(new { message = "данных о продажах нет" }):Ok(sale);
        }
        [HttpPost]
        public async Task<ActionResult<Sale>> Create([FromBody] Sale sale)
        {
            if (saleService.Create(sale))
                return CreatedAtAction(nameof(GetById), new {Id = sale.IdSales}, sale);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Sale>> Update(int id, [FromBody] Sale sale)
        {
            if (id != sale.IdSales) return BadRequest();
            if (saleService.Update(id, sale))
                return Ok(sale);
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delite(int id)
        {
            if (saleService.Delete(id)) return NoContent();
            return NotFound();
        }
        
    }
    
}
