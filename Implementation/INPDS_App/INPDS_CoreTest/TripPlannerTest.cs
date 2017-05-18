using System.Linq;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    [TestClass]
    public class TripPlannerTest
    {
        private static Order _primaryOrder;
        private static Order _secondaryOrder;
        private readonly ITripPlanner _planner = new TripPlanner();

        [TestMethod]
        public void TestPlan()
        {
            var validationResult = _planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);

            var trip = GetTripByOrder(_primaryOrder);
            Assert.IsNotNull(trip);
        }

        [TestMethod]
        public void TestPlanSecondary()
        {
            var validationResult = _planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);

            var trip = GetTripByOrder(_primaryOrder);
            Assert.IsNotNull(trip);

            validationResult = _planner.PlanTrip(trip, _secondaryOrder);
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanNull()
        {
            var validationResult = _planner.PlanTrip(null);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanSecondaryNullOrder()
        {
            var validationResult = _planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);

            var trip = GetTripByOrder(_primaryOrder);
            Assert.IsNotNull(trip);

            validationResult = _planner.PlanTrip(trip, null);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanSecondaryNullTrip()
        {
            var validationResult = _planner.PlanTrip(null, _secondaryOrder);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanSecondaryNullTripAndOrder()
        {
            var validationResult = _planner.PlanTrip(null, null);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanAlreadyPlannedTrip()
        {
            var validationResult = _planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);

            validationResult = _planner.PlanTrip(_primaryOrder);
            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void TestPlanAlreadyPlannedTrip2()
        {
            var validationResult = _planner.PlanTrip(_primaryOrder);
            Assert.IsTrue(validationResult.IsValid);
            
            var trip = GetTripByOrder(_primaryOrder);
            Assert.IsNotNull(trip);

            validationResult = _planner.PlanTrip(trip, _secondaryOrder);
            Assert.IsTrue(validationResult.IsValid);

            validationResult = _planner.PlanTrip(trip, _secondaryOrder);
            Assert.IsFalse(validationResult.IsValid);

            validationResult = _planner.PlanTrip(trip, _primaryOrder);
            Assert.IsFalse(validationResult.IsValid);
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
            //remove all created trips
            using (var context = new ReturnFreightContext())
            {
                var createdTrips = context.Trips.Where(
                    t =>
                        t.PrimaryOrder.Id == _primaryOrder.Id || t.PrimaryOrder.Id == _secondaryOrder.Id ||
                        t.SecondaryOrder.Id == _primaryOrder.Id || t.SecondaryOrder.Id == _secondaryOrder.Id).ToList();
                context.Trips.RemoveRange(createdTrips);
            }
            TestUtils.DeleteTestOrder(_primaryOrder);
            TestUtils.DeleteTestOrder(_secondaryOrder);
        }

        private Trip GetTripByOrder(Order order)
        {
            using (var context = new ReturnFreightContext())
            {
                return
                    context.Trips.SingleOrDefault(
                        trip => trip.PrimaryOrder.Id == order.Id || trip.SecondaryOrder.Id == order.Id);
            }
        }

        #endregion
    }
}