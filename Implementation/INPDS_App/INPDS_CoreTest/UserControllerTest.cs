using INPDS_Core.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    //TODO Implement tests
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void IsLoggedInTestTrue()
        {
            IUserController userController = new UserController();
            userController.Login("user", "pass");
            Assert.IsTrue(userController.IsLoggedIn);
        }

        [TestMethod]
        public void IsLoggedInTestFalse()
        {
            IUserController userController = new UserController();
            userController.Login("user", "passs");
            Assert.IsFalse(userController.IsLoggedIn);
        }

        [TestMethod]
        public void LoggedUserTestIsNull()
        {
            IUserController userController = new UserController();
            userController.Login("user", "passs");
            Assert.IsNull(userController.LoggedUser);
        }

        [TestMethod]
        public void LoggedUserTestNotNull()
        {
            IUserController userController = new UserController();
            userController.Login("user", "pass");
            Assert.IsNotNull(userController.LoggedUser);
        }

        [TestMethod]
        public void LogOutTest()
        {
            IUserController userController = new UserController();
            userController.Login("user", "pass");

            Assert.IsNotNull(userController.LoggedUser);

            userController.Logout();

            Assert.IsNull(userController.LoggedUser);
        }
    }
}