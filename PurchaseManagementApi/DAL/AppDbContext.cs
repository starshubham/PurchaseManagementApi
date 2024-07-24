using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace PurchaseManagementApi.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set;}
        public DbSet<PO_Item> PO_Items { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<IDCardType> IDCardTypes { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
    }
}
