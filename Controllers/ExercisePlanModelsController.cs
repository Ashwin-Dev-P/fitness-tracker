using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;
using fitt.Dao;

namespace fitt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisePlanModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExercisePlanModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExercisePlanModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExercisePlanModel>>> GetExercisePlan()
        {
          if (_context.ExercisePlan == null)
          {
              return NotFound();
          }
            return await _context.ExercisePlan.ToListAsync();
        }

        // GET: api/ExercisePlanModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExercisePlanModel>> GetExercisePlanModel(int id)
        {
          if (_context.ExercisePlan == null)
          {
              return NotFound();
          }
            var exercisePlanModel = await _context.ExercisePlan.FindAsync(id);

            if (exercisePlanModel == null)
            {
                return NotFound();
            }

            return exercisePlanModel;
        }

        // PUT: api/ExercisePlanModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercisePlanModel(int id, ExercisePlanModel exercisePlanModel)
        {
            if (id != exercisePlanModel.ExercisePlanId)
            {
                return BadRequest();
            }

            _context.Entry(exercisePlanModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExercisePlanModelExists(id))
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

        // POST: api/ExercisePlanModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExercisePlanModel>> PostExercisePlanModel(ExercisePlanModelDao exercisePlanModelDao)
        {
          if (_context.ExercisePlan == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ExercisePlan'  is null.");
          }

            ExercisePlanModel exercisePlanModel =  new ExercisePlanModel() { 
                Name = exercisePlanModelDao.Name,
                Description = exercisePlanModelDao.Description,
                ExerciseTypeId = exercisePlanModelDao.ExerciseTypeId
            };

            _context.ExercisePlan.Add(exercisePlanModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercisePlanModel", new { id = exercisePlanModel.ExercisePlanId }, exercisePlanModel);
        }

        // DELETE: api/ExercisePlanModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercisePlanModel(int id)
        {
            if (_context.ExercisePlan == null)
            {
                return NotFound();
            }
            var exercisePlanModel = await _context.ExercisePlan.FindAsync(id);
            if (exercisePlanModel == null)
            {
                return NotFound();
            }

            _context.ExercisePlan.Remove(exercisePlanModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExercisePlanModelExists(int id)
        {
            return (_context.ExercisePlan?.Any(e => e.ExercisePlanId == id)).GetValueOrDefault();
        }
    }
}
