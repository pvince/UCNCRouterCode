using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgToolCmdTest and is intended
    ///to contain all CNCRMsgToolCmdTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgToolCmdTest
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
            bool toolOn = false; // TODO: Initialize to an appropriate value
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isToolOn
        ///</summary>
        [TestMethod()]
        public void isToolOnTest()
        {
            bool toolOn = false; // TODO: Initialize to an appropriate value
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.isToolOn();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CNCRMsgToolCmd Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgToolCmdConstructorTest()
        {
            bool toolOn = false; // TODO: Initialize to an appropriate value
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
