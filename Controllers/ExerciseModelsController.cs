using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;

namespace fitt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        string IMAGE_UPLOAD_PATH = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets\images\uploads\exercises\");


        public ExerciseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseModel>>> GetExercise()
        {
          if (_context.Exercise == null)
          {
              return NotFound();
          }
            return await _context.Exercise.ToListAsync();
        }

        // GET: api/ExerciseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseModel>> GetExerciseModel(int id)
        {
          if (_context.Exercise == null)
          {
              return NotFound();
          }
            var exerciseModel = await _context.Exercise.FindAsync(id);

            if (exerciseModel == null)
            {
                return NotFound();
            }

            return exerciseModel;
        }

        // PUT: api/ExerciseModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseModel(int id, ExerciseModel exerciseModel)
        {
            if (id != exerciseModel.ExerciseId)
            {
                return BadRequest();
            }

            _context.Entry(exerciseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseModelExists(id))
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

        // POST: api/ExerciseModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseModel>> PostExerciseModel([FromForm] ExerciseModel exerciseModel)
        {
          if (_context.Exercise == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Exercise'  is null.");
          }

            string? ext = null;
            if (this.Request.Form.Files.Count > 0)
            {
                // get extension of picture
                ext = Path.GetExtension(this.Request.Form.Files[0].FileName);
                exerciseModel.ImageExtension = ext;
            }

            _context.Exercise.Add(exerciseModel);
            await _context.SaveChangesAsync();

            // IMAGE UPLOAD
            if (this.Request.Form.Files.Count > 0)
            {
                // Generate name for the file
                int exerciseId = exerciseModel.ExerciseId;
                string fileName = Convert.ToString(exerciseId) + ext;


                // Create path and stream it to the location
                var filePath = @IMAGE_UPLOAD_PATH + fileName;
                using (var stream = System.IO.File.Create(filePath))
                {
                    this.Request.Form.Files[0].CopyTo(stream);
                }
            }

            return CreatedAtAction("GetExerciseModel", new { id = exerciseModel.ExerciseId }, exerciseModel);
        }

        // DELETE: api/ExerciseModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseModel(int id)
        {
            if (_context.Exercise == null)
            {
                return NotFound();
            }
            var exerciseModel = await _context.Exercise.FindAsync(id);
            if (exerciseModel == null)
            {
                return NotFound();
            }

            _context.Exercise.Remove(exerciseModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseModelExists(int id)
        {
            return (_context.Exercise?.Any(e => e.ExerciseId == id)).GetValueOrDefault();
        }
    }
}
