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
using fitt.Dao.BodyWeightsDao;

namespace fitt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BodyWeightModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BodyWeightModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BodyWeightModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodyWeightModel>>> GetBodyWeight()
        {
          if (_context.BodyWeight == null)
          {
              return NotFound();
          }
            return await _context.BodyWeight.ToListAsync();
        }

        // GET: api/BodyWeightModels
        [HttpGet("MyWeights")]
        public async Task<ActionResult<IEnumerable<BodyWeightModel>>> GetMyBodyWeight()
        {
            if (_context.BodyWeight == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.BodyWeight.Where(bwOwject=>bwOwject.ApplicationUserId == userId).ToListAsync();
        }

        // GET: api/BodyWeightModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodyWeightModel>> GetBodyWeightModel(int id)
        {
          if (_context.BodyWeight == null)
          {
              return NotFound();
          }
            var bodyWeightModel = await _context.BodyWeight.FindAsync(id);

            if (bodyWeightModel == null)
            {
                return NotFound();
            }

            return bodyWeightModel;
        }

        // PUT: api/BodyWeightModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodyWeightModel(int id, BodyWeightModel bodyWeightModel)
        {
            if (id != bodyWeightModel.BodyWeightId)
            {
                return BadRequest();
            }

            _context.Entry(bodyWeightModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyWeightModelExists(id))
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

        // POST: api/BodyWeightModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodyWeightModel>> PostBodyWeightModel(BodyWeightsModelPostDao bodyWeightModelDao)
        {
          if (_context.BodyWeight == null)
          {
              return Problem("Entity set 'ApplicationDbContext.BodyWeight'  is null.");
          }

            BodyWeightModel bodyWeightModel = new BodyWeightModel() { 
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                RecordedDate = bodyWeightModelDao.RecordedDate,
                BodyWeight = bodyWeightModelDao.BodyWeight
            };
            _context.BodyWeight.Add(bodyWeightModel);
            await _context.SaveChangesAsync();

            return Ok("added");
        }

        // DELETE: api/BodyWeightModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodyWeightModel(int id)
        {
            if (_context.BodyWeight == null)
            {
                return NotFound();
            }
            var bodyWeightModel = await _context.BodyWeight.FindAsync(id);
            if (bodyWeightModel == null)
            {
                return NotFound();
            }

            _context.BodyWeight.Remove(bodyWeightModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BodyWeightModelExists(int id)
        {
            return (_context.BodyWeight?.Any(e => e.BodyWeightId == id)).GetValueOrDefault();
        }
    }
}
