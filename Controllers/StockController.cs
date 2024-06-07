using DotnetAPITest.Data;
using DotnetAPITest.Dtos.Stock;
using DotnetAPITest.Mappers;
using DotnetAPITest.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPITest.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StockDto>> GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(x => x.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public ActionResult<StockDto> GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public ActionResult<StockDto> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
    }
}
