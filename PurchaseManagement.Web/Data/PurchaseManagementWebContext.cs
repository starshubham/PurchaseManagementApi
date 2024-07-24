using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace PurchaseManagement.Web.Data
{
    public class PurchaseManagementWebContext : DbContext
    {
        public PurchaseManagementWebContext (DbContextOptions<PurchaseManagementWebContext> options)
            : base(options)
        {
        }

        public DbSet<Common.Models.Candidate> Candidate { get; set; } = default!;
    }
}
