using INPDS_Core.Controller;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    //TODO Implement tests
    [TestClass]
    public class TripPlannerTest
    {
        private static Order _primaryOrder;
        private static Order _secondaryOrder;
        private readonly ITripPlanner planner = new TripPlanner();

        [TestMethod]
        public void TestPlanPrimaryAndSecondaryNull()
        {
            var validationResult = planner.PlanTrip(null);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanPrimaryNullSecondaryNotNull()
        {
            var validationResult = planner.PlanTrip(null, _primaryOrder);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanPrimaryNotNull()
        {
            var validationResult = planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanPrimaryAndSecondaryNotNull()
        {
            var validationResult = planner.PlanTrip(_primaryOrder, _secondaryOrder);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanFirstPrimaryThenSecondaryNotNull()
        {
            var validationResult = planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);

            validationResult = planner.PlanTrip(_primaryOrder, _secondaryOrder);
            Assert.IsTrue(validationResult.IsValid);
        }

        #region Private method

        [TestInitialize]
        public void Initialize()
        {
            _primaryOrder = TestUtils.CreateTestOrder();
            _secondaryOrder = TestUtils.CreateTestOrder(TestUtils.NextWeek.AddDays(5), TestUtils.NextWeek.AddDays(1),
                "to", "from");
        }

        [TestCleanup]
        public void DeleteTestOrder()
        {
            TestUtils.DeleteTestOrder(_primaryOrder);
        }

        #endregion
    }
}