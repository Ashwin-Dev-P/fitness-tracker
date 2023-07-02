using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;
using fitt.Dao.ExercisePlanExerciseDailyPlanModelDao;

namespace fitt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisePlanExerciseDailyPlanModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExercisePlanExerciseDailyPlanModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExercisePlanExerciseDailyPlanModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExercisePlanExerciseDailyPlanModel>>> GetExercisePlanExerciseDailyPlanModel()
        {
          if (_context.ExercisePlanExerciseDailyPlanModel == null)
          {
              return NotFound();
          }
            return await _context.ExercisePlanExerciseDailyPlanModel.ToListAsync();
        }

        // GET: api/ExercisePlanExerciseDailyPlanModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExercisePlanExerciseDailyPlanModel>> GetExercisePlanExerciseDailyPlanModel(int id)
        {
          if (_context.ExercisePlanExerciseDailyPlanModel == null)
          {
              return NotFound();
          }
            var exercisePlanExerciseDailyPlanModel = await _context.ExercisePlanExerciseDailyPlanModel.FindAsync(id);

            if (exercisePlanExerciseDailyPlanModel == null)
            {
                return NotFound();
            }

            return exercisePlanExerciseDailyPlanModel;
        }

        // PUT: api/ExercisePlanExerciseDailyPlanModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercisePlanExerciseDailyPlanModel(int id, ExercisePlanExerciseDailyPlanModel exercisePlanExerciseDailyPlanModel)
        {
            if (id != exercisePlanExerciseDailyPlanModel.ExercisePlanExerciseDailyPlanId)
            {
                return BadRequest();
            }

            _context.Entry(exercisePlanExerciseDailyPlanModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExercisePlanExerciseDailyPlanModelExists(id))
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

        // POST: api/ExercisePlanExerciseDailyPlanModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExercisePlanExerciseDailyPlanModel>> PostExercisePlanExerciseDailyPlanModel(ExercisePlanExerciseDailyPlanModelDao exercisePlanExerciseDailyPlanModelDao)
        {

              if (_context.ExercisePlanExerciseDailyPlanModel == null)
              {
                  return Problem("Entity set 'ApplicationDbContext.ExercisePlanExerciseDailyPlanModel'  is null.");
              }

              ExercisePlanExerciseDailyPlanModel exercisePlanExerciseDailyPlanModel = new ExercisePlanExerciseDailyPlanModel()
              {
                  ExercisePlanId = exercisePlanExerciseDailyPlanModelDao.ExercisePlanId,
                  ExerciseDailyPlanId = exercisePlanExerciseDailyPlanModelDao.ExerciseDailyPlanId
              };


            _context.ExercisePlanExerciseDailyPlanModel.Add(exercisePlanExerciseDailyPlanModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercisePlanExerciseDailyPlanModel", new { id = exercisePlanExerciseDailyPlanModel.ExercisePlanExerciseDailyPlanId }, exercisePlanExerciseDailyPlanModel);
        }

        // DELETE: api/ExercisePlanExerciseDailyPlanModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercisePlanExerciseDailyPlanModel(int id)
        {
            if (_context.ExercisePlanExerciseDailyPlanModel == null)
            {
                return NotFound();
            }
            var exercisePlanExerciseDailyPlanModel = await _context.ExercisePlanExerciseDailyPlanModel.FindAsync(id);
            if (exercisePlanExerciseDailyPlanModel == null)
            {
                return NotFound();
            }

            _context.ExercisePlanExerciseDailyPlanModel.Remove(exercisePlanExerciseDailyPlanModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExercisePlanExerciseDailyPlanModelExists(int id)
        {
            return (_context.ExercisePlanExerciseDailyPlanModel?.Any(e => e.ExercisePlanExerciseDailyPlanId == id)).GetValueOrDefault();
        }
    }
}
