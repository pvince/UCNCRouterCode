using CNCRouterCommand;
using System;
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
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(128);
            byte[] expected = { 0x30, 0xFF, 0xCF };
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual<byte>(expected[0], actual[0], "Byte 0");
            Assert.AreEqual<byte>(expected[1], actual[1], "Byte 1");
            Assert.AreEqual<byte>(expected[2], actual[2], "Byte 2");
        }

        /// <summary>
        ///A test for getCommandCount
        ///</summary>
        [TestMethod()]
        public void getCommandCountTest()
        {
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(128);
            int expected = 128;
            int actual;
            actual = target.getCommandCount();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgRequestCommandsConstructorTest2()
        {
            byte commandCount = 128;
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(commandCount);
            Assert.AreEqual(CNCRMESSAGE_TYPE.REQUEST_COMMAND, target.getMessageType());
            Assert.AreEqual(0x30, target.getMsgTypeByte());
            Assert.AreEqual(commandCount, target.getCommandCount());
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Constructor allowed large command count.")]
        public void CNCRMsgRequestCommandsConstructorTest2_Fail()
        {
            byte commandCount = 129;
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(commandCount);
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void CNCRMsgRequestCommandsConstructorTest1()
        {
            CNCRMsgRequestCommands_Accessor target = new CNCRMsgRequestCommands_Accessor();
        }

        /// <summary>
        ///A test for CNCRMsgRequestCommands Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgRequestCommandsConstructorTest()
        {
            byte[] msgBytes = { 0x30, 0xFF, 0xCF }; // TODO: Initialize to an appropriate value
            CNCRMsgRequestCommands target = new CNCRMsgRequestCommands(msgBytes);
            Assert.AreEqual(CNCRMESSAGE_TYPE.REQUEST_COMMAND, target.getMessageType());
            Assert.AreEqual(0x30, target.getMsgTypeByte());
            Assert.AreEqual(128, target.getCommandCount());
        }
    }
}
