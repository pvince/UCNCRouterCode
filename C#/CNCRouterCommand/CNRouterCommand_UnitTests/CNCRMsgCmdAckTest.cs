using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNRouterCommand_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgCmdAckTest and is intended
    ///to contain all CNCRMsgCmdAckTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgCmdAckTest
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
        ///A test for Firmware
        ///</summary>
        [TestMethod()]
        public void FirmwareTest()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(); // TODO: Initialize to an appropriate value
            byte expected = 0; // TODO: Initialize to an appropriate value
            byte actual;
            target.Firmware = expected;
            actual = target.Firmware;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Error
        ///</summary>
        [TestMethod()]
        public void ErrorTest()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.Error = expected;
            actual = target.Error;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for toSerial
        ///</summary>
        [TestMethod()]
        public void toSerialTest()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest2()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest1()
        {
            bool isError = false; // TODO: Initialize to an appropriate value
            byte firmware = 0; // TODO: Initialize to an appropriate value
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(isError, firmware);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest()
        {
            byte[] msgBytes = null; // TODO: Initialize to an appropriate value
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(msgBytes);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
