using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgPingTest and is intended
    ///to contain all CNCRMsgPingTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgPingTest
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
            CNCRMsgPing target = new CNCRMsgPing(); // TODO: Initialize to an appropriate value
            byte[] expected = {0x00, 0x00}; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
            
        }

        /// <summary>
        ///A test for CNCRMsgPing Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgPingConstructorTest()
        {
            CNCRMsgPing target = new CNCRMsgPing();
            Assert.AreEqual(CNCRMSG_TYPE.PING, target.getMessageType());
            Assert.AreEqual(0x00, target.getMsgTypeByte());
        }
    }
}
