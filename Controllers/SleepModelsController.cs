using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using fitt.Dao.SleepModelDao;
using Microsoft.OpenApi.Any;

namespace fitt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SleepModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SleepModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SleepModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SleepModel>>> GetSleep()
        {
          if (_context.Sleep == null)
          {
              return NotFound();
          }
            return await _context.Sleep.ToListAsync();
        }

        // GET: api/SleepModels/MySleep
        [HttpGet("MySleep")]
        public async Task<ActionResult<IEnumerable<SleepModelGetDao>?>> GetMySleep()
        {
            if (_context.Sleep == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Sleep
                .Where(s=>s.ApplicationUserId == userId)
                .Select(s=>new SleepModelGetDao {
                    SleepId = s.SleepId,
                    SleepDuration = s.SleepDuration,
                    SleepDate = s.SleepDate,
                })
                .OrderBy(s=>s.SleepDate)
                .ToListAsync();
        }

        // GET: api/SleepModels/MySleep/Average
        [HttpGet("MySleep/Average")]
        public async Task<ActionResult<double>?> GetMyAverageSleepDuration()
        {
            if (_context.Sleep == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<int> sleepDurationArray = await _context.Sleep.Where(s => s.ApplicationUserId == userId).Select(s => ((s.SleepDuration.Hours * 60) + s.SleepDuration.Minutes)).ToListAsync();


            if ( sleepDurationArray == null || sleepDurationArray.Count == 0)
            {
                   return NotFound() ;
            }
            else
            {
                return sleepDurationArray.Average();
            }

            

            // Return average sleep duration of an user in minutes


        }

        // GET: api/SleepModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SleepModel>> GetSleepModel(int id)
        {
          if (_context.Sleep == null)
          {
              return NotFound();
          }
            var sleepModel = await _context.Sleep.FindAsync(id);

            if (sleepModel == null)
            {
                return NotFound();
            }

            return sleepModel;
        }

        // PUT: api/SleepModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSleepModel(int id, SleepModel sleepModel)
        {
            if (id != sleepModel.SleepId)
            {
                return BadRequest();
            }

            _context.Entry(sleepModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SleepModelExists(id))
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

        // POST: api/SleepModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SleepModel>> PostSleepModel( SleepPostModelDao sleepModelDao)
        {
          if (_context.Sleep == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Sleep'  is null.");
          }
            Console.WriteLine(sleepModelDao.SleepDuration);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            SleepModel sleepModel = new SleepModel()
            {
                SleepDate = sleepModelDao.SleepDate,
                SleepDuration = sleepModelDao.SleepDuration,
                ApplicationUserId = userId
            };

            _context.Sleep.Add(sleepModel);
            await _context.SaveChangesAsync();

            return Ok("Sleep data added");
        }

        // DELETE: api/SleepModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSleepModel(int id)
        {
            if (_context.Sleep == null)
            {
                return NotFound();
            }
            var sleepModel = await _context.Sleep.FindAsync(id);
            if (sleepModel == null)
            {
                return NotFound();
            }

            _context.Sleep.Remove(sleepModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SleepModelExists(int id)
        {
            return (_context.Sleep?.Any(e => e.SleepId == id)).GetValueOrDefault();
        }
    }
}
