
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Models.Data;
using RestAPI.Models.Services;
namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Sale>> Get(int id)
        {
            var sale = saleService.Get(id);
            return sale == null ? NotFound(new { message = "данных о продажах нет" }):Ok(sale);
        }
        [HttpPost]
        public async Task<ActionResult<Sale>> Create([FromBody] Sale sale)
        {
            if (saleService.Create(sale))
                return CreatedAtAction(nameof(Get), new {Id = sale.IdSales}, sale);
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Sale>> Update(int id, [FromBody] Sale sale)
        {
            if (id != sale.IdSales) return BadRequest();
            if (saleService.Update(id, sale))
                return Ok(sale);
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delite(int id)
        {
            if (saleService.Delete(id)) return NoContent();
            return BadRequest();
        }
        
    }
    
}
