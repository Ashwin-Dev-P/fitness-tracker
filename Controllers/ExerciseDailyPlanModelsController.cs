using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;
using System.Collections.Immutable;

namespace fitt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseDailyPlanModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExerciseDailyPlanModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseDailyPlanModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDailyPlanModel>>> GetExerciseDailyPlan()
        {
          if (_context.ExerciseDailyPlan == null)
          {
              return NotFound();
          }
            return await _context.ExerciseDailyPlan.ToListAsync();
        }

        // GET: api/ExerciseDailyPlanModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDailyPlanModel>> GetExerciseDailyPlanModel(int id)
        {
          if (_context.ExerciseDailyPlan == null)
          {
              return NotFound();
          }

           

            
            var exerciseDailyPlanModel = await _context.ExerciseDailyPlan.FindAsync(id);
            if (exerciseDailyPlanModel == null)
            {
                return NotFound();
            }

           

            

            return exerciseDailyPlanModel;
        }

        // GET: api/ExerciseDailyPlanModels/5/Exercises
        [HttpGet("{id}/Exercises")]
        public async Task<ActionResult<List<ExerciseModel>>> GetExercisesFromExerciseDailyPlanModel(int id)
        {
            if (_context.ExerciseDailyPlan == null)
            {
                return NotFound();
            }

            List<ExerciseModel>? exercises = await (from item in _context.ExerciseDailyPlanExercise
                                                    where item.ExerciseDailyPlanId == id
                                                    join
                                                    exercise in _context.Exercise on item.ExerciseId equals exercise.ExerciseId
                                                    join
                                                    exerciseDailyPlan in _context.ExerciseDailyPlan on item.ExerciseDailyPlanId equals exerciseDailyPlan.ExerciseDailyPlanId
                                                    // select (new { item.Exercise.ExerciseId , item.Exercise.Name , item.Exercise.ImageExtension, item.Exercise.Description   })
                                                    select (item.Exercise)).ToListAsync();
            ;

            if (exercises.Count == 0)
            {
                return NotFound();
            }



            return exercises;
        }

        // PUT: api/ExerciseDailyPlanModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseDailyPlanModel(int id, ExerciseDailyPlanModel exerciseDailyPlanModel)
        {
            if (id != exerciseDailyPlanModel.ExerciseDailyPlanId)
            {
                return BadRequest();
            }

            _context.Entry(exerciseDailyPlanModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseDailyPlanModelExists(id))
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

        // POST: api/ExerciseDailyPlanModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseDailyPlanModel>> PostExerciseDailyPlanModel(ExerciseDailyPlanModel exerciseDailyPlanModel)
        {
          if (_context.ExerciseDailyPlan == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ExerciseDailyPlan'  is null.");
          }
            _context.ExerciseDailyPlan.Add(exerciseDailyPlanModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseDailyPlanModel", new { id = exerciseDailyPlanModel.ExerciseDailyPlanId }, exerciseDailyPlanModel);
        }

        // DELETE: api/ExerciseDailyPlanModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseDailyPlanModel(int id)
        {
            if (_context.ExerciseDailyPlan == null)
            {
                return NotFound();
            }
            var exerciseDailyPlanModel = await _context.ExerciseDailyPlan.FindAsync(id);
            if (exerciseDailyPlanModel == null)
            {
                return NotFound();
            }

            _context.ExerciseDailyPlan.Remove(exerciseDailyPlanModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseDailyPlanModelExists(int id)
        {
            return (_context.ExerciseDailyPlan?.Any(e => e.ExerciseDailyPlanId == id)).GetValueOrDefault();
        }
    }
}
