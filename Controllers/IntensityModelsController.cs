﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fitt.Data;
using fitt.Models;
using Microsoft.AspNetCore.Authorization;
using fitt.Dao;
using System.Security.Claims;

namespace fitt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IntensityModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IntensityModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/IntensityModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntensityModel>>> GetIntensityModel()
        {
          if (_context.IntensityModel == null)
          {
              return NotFound();
          }
            return await _context.IntensityModel.ToListAsync();
        }

        // GET: api/IntensityModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntensityModel>> GetIntensityModel(int id)
        {
          if (_context.IntensityModel == null)
          {
              return NotFound();
          }
            var intensityModel = await _context.IntensityModel.FindAsync(id);

            if (intensityModel == null)
            {
                return NotFound();
            }

            return intensityModel;
        }

        // PUT: api/IntensityModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntensityModel(int id, IntensityModel intensityModel)
        {
            if (id != intensityModel.IntensityId)
            {
                return BadRequest();
            }

            _context.Entry(intensityModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntensityModelExists(id))
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

        // POST: api/IntensityModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IntensityModel>> PostIntensityModel(  IntensityModelDao intensityModelDao)
        {
          if (_context.IntensityModel == null)
          {
              return Problem("Entity set 'ApplicationDbContext.IntensityModel'  is null.");
          }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IntensityModel intensityModel = new IntensityModel() {
                weights = intensityModelDao.weights,
                repetitions = intensityModelDao.repetitions,
                ExerciseDate = intensityModelDao.ExerciseDate,
                ExerciseId = intensityModelDao.ExerciseId,
                ApplicationUserId = userId
            };
            _context.IntensityModel.Add(intensityModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/IntensityModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntensityModel(int id)
        {
            if (_context.IntensityModel == null)
            {
                return NotFound();
            }
            var intensityModel = await _context.IntensityModel.FindAsync(id);
            if (intensityModel == null)
            {
                return NotFound();
            }

            _context.IntensityModel.Remove(intensityModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IntensityModelExists(int id)
        {
            return (_context.IntensityModel?.Any(e => e.IntensityId == id)).GetValueOrDefault();
        }
    }
}