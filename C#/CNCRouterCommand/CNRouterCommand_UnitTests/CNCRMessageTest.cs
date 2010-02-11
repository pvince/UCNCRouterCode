using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNRouterCommand_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMessageTest and is intended
    ///to contain all CNCRMessageTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMessageTest
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
            CNCRMessage target = CreateCNCRMessage(); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual CNCRMessage CreateCNCRMessage()
        {
            // TODO: Instantiate an appropriate concrete class.
            CNCRMessage target = null;
            return target;
        }

        /// <summary>
        ///A test for getMessageType
        ///</summary>
        [TestMethod()]
        public void getMessageTypeTest()
        {
            CNCRMessage target = CreateCNCRMessage(); // TODO: Initialize to an appropriate value
            CNCRMESSAGE_TYPE expected = new CNCRMESSAGE_TYPE(); // TODO: Initialize to an appropriate value
            CNCRMESSAGE_TYPE actual;
            actual = target.MessageType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
