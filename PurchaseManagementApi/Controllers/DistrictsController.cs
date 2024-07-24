using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.DAL;

namespace PurchaseManagementApi.Controllers
{
    [Route("api/districts")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DistrictsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<District>>> GetAllDistricts()
        {
            var districts = await _context.Districts.ToListAsync();
            return Ok(districts);
        }
    }
}
