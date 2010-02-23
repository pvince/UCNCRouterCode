using CNCRouterCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRMsgSetSpeedTest and is intended
    ///to contain all CNCRMsgSetSpeedTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRMsgSetSpeedTest
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
        public void toSerialTest_XY()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, false, 300);
            byte[] expected = { 0x5C, 0x00, 0x2D, 0x05, 0x74 };
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }
        /// <summary>
        ///A test for toSerial
        ///</summary>
        [TestMethod()]
        public void toSerialTest_Z()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(false, false, true, 300);
            byte[] expected = { 0x53, 0x00, 0x2D, 0x05, 0x7B };
            byte[] actual;
            actual = target.toSerial();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for isZ
        ///</summary>
        [TestMethod()]
        public void isZTest_True()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, true, 300);
            bool expected = true;
            bool actual;
            actual = target.isZ();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for isZ
        ///</summary>
        [TestMethod()]
        public void isZTest_False()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, false, 300);
            bool expected = false;
            bool actual;
            actual = target.isZ();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for isY
        ///</summary>
        [TestMethod()]
        public void isYTest_True()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, true, 300);
            bool expected = true;
            bool actual;
            actual = target.isY();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for isY
        ///</summary>
        [TestMethod()]
        public void isYTest_False()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, false, true, 300); 
            bool expected = false; 
            bool actual;
            actual = target.isY();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for isX
        ///</summary>
        [TestMethod()]
        public void isXTest_True()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, true, 300);
            bool expected = true;
            bool actual;
            actual = target.isX();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for isX
        ///</summary>
        [TestMethod()]
        public void isXTest_False()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(false, true, true, 300);
            bool expected = false;
            bool actual;
            actual = target.isX();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getSpeed
        ///</summary>
        [TestMethod()]
        public void getSpeedTest_300()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, true, 300);
            ushort expected = 300;
            ushort actual;
            actual = target.getSpeed();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getSpeed
        ///</summary>
        [TestMethod()]
        public void getSpeedTest_0()
        {
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(true, true, true, 0);
            ushort expected = 0;
            ushort actual;
            actual = target.getSpeed();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CNCRMsgSetSpeed Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgSetSpeedConstructorTest2()
        {
            bool X = false;
            bool Y = false;
            bool Z = false;
            ushort speed = 0;
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(X, Y, Z, speed);
            Assert.AreEqual(X, target.isX());
            Assert.AreEqual(Y, target.isY());
            Assert.AreEqual(Z, target.isZ());
            Assert.AreEqual(speed, target.getSpeed());
            Assert.AreEqual(CNCRMSG_TYPE.SET_SPEED, target.getMessageType());
            Assert.AreEqual(0x50, target.getMsgTypeByte());

        }

        /// <summary>
        ///A test for CNCRMsgSetSpeed Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void CNCRMsgSetSpeedConstructorTest1()
        {
            CNCRMsgSetSpeed_Accessor target = new CNCRMsgSetSpeed_Accessor();
        }

        /// <summary>
        ///A test for CNCRMsgSetSpeed Constructor
        ///</summary>
        [TestMethod()]
        public void CNCRMsgSetSpeedConstructorTest()
        {
            byte[] msgBytes = { 0x5C, 0x00, 0x2D, 0x05, 0x74 };
            CNCRMsgSetSpeed target = new CNCRMsgSetSpeed(msgBytes);
            Assert.AreEqual(true, target.isX());
            Assert.AreEqual(true, target.isY());
            Assert.AreEqual(false, target.isZ());
            Assert.AreEqual(300, target.getSpeed());
            Assert.AreEqual(CNCRMSG_TYPE.SET_SPEED, target.getMessageType());
            Assert.AreEqual(0x50, target.getMsgTypeByte());
        }
    }
}
