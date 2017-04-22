using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;

namespace INPDS_InitialData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ReturnFreightContext>());
            using (var context = new ReturnFreightContext())
            {
                InitializeUsers(context);
            }
        }

        private static void InitializeUsers(ReturnFreightContext context)
        {
            using (SHA512 sha = new SHA512Managed())
            {
                var encoding = Encoding.UTF8;
                context.Users.Add(new User("user", encoding.GetString(sha.ComputeHash(encoding.GetBytes("pass"))),
                    UserRole.Customer));
                context.SaveChanges();
            }
        }
    }
}