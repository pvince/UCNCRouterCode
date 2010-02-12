using CNCRouterCommand;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNRouterCommand_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgStartQueueTest and is intended
    ///to contain all CNCRMsgStartQueueTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgStartQueueTest
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
            CNCRMsgStartQueue target = new CNCRMsgStartQueue();
            byte[] expected = { 0x40, 255 };
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CNCRMsgStartQueue Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgStartQueueConstructorTest()
        {
            CNCRMsgStartQueue target = new CNCRMsgStartQueue();
            Assert.AreEqual(CNCRMESSAGE_TYPE.START_QUEUE, target.getMessageType());
        }
    }
}
