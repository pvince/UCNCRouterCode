using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgEStopTest and is intended
    ///to contain all CNCRMsgEStopTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgEStopTest
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
            CNCRMsgEStop target = new CNCRMsgEStop(); // TODO: Initialize to an appropriate value
            byte[] expected = { 0x21, 0x21 }; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual<byte>(expected[0], actual[0]);
            Assert.AreEqual<byte>(expected[1], actual[1]);
        }

        /// <summary>
        ///A test for CNCRMsgEStop Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgEStopConstructorTest()
        {
            CNCRMsgEStop target = new CNCRMsgEStop();
            Assert.AreEqual(CNCRMESSAGE_TYPE.E_STOP, target.getMessageType());
            Assert.AreEqual(0x20, target.getMsgTypeByte());
        }
    }
}
