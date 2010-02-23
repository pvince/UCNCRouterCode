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
        public void toSerialTest_ToolOn()
        {
            bool toolOn = true;
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn);
            byte[] expected = { 0x72, 0x72 };
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        /// <summary>
        ///A test for toSerial
        ///</summary>
        [TestMethod()]
        public void toSerialTest_ToolOff()
        {
            bool toolOn = false;
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn);
            byte[] expected = { 0x71, 0x71 };
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        /// <summary>
        ///A test for isToolOn
        ///</summary>
        [TestMethod()]
        public void isToolOnTest_Off()
        {
            bool toolOn = false;
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn);
            bool expected = false;
            bool actual;
            actual = target.isToolOn();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for isToolOn
        ///</summary>
        [TestMethod()]
        public void isToolOnTest_On()
        {
            bool toolOn = true;
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn);
            bool expected = true;
            bool actual;
            actual = target.isToolOn();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CNCRMsgToolCmd Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgToolCmdConstructorTest()
        {
            bool toolOn = false; // TODO: Initialize to an appropriate value
            CNCRMsgToolCmd target = new CNCRMsgToolCmd(toolOn);
            Assert.AreEqual(CNCRMSG_TYPE.TOOL_CMD, target.getMessageType());
            Assert.AreEqual(0x70, target.getMsgTypeByte());
            Assert.AreEqual(toolOn, target.isToolOn());
        }
    }
}
