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
                context.Users.Add(CreateUser("customer", sha, "pass", UserRole.Customer));
                context.Users.Add(CreateUser("accountant", sha, "pass", UserRole.Accountant));
                context.SaveChanges();
            }
        }

        private static User CreateUser(string userName, SHA512 sha, string pass, UserRole role)
        {
            var encoding = Encoding.UTF8;
            var entity = new User(userName, encoding.GetString(sha.ComputeHash(encoding.GetBytes(pass))), role);
            return entity;
        }
    }
}