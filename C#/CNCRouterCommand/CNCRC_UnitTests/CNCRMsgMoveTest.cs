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
            CNCRMsgMove target = new CNCRMsgMove(-32768, 32767, 24155);
            byte[] expected = {0x60, 0x81, 0x00, 0x00, 0x7E, 0xFF, 0x06, 0x5F, 0x5A, 0x03, 0x60};
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i], "Byte " + i);
            }
        }

        /// <summary>
        ///A test for getZ
        ///</summary>
        [TestMethod()]
        public void getZTest()
        {
            CNCRMsgMove target = new CNCRMsgMove(-32768, 32767, 24155);
            short expected = 24155;
            short actual;
            actual = target.getZ();
            Assert.AreEqual<short>(expected, actual);
        }

        /// <summary>
        ///A test for getY
        ///</summary>
        [TestMethod()]
        public void getYTest()
        {
            CNCRMsgMove target = new CNCRMsgMove(-32768, 32767, 24155);
            short expected = 32767;
            short actual;
            actual = target.getY();
            Assert.AreEqual<short>(expected, actual);
        }

        /// <summary>
        ///A test for getX
        ///</summary>
        [TestMethod()]
        public void getXTest()
        {
            CNCRMsgMove target = new CNCRMsgMove(-32768, 32767, 24155);
            short expected = -32768;
            short actual;
            actual = target.getX();
            Assert.AreEqual<short>(expected, actual);
        }

        /// <summary>
        ///A test for CNCRMsgMove Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void CNCRMsgMoveConstructorTest2()
        {
            CNCRMsgMove_Accessor target = new CNCRMsgMove_Accessor();
        }

        /// <summary>
        ///A test for CNCRMsgMove Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgMoveConstructorTest1()
        {
            byte[] msgBytes = { 0x60, 0x81, 0x00, 0x00, 0x7E, 0xFF, 0x06, 0x5F, 0x5A, 0x03, 0x60 };
            CNCRMsgMove target = new CNCRMsgMove(msgBytes);
            Assert.AreEqual <CNCRMESSAGE_TYPE>(CNCRMESSAGE_TYPE.MOVE, target.getMessageType());
            Assert.AreEqual<byte>(0x60, target.getMsgTypeByte());
            Assert.AreEqual<short>(-32768, target.getX());
            Assert.AreEqual<short>(32767, target.getY());
            Assert.AreEqual<short>(24155, target.getZ());
        }

        /// <summary>
        ///A test for CNCRMsgMove Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgMoveConstructorTest()
        {
            //-32768, 32767, 24155
            short X = -32768; // TODO: Initialize to an appropriate value
            short Y = 32767; // TODO: Initialize to an appropriate value
            short Z = 24155; // TODO: Initialize to an appropriate value
            CNCRMsgMove target = new CNCRMsgMove(X, Y, Z);
            Assert.AreEqual<CNCRMESSAGE_TYPE>(CNCRMESSAGE_TYPE.MOVE, target.getMessageType());
            Assert.AreEqual<byte>(0x60, target.getMsgTypeByte());
            Assert.AreEqual<short>(-32768, target.getX());
            Assert.AreEqual<short>(32767, target.getY());
            Assert.AreEqual<short>(24155, target.getZ());
        }
    }
}
