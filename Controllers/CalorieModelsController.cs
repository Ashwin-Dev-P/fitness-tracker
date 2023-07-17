using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;
using System.Security.Claims;

namespace fitt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalorieModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalorieModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CalorieModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieModel>>> GetCalorie()
        {
          if (_context.Calorie == null)
          {
              return NotFound();
          }
            return await _context.Calorie.ToListAsync();
        }

        // GET: api/CalorieModels/MyCalorie
        [HttpGet("MyCalorie")]
        public async Task<ActionResult<IEnumerable<CalorieModel>>> GetMyCalorie()
        {
            if (_context.Calorie == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Calorie.Where(c=>c.ApplicationUserId == userId).OrderBy(c=>c.CalorieConsumptionDate).ToListAsync();
        }

        // GET: api/CalorieModels/MyCalorie/Average
        [HttpGet("MyCalorie/Average")]
        public async Task<ActionResult<double?>> GetMyAverageCalorie()
        {
            if (_context.Calorie == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<int> calorieList = await _context.Calorie.Where(c => c.ApplicationUserId == userId)
                .Select(c => c.CalorieCount)
                .ToListAsync();

            if (calorieList == null || calorieList.Count == 0)
            {
                return 0;
            }
           
            return calorieList.Average();
        }

        // GET: api/CalorieModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalorieModel>> GetCalorieModel(int id)
        {
          if (_context.Calorie == null)
          {
              return NotFound();
          }
            var calorieModel = await _context.Calorie.FindAsync(id);

            if (calorieModel == null)
            {
                return NotFound();
            }

            return calorieModel;
        }

        // PUT: api/CalorieModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalorieModel(int id, CalorieModel calorieModel)
        {
            if (id != calorieModel.CalorieId)
            {
                return BadRequest();
            }

            _context.Entry(calorieModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalorieModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CalorieModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalorieModel>> PostCalorieModel(Dao.CalorieDao.CaloriePostDao calorieModelDao)
        {
          if (_context.Calorie == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Calorie'  is null.");
          }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CalorieModel calorieModel = new CalorieModel()
            {
                CalorieCount = calorieModelDao.CalorieCount,
                CalorieConsumptionDate = calorieModelDao.CalorieConsumptionDate,
                ApplicationUserId = userId
            };
            _context.Calorie.Add(calorieModel);
            await _context.SaveChangesAsync();

            return Ok("Calorie tracked");
        }

        // DELETE: api/CalorieModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalorieModel(int id)
        {
            if (_context.Calorie == null)
            {
                return NotFound();
            }
            var calorieModel = await _context.Calorie.FindAsync(id);
            if (calorieModel == null)
            {
                return NotFound();
            }

            _context.Calorie.Remove(calorieModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalorieModelExists(int id)
        {
            return (_context.Calorie?.Any(e => e.CalorieId == id)).GetValueOrDefault();
        }
    }
}
