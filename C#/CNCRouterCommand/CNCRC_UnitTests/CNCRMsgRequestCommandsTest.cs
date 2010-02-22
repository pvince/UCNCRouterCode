using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgRequestCommandsTest and is intended
    ///to contain all CNCRMsgRequestCommandsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgRequestCommandsTest
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
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(null); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getCommandCount
        ///</summary>
        [TestMethod()]
        public void getCommandCountTest()
        {
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(null); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.getCommandCount();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgRequestCommandsConstructorTest2()
        {
            byte commandCount = 0; // TODO: Initialize to an appropriate value
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(commandCount);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void CNCRMsgRequestCommandsConstructorTest1()
        {
            CNCRMsgRequestCommands_Accessor target = new CNCRMsgRequestCommands_Accessor();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgRequestCommandsConstructorTest()
        {
            byte[] msgBytes = null; // TODO: Initialize to an appropriate value
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(msgBytes);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
