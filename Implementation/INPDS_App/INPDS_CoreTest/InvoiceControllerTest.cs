using INPDS_Core.Controller;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    [TestClass]
    public class InvoiceControllerTest
    {
        private static Order _order;

        [TestMethod]
        public void RegisterInvoiceTestTrue()
        {
            var testInvoice = new Invoice(_order, 1000);
            var invoiceController = new InvoiceController();
            var vysledek = invoiceController.RegisterInvoice(testInvoice);
            Assert.IsTrue(vysledek.IsValid);
            TestUtils.DeleteTestInovice(testInvoice);
        }

        [TestMethod]
        public void RegisterInvoiceTestZeroPrice()
        {
            var testInvoice = new Invoice(_order, 0);
            var invoiceController = new InvoiceController();

            Assert.IsTrue(invoiceController.RegisterInvoice(testInvoice).IsValid);
            TestUtils.DeleteTestInovice(testInvoice);
        }

        [TestMethod]
        public void RegisterInvoiceTestPrice()
        {
            var testInvoice = new Invoice(_order, -10);
            var invoiceController = new InvoiceController();

            Assert.IsFalse(invoiceController.RegisterInvoice(testInvoice).IsValid);
        }

        #region Private method

        [ClassInitialize]
        public static void Initialize(TestContext con)
        {
            _order = TestUtils.CreateTestOrder();
        }

        [ClassCleanup]
        public static void DeleteTestOrder()
        {
            TestUtils.DeleteTestOrder(_order);
        }

        #endregion
    }
}