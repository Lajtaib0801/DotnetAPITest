using DotnetAPITest.Data;
using DotnetAPITest.Dtos.Stock;
using DotnetAPITest.Mappers;
using DotnetAPITest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<StockDto>>> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stocksDto = stocks.Select(x => x.ToStockDto());
            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<ActionResult<StockDto>> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StockDto>> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel is null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
