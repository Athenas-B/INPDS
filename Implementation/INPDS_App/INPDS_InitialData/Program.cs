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
            using (var context = new ReturnFreightContext())
            {
                //Remove all existing entries
                context.Orders.RemoveRange(context.Orders);
                context.Users.RemoveRange(context.Users);

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