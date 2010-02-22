using CNCRouterCommand;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
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
        ///A test for toSerial
        ///</summary>
        [TestMethod()]
        public void toSerialTest()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(true, 127);
            byte[] expected = { 0x12, 0xFF, 0xED };
            byte[] actual;
            actual = target.toSerial();
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for getFirmware
        ///</summary>
        [TestMethod()]
        public void getFirmwareTest()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(true, 127); // TODO: Initialize to an appropriate value
            byte expected = 127; // TODO: Initialize to an appropriate value
            byte actual;
            actual = target.getFirmware();
            Assert.AreEqual<byte>(expected, actual);
        }

        /// <summary>
        ///A test for getError
        ///</summary>
        [TestMethod()]
        public void getErrorTest()
        {
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(true, 127); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.getError();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void CNCRMsgCmdAckConstructorTest2()
        {
            CNCRMsgCmdAck_Accessor target = new CNCRMsgCmdAck_Accessor();
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest1()
        {
            byte[] msgBytes = { 0x12, 0xFF, 0xED };
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(msgBytes);
            Assert.AreEqual(true, target.getError());
            Assert.AreEqual(127, target.getFirmware());
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest_valid()
        {
            bool isError = false;
            byte firmware = 0;
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(isError, firmware);
            Assert.AreEqual(isError, target.getError());
            Assert.AreEqual(firmware, target.getFirmware());
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest_ErrorLowFW()
        {
            bool isError = true;
            byte firmware = 0;
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(isError, firmware);
            Assert.AreEqual(isError, target.getError());
            Assert.AreEqual(firmware, target.getFirmware());
        }

        /// <summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgCmdAckConstructorTest_ErrHighFW()
        {
            bool isError = true;
            byte firmware = 127;
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(isError, firmware);
            Assert.AreEqual(isError, target.getError());
            Assert.AreEqual(firmware, target.getFirmware());
        }

        ///<summary>
        ///A test for CNCRMsgCmdAck Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Constructor allowed a bad FW version.")]
        public void CNCRMsgCmdAckConstructorTest_ErrBadFW()
        {
            bool isError = true;
            byte firmware = 128;
            CNCRMsgCmdAck target = new CNCRMsgCmdAck(isError, firmware);
            Assert.AreEqual(isError, target.getError());
            Assert.AreEqual(firmware, target.getFirmware());
        }
    }
}
