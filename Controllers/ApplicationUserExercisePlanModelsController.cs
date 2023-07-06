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
using Microsoft.AspNetCore.Identity;
using fitt.Dao;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace fitt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserExercisePlanModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //private Task<ApplicationUser> GetCurrentUserAsync() =>  _context.Users.Find(HttpContext.User);


        public ApplicationUserExercisePlanModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationUserExercisePlanModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserExercisePlanModel>>> GetApplicationUserExercisePlanModel()
        {
          if (_context.ApplicationUserExercisePlanModel == null)
          {
              return NotFound();
          }
            return await _context.ApplicationUserExercisePlanModel.ToListAsync();
        }

        // GET: api/ApplicationUserExercisePlanModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserExercisePlanModel>> GetApplicationUserExercisePlanModel(int id)
        {
          if (_context.ApplicationUserExercisePlanModel == null)
          {
              return NotFound();
          }
            var applicationUserExercisePlanModel = await _context.ApplicationUserExercisePlanModel.FindAsync(id);

            if (applicationUserExercisePlanModel == null)
            {
                return NotFound();
            }

            return applicationUserExercisePlanModel;
        }

        // GET: api/ApplicationUserExercisePlanModels/ExercisePlans
        [HttpGet("ExercisePlans")]
        public async Task<ActionResult<List<ExercisePlanDisplayModelDao>>> GetExercisePlansForApplicationUser()
        {
            if (_context.ExercisePlan == null)
            {
                return NotFound();
            }
            
            

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ExercisePlanDisplayModelDao> exercise_plans = await (from application_user_exercise_exercise_plan in _context.ApplicationUserExercisePlanModel
                                                                where
                                                                application_user_exercise_exercise_plan.ApplicationUserId == userId
                                                                join
                                                                exercise_plan in _context.ExercisePlan
                                                                on application_user_exercise_exercise_plan.ExercisePlanId equals exercise_plan.ExercisePlanId
                                                                select
                                                                    (new ExercisePlanDisplayModelDao(){
                                                                        ExercisePlanId = exercise_plan.ExercisePlanId,
                                                                        Name = exercise_plan.Name,
                                                                        Description = exercise_plan.Description
                                                                    })
                                                                ).ToListAsync();
                                                                ;






            if (exercise_plans == null)
            {
                return NotFound();
            }
            return exercise_plans;
        }

        // PUT: api/ApplicationUserExercisePlanModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUserExercisePlanModel(int id, ApplicationUserExercisePlanModel applicationUserExercisePlanModel)
        {
            if (id != applicationUserExercisePlanModel.ApplicationUserExercisePlanId)
            {
                return BadRequest();
            }

            _context.Entry(applicationUserExercisePlanModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExercisePlanModelExists(id))
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

        // POST: api/ApplicationUserExercisePlanModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUserExercisePlanModel>> PostApplicationUserExercisePlanModel(ApplicationUserExercisePlanModelDao applicationUserExercisePlanModelDao)
        {
              if (_context.ApplicationUserExercisePlanModel == null)
              {
                  return Problem("Entity set 'ApplicationDbContext.ApplicationUserExercisePlanModel'  is null.");
              }

            string userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            ApplicationUserExercisePlanModel applicationUserExercisePlanModel = new ApplicationUserExercisePlanModel()
            { 
                ExercisePlanId = applicationUserExercisePlanModelDao.ExercisePlanId,
                ApplicationUserId = userId
            };
            _context.ApplicationUserExercisePlanModel.Add(applicationUserExercisePlanModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ApplicationUserExercisePlanModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUserExercisePlanModel(int id)
        {
            if (_context.ApplicationUserExercisePlanModel == null)
            {
                return NotFound();
            }
            var applicationUserExercisePlanModel = await _context.ApplicationUserExercisePlanModel.FindAsync(id);
            if (applicationUserExercisePlanModel == null)
            {
                return NotFound();
            }

            _context.ApplicationUserExercisePlanModel.Remove(applicationUserExercisePlanModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        // DELETE: api/ApplicationUserExercisePlanModels/RemoveExercisePlan/5
        [HttpDelete("RemoveExercisePlan/{id}")]
        public async Task<IActionResult> RemoveExercisePlanForUser(int id)
        {
            if (_context.ApplicationUserExercisePlanModel == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             //applicationUserExercisePlanModelToBeDeleted =await(from applicationUserExercisePlanModelItem in _context.ApplicationUserExercisePlanModel
             //               where applicationUserExercisePlanModelItem.ApplicationUserId == userId && applicationUserExercisePlanModelItem.ExercisePlanId == id 
             //               select applicationUserExercisePlanModelItem).ToArrayAsync()[0];
            //var applicationUserExercisePlanModel = await _context.ApplicationUserExercisePlanModel.FindAsync(id);

            ApplicationUserExercisePlanModel?  applicationUserExercisePlanModelToBeDeleted = _context.ApplicationUserExercisePlanModel.Where(a=>a.ApplicationUserId == userId && a.ExercisePlanId == id).FirstOrDefault();
            if (applicationUserExercisePlanModelToBeDeleted == null)
            {
                return NotFound();
            }

            _context.ApplicationUserExercisePlanModel.Remove(applicationUserExercisePlanModelToBeDeleted);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationUserExercisePlanModelExists(int id)
        {
            return (_context.ApplicationUserExercisePlanModel?.Any(e => e.ApplicationUserExercisePlanId == id)).GetValueOrDefault();
        }
    }
}
