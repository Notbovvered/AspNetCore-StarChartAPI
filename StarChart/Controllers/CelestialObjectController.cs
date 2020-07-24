using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{name}")]
        public IActionResult GetByName (string name)
        {
            var celestialObjects = _context.CelestialObjects.Where(e => e.Name == name);
            if(!celestialObjects.Any())
            {
                return NotFound();
            }
            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(
                    e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            return Ok(celestialObjects.ToList());

            
        }
        [HttpGet]
        public IActionResult GetByAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList(); 
            
            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId
                    == celestialObject.Id).ToList();
            }
            return Ok(celestialObjects);
        }
    }
}
