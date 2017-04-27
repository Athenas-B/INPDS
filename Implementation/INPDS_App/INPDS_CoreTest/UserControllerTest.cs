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
            IUserController userController = UserController.Instance;
            userController.Login("customer", "pass");
            Assert.IsTrue(userController.IsLoggedIn);
        }

        [TestMethod]
        public void IsLoggedInTestFalse()
        {
            IUserController userController = UserController.Instance;
            userController.Login("customer", "passs");
            Assert.IsFalse(userController.IsLoggedIn);
        }

        [TestMethod]
        public void LoggedUserTestIsNull()
        {
            IUserController userController = UserController.Instance;
            userController.Login("customer", "passs");
            Assert.IsNull(userController.LoggedUser);
        }

        [TestMethod]
        public void LoggedUserTestNotNull()
        {
            IUserController userController = UserController.Instance;
            userController.Login("customer", "pass");
            Assert.IsNotNull(userController.LoggedUser);
        }

        [TestMethod]
        public void LogOutTest()
        {
            IUserController userController = UserController.Instance;
            userController.Login("customer", "pass");

            Assert.IsNotNull(userController.LoggedUser);

            userController.Logout();

            Assert.IsNull(userController.LoggedUser);
        }

        [TestCleanup()]
        public void LogOutUser()
        {
            UserController.Instance.Logout();
        }
    }
}