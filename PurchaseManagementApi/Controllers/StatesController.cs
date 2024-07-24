using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.DAL;

namespace PurchaseManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StatesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<State>>> GetAllStates()
        {
            var states = await _context.States.ToListAsync();
            return Ok(states);
        }
    }
}
