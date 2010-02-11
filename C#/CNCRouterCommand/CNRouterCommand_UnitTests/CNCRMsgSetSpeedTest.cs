using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNRouterCommand_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgSetSpeedTest and is intended
    ///to contain all CNCRMsgSetSpeedTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgSetSpeedTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for toSerial
        ///</summary>
        [TestMethod()]
        public void toSerialTest()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for setSpeed
        ///</summary>
        [TestMethod()]
        public void setSpeedTest()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(); // TODO: Initialize to an appropriate value
            byte speed = 0; // TODO: Initialize to an appropriate value
            target.setSpeed(speed);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for getSpeed
        ///</summary>
        [TestMethod()]
        public void getSpeedTest()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(); // TODO: Initialize to an appropriate value
            byte expected = 0; // TODO: Initialize to an appropriate value
            byte actual;
            actual = target.getSpeed();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CNCRMsgSetSpeed Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgSetSpeedConstructorTest1()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgSetSpeed Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgSetSpeedConstructorTest()
        {
            byte speed = 0; // TODO: Initialize to an appropriate value
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(speed);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
