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
    public class ExerciseDailyPlanModelExerciseModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExerciseDailyPlanModelExerciseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseDailyPlanModelExerciseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDailyPlanModelExerciseModel>>> GetExerciseDailyPlanExerciseModel()
        {
          if (_context.ExerciseDailyPlanExercise == null)
          {
              return NotFound();
          }

          
            return await _context.ExerciseDailyPlanExercise.ToListAsync();
        }

        // GET: api/ExerciseDailyPlanModelExerciseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDailyPlanModelExerciseModel>> GetExerciseDailyPlanModelExerciseModel(int id)
        {
          if (_context.ExerciseDailyPlanExercise == null)
          {
              return NotFound();
          }

            var ans = from item in _context.ExerciseDailyPlanExercise
                      where item.ExerciseDailyPlanId == id
                      join 
                      exercise in _context.Exercise on item.ExerciseId equals exercise.ExerciseId
                      join 
                      exerciseDailyPlan in _context.ExerciseDailyPlan on item.ExerciseDailyPlanId equals exerciseDailyPlan.ExerciseDailyPlanId
                      select (item.Exercise)
                      
                      ;
            Console.WriteLine(ans);
            foreach(var exercise  in ans)
            {
                Console.WriteLine(exercise.Name);
                Console.WriteLine(exercise.ImageExtension);

            }
            var exerciseDailyPlanModelExerciseModel = await _context.ExerciseDailyPlanExercise.FindAsync(id);
            
            
            if (exerciseDailyPlanModelExerciseModel == null)
            {
                return NotFound();
            }

            return exerciseDailyPlanModelExerciseModel;
        }

        // PUT: api/ExerciseDailyPlanModelExerciseModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseDailyPlanModelExerciseModel(int id, ExerciseDailyPlanModelExerciseModel exerciseDailyPlanModelExerciseModel)
        {
            if (id != exerciseDailyPlanModelExerciseModel.ExerciseDailyPlanExerciseId)
            {
                return BadRequest();
            }

            _context.Entry(exerciseDailyPlanModelExerciseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseDailyPlanModelExerciseModelExists(id))
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

        // POST: api/ExerciseDailyPlanModelExerciseModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseDailyPlanModelExerciseModel>> PostExerciseDailyPlanModelExerciseModel([FromBody] ExerciseDailyPlanModelExerciseModelsDao exerciseDailyPlanModelExerciseModelDao)
        {

            ExerciseDailyPlanModelExerciseModel exerciseDailyPlanModelExerciseModel = new ExerciseDailyPlanModelExerciseModel() { 
                 ExerciseDailyPlanId = exerciseDailyPlanModelExerciseModelDao.ExerciseDailyPlanId,
                 ExerciseId = exerciseDailyPlanModelExerciseModelDao.ExerciseId
            };

          if (_context.ExerciseDailyPlanExercise == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ExerciseDailyPlanExerciseModel'  is null.");
          }
            _context.ExerciseDailyPlanExercise.Add(exerciseDailyPlanModelExerciseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseDailyPlanModelExerciseModel", new { id = exerciseDailyPlanModelExerciseModel.ExerciseDailyPlanExerciseId }, exerciseDailyPlanModelExerciseModel);
        }

        // DELETE: api/ExerciseDailyPlanModelExerciseModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseDailyPlanModelExerciseModel(int id)
        {
            if (_context.ExerciseDailyPlanExercise == null)
            {
                return NotFound();
            }
            var exerciseDailyPlanModelExerciseModel = await _context.ExerciseDailyPlanExercise.FindAsync(id);
            if (exerciseDailyPlanModelExerciseModel == null)
            {
                return NotFound();
            }

            _context.ExerciseDailyPlanExercise.Remove(exerciseDailyPlanModelExerciseModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseDailyPlanModelExerciseModelExists(int id)
        {
            return (_context.ExerciseDailyPlanExercise?.Any(e => e.ExerciseDailyPlanExerciseId == id)).GetValueOrDefault();
        }
    }
}
