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
    public class ExerciseTypeModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        string IMAGE_UPLOAD_PATH = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets\images\uploads\exercise_types\");


        public ExerciseTypeModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseTypeModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseTypeModel>>> GetExerciseType()
        {
          if (_context.ExerciseType == null)
          {
              return NotFound();
          }
            return await _context.ExerciseType.ToListAsync();
        }

        // GET: api/ExerciseTypeModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ExercisePlanDisplayModelDao>>> GetExerciseTypeModel(int id)
        {
          if (_context.ExerciseType == null)
          {
              return NotFound();
          }
            

            List<ExercisePlanDisplayModelDao> exercisePlans = await (from exercisePlan in _context.ExercisePlan
                                                           where exercisePlan.ExerciseTypeId == id
                                                           select new ExercisePlanDisplayModelDao()  { Name = exercisePlan.Name , Description = exercisePlan.Description , ExercisePlanId = exercisePlan.ExercisePlanId  }
                                ).ToListAsync();

            if (exercisePlans == null || exercisePlans.Count == 0)
            {
                return NotFound();
            }
                                

            return exercisePlans;
        }

        // PUT: api/ExerciseTypeModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseTypeModel(int id, ExerciseTypeModel exerciseTypeModel)
        {
            if (id != exerciseTypeModel.ExerciseTypeId)
            {
                return BadRequest();
            }

            _context.Entry(exerciseTypeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseTypeModelExists(id))
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

        // POST: api/ExerciseTypeModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseTypeModel>> PostExerciseTypeModel([FromForm] ExerciseTypeModelDao exerciseTypeModelDao)
        {

            
            if (_context.ExerciseType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExerciseType'  is null.");
            }

            ExerciseTypeModel exerciseTypeModel = new ExerciseTypeModel() { 
                Name = exerciseTypeModelDao.Name,
                Description = exerciseTypeModelDao.Description,

            };


            string? ext = null;
            if (this.Request.Form.Files.Count > 0)
            {
                // get extension of picture
                ext = Path.GetExtension(this.Request.Form.Files[0].FileName);
                exerciseTypeModel.ImageExtension = ext;
            }

            _context.ExerciseType.Add(exerciseTypeModel);
            await _context.SaveChangesAsync();

            // IMAGE UPLOAD
            if (this.Request.Form.Files.Count > 0)
            {
                // Generate name for the file
                int movieId = exerciseTypeModel.ExerciseTypeId;
                string fileName = Convert.ToString(movieId) + ext;


                // Create path and stream it to the location
                var filePath = @IMAGE_UPLOAD_PATH + fileName;
                using (var stream = System.IO.File.Create(filePath))
                {
                    this.Request.Form.Files[0].CopyTo(stream);
                }
            }

            return CreatedAtAction("GetExerciseTypeModel", new { id = exerciseTypeModel.ExerciseTypeId }, exerciseTypeModel);
        }

        // DELETE: api/ExerciseTypeModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseTypeModel(int id)
        {
            if (_context.ExerciseType == null)
            {
                return NotFound();
            }
            var exerciseTypeModel = await _context.ExerciseType.FindAsync(id);
            if (exerciseTypeModel == null)
            {
                return NotFound();
            }

            _context.ExerciseType.Remove(exerciseTypeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseTypeModelExists(int id)
        {
            return (_context.ExerciseType?.Any(e => e.ExerciseTypeId == id)).GetValueOrDefault();
        }
    }
}
