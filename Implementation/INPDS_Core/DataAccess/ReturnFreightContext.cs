using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.DataAccess
{
    public class ReturnFreightContext : DbContext
    {
        public ReturnFreightContext() : base("ReturnFreightConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public ValidationResult TrySaveChanges()
        {
            try
            {
                SaveChanges();
                return ValidationResult.Ok();
            }
            catch (DbUpdateException)
            {
                return ValidationResult.Error("Uložení změn se nezdařilo.");
            }
        }
    }
}