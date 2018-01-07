using System;
using System.Web.Mvc;
using MachineaCafe.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestMachineCafe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIndex()
        {
            Machine_CafeController controller = new Machine_CafeController();

            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
