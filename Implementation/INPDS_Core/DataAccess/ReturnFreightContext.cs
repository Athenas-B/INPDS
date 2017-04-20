using System.Data.Entity;
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
    }
}