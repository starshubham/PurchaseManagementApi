using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.DAL;

namespace PurchaseManagementApi.Controllers
{
    [Route("api/iDCardTypes")]
    [ApiController]
    public class IDCardTypesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IDCardTypesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<IDCardType>>> GetAllIDCardTypes()
        {
            var idCardTypes = await _context.IDCardTypes.ToListAsync();
            return Ok(idCardTypes);
        }
    }
}
