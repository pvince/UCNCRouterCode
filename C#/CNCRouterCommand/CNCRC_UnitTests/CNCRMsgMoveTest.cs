using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgMoveTest and is intended
    ///to contain all CNCRMsgMoveTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgMoveTest
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
            CNCRMsgMove target = new CNCRMsgMove(null); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getZ
        ///</summary>
        [TestMethod()]
        public void getZTest()
        {
            CNCRMsgMove target = new CNCRMsgMove(null); // TODO: Initialize to an appropriate value
            short expected = 0; // TODO: Initialize to an appropriate value
            short actual;
            actual = target.getZ();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getY
        ///</summary>
        [TestMethod()]
        public void getYTest()
        {
            CNCRMsgMove target = new CNCRMsgMove(null); // TODO: Initialize to an appropriate value
            short expected = 0; // TODO: Initialize to an appropriate value
            short actual;
            actual = target.getY();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getX
        ///</summary>
        [TestMethod()]
        public void getXTest()
        {
            CNCRMsgMove target = new CNCRMsgMove(null); // TODO: Initialize to an appropriate value
            short expected = 0; // TODO: Initialize to an appropriate value
            short actual;
            actual = target.getX();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CNCRMsgMove Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void CNCRMsgMoveConstructorTest2()
        {
            CNCRMsgMove_Accessor target = new CNCRMsgMove_Accessor();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgMove Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgMoveConstructorTest1()
        {
            byte[] msgBytes = null; // TODO: Initialize to an appropriate value
            CNCRMsgMove target = new CNCRMsgMove(msgBytes);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CNCRMsgMove Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgMoveConstructorTest()
        {
            short X = 0; // TODO: Initialize to an appropriate value
            short Y = 0; // TODO: Initialize to an appropriate value
            short Z = 0; // TODO: Initialize to an appropriate value
            CNCRMsgMove target = new CNCRMsgMove(X, Y, Z);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
