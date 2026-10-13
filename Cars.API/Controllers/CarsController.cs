using Cars.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Cars.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cars.API.Controllers
{
    public class CarsController : BaseApiController
    {
        private readonly DataContext _context;

        public CarsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            return await _context.Cars.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            var car = await _context.Cars.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (car is null) return NotFound();

            return car;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCar([FromBody] Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            var car = await _context.Cars.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (car is null) return NotFound();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
