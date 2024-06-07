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

        [HttpPut("{id}")]
        public ActionResult<StockDto> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel is null)
            {
                return NotFound();
            }

            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel is null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
