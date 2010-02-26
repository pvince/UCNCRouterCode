using CNCRouterCommand;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CNCRC_UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CNCRToolsTest and is intended
    ///to contain all CNCRToolsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CNCRToolsTest
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
        ///A test for validateParityBytes
        ///</summary>
        [TestMethod()]
        public void validateParityBytesTest_Valid()
        {
            byte[] serialBytes = { 3, 90, 89};
            bool expected = true;
            bool actual;
            actual = CNCRTools.validateParityBytes(serialBytes);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for validateParityBytes
        ///</summary>
        [TestMethod()]
        public void validateParityBytesTest_InvalidFinalByte()
        {
            byte[] serialBytes = { 3, 90, 0 };
            bool expected = false;
            bool actual;
            actual = CNCRTools.validateParityBytes(serialBytes);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for validateParityBit
        ///</summary>
        [TestMethod()]
        public void validateParityBitTest_Valid()
        {
            byte serialByte = 255;
            bool expected = true;
            bool actual;
            actual = CNCRTools.validateParityBit(serialByte);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for validateParityBit
        ///</summary>
        [TestMethod()]
        public void validateParityBitTest_Invalid()
        {
            byte serialByte = 254;
            bool expected = false;
            bool actual;
            actual = CNCRTools.validateParityBit(serialByte);
            Assert.AreEqual(expected, actual);
        }

        //TODO: Implement these tests.
        /*
        /// <summary>
        ///A test for GetCommPortList
        ///</summary>
        [TestMethod()]
        public void GetCommPortListTest()
        {
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = CNCRTools.GetCommPortList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCNCRouterVersion
        ///</summary>
        [TestMethod()]
        public void GetCNCRouterVersionTest()
        {
            string SerialPortName = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = CNCRTools.GetCNCRouterVersion(SerialPortName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCNCRouterPorts
        ///</summary>
        [TestMethod()]
        public void GetCNCRouterPortsTest()
        {
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = CNCRTools.GetCNCRouterPorts();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        //*/
        /// <summary>
        ///A test for generateUInt16FromThreeBytes
        ///</summary>
        [TestMethod()]
        public void generateUInt16FromThreeBytesTest_Valid()
        {
            byte[] parityBytes = { 0, 170, 170, 0 };
            int startIndex = 1;
            ushort expected = 43690;
            ushort actual;
            actual = CNCRTools.generateUInt16FromThreeBytes(parityBytes, startIndex);
            Assert.AreEqual<ushort>(expected, actual);
        }

        /// <summary>
        ///A test for generateUInt16FromThreeBytes
        ///</summary>
        [TestMethod()]
        public void generateUInt16FromThreeBytesTest_ValidComplex()
        {
            //TODO: Validate this test.
            byte[] parityBytes = { 0, 0xCA, 0x53, 4 };
            int startIndex = 1;
            ushort expected = 52050;
            ushort actual;
            actual = CNCRTools.generateUInt16FromThreeBytes(parityBytes, startIndex);
            Assert.AreEqual<ushort>(expected, actual);
        }

        /// <summary>
        ///A test for generateUInt16FromThreeBytes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException), 
            "An out of bound startIndex was allowed")]
        public void generateUInt16FromThreeBytesTest_BadStart()
        {
            byte[] parityBytes = { 0, 170, 170, 0 };
            int startIndex = 2;
            ushort actual;
            actual = CNCRTools.generateUInt16FromThreeBytes(parityBytes, startIndex);
        }

        /// <summary>
        ///A test for generateUInt16FromThreeBytes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "An out of bound startIndex was allowed")]
        public void generateUInt16FromThreeBytesTest_ShortArray()
        {
            byte[] parityBytes = { 0, 170 };
            int startIndex = 0;
            ushort actual;
            actual = CNCRTools.generateUInt16FromThreeBytes(parityBytes, startIndex);
        }

        /// <summary>
        ///A test for generateTwoBytesFromThree
        ///</summary>
        [TestMethod()]
        public void generateTwoBytesFromThreeTest_Valid()
        {
            byte[] byteArray = { 0, 170, 170, 0 };
            int startIndex = 1;
            byte[] expected = {170, 170};
            byte[] actual;
            actual = CNCRTools.generateTwoBytesFromThree(byteArray, startIndex);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for generateTwoBytesFromThree
        ///</summary>
        [TestMethod()]
        public void generateTwoBytesFromThreeTest_ValidComplex()
        {
            byte[] byteArray = { 0, 0xCA, 0x53, 4 };
            int startIndex = 1;
            byte[] expected = { 0x52, 0xCB };
            byte[] actual;
            actual = CNCRTools.generateTwoBytesFromThree(byteArray, startIndex);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for generateTwoBytesFromThree
        ///</summary>
        [TestMethod()]
        public void generateTwoBytesFromThreeTest_InvalidComplex()
        {
            byte[] byteArray = { 0, 0xCA, 0x53, 4 };
            int startIndex = 1;
            byte[] expected = { 0xCB, 0x52 };
            byte[] actual;
            actual = CNCRTools.generateTwoBytesFromThree(byteArray, startIndex);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreNotEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for generateTwoBytesFromThree
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "An out of bound startIndex was allowed")]
        public void generateTwoBytesFromThreeTest_InvalidIndex()
        {
            byte[] byteArray = { 0, 0xCA, 0x53, 4 };
            int startIndex = 2;
            byte[] actual;
            actual = CNCRTools.generateTwoBytesFromThree(byteArray, startIndex);
        }

        /// <summary>
        ///A test for generateThreeBytesFromUInt16
        ///</summary>
        [TestMethod()]
        public void generateThreeBytesFromUInt16Test()
        {
            ushort value = 52050;
            byte[] expected = { 0xCA, 0x52, 4 };
            byte[] actual;
            actual = CNCRTools.generateThreeBytesFromUInt16(value);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for generateThreeBytesFromTwo
        ///</summary>
        [TestMethod()]
        public void generateThreeBytesFromTwoTest()
        {
            byte[] bytes = { 0x52, 0xCB }; // 52050, result of Bitconvert.getBytes()
            byte[] expected = { 0xCA, 0x52, 4 };
            byte[] actual;
            actual = CNCRTools.generateThreeBytesFromTwo(bytes);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for generateThreeBytesFromInt16
        ///</summary>
        [TestMethod()]
        public void generateThreeBytesFromInt16Test()
        {
            short value = 25000;
            byte[] expected = {0x60, 0xA8, 4};
            byte[] actual;
            actual = CNCRTools.generateThreeBytesFromInt16(value);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }

        /// <summary>
        ///A test for generateParityByte
        ///</summary>
        [TestMethod()]
        public void generateParityByteTest()
        {
            byte[] serialBytes = { 0x33, 0xFF, 0xBE, 0x06, 0 };
            byte[] serialBytesExpected = { 0x33, 0xFF, 0xBE, 0x06, 0x74 };
            CNCRTools.generateParityByte(ref serialBytes);
            for (int i = 0; i < serialBytesExpected.Length; i++)
            {
                Assert.AreEqual<byte>(serialBytesExpected[i], serialBytes[i]);
            }
        }

        /// <summary>
        ///A test for generateParityBits
        ///</summary>
        [TestMethod()]
        public void generateParityBitsTest1()
        {
            byte[] serialBytes = {232, 0, 2, 254, 6 };
            byte[] serialBytesExpected = {232, 0, 3, 255, 6};
            CNCRTools.generateParityBits(ref serialBytes);
            for (int i = 0; i < serialBytesExpected.Length; i++)
            {
                Assert.AreEqual<byte>(serialBytesExpected[i], serialBytes[i]);
            }
        }

        /// <summary>
        ///A test for generateParityBit
        ///</summary>
        [TestMethod()]
        public void generateParityBitTest_Valid1()
        {
            byte serialByte = 234;
            byte serialByteExpected = 235;
            CNCRTools.generateParityBit(ref serialByte);
            Assert.AreEqual(serialByteExpected, serialByte);
        }

        /// <summary>
        ///A test for generateParityBit
        ///</summary>
        [TestMethod()]
        public void generateParityBitTest_Valid1_2()
        {
            byte serialByte = 254;
            byte serialByteExpected = 255;
            CNCRTools.generateParityBit(ref serialByte);
            Assert.AreEqual(serialByteExpected, serialByte);
        }

        /// <summary>
        ///A test for generateParityBit
        ///</summary>
        [TestMethod()]
        public void generateParityBitTest_Valid0()
        {
            byte serialByte = 232;
            byte serialByteExpected = 232;
            CNCRTools.generateParityBit(ref serialByte);
            Assert.AreEqual(serialByteExpected, serialByte);
        }

        /// <summary>
        ///A test for generateParity
        ///</summary>
        [TestMethod()]
        public void generateParityTest()
        {
            byte[] serialBytes = {0x32, 0xFE, 0xBE, 0x06, 0};
            byte[] serialBytesExpected = {0x33, 0xFF, 0xBE, 0x06, 0x74 };
            CNCRTools.generateParity(ref serialBytes);
            for (int i = 0; i < serialBytesExpected.Length; i++)
            {
                Assert.AreEqual<byte>(serialBytesExpected[i], serialBytes[i]);
            }
        }

        /// <summary>
        ///A test for generateInt16FromThreeBytes
        ///</summary>
        [TestMethod()]
        public void generateInt16FromThreeBytesTest()
        {
            byte[] parityBytes = { 0, 0x60, 0xA8, 4, 0 };
            int startIndex = 1;
            short expected = 25000;
            short actual;
            actual = CNCRTools.generateInt16FromThreeBytes(parityBytes, startIndex);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for generateInt16FromThreeBytes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "An out of bound startIndex was allowed")]
        public void generateInt16FromThreeBytesTest_BadIndex()
        {
            byte[] parityBytes = { 0, 0x60, 0xA8, 4, 0 };
            int startIndex = 3;
            short expected = 25000;
            short actual;
            actual = CNCRTools.generateInt16FromThreeBytes(parityBytes, startIndex);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            byte[] bytes = {0xFF, 0x01};
            string expected = "FF01";
            string actual;
            actual = CNCRTools.BytesToHex(bytes);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsHexDigit
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void IsHexDigitTest_valid()
        {
            char c = 'A';
            bool expected = true;
            bool actual;
            actual = CNCRTools_Accessor.IsHexDigit(c);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsHexDigit
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void IsHexDigitTest_Invalid()
        {
            char c = 'G';
            bool expected = false;
            bool actual;
            actual = CNCRTools_Accessor.IsHexDigit(c);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsHexDigit
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void IsHexDigitTest_ValidDigit()
        {
            char c = '1';
            bool expected = true;
            bool actual;
            actual = CNCRTools_Accessor.IsHexDigit(c);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsHexDigit
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void IsHexDigitTest_ValidA()
        {
            char c = 'a';
            bool expected = true;
            bool actual;
            actual = CNCRTools_Accessor.IsHexDigit(c);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HexToByte
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void HexToByteTest()
        {
            string hex = "FE";
            byte expected = 254;
            byte actual;
            actual = CNCRTools_Accessor.HexToByte(hex);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for HexToByte
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CNCRouterCommand.exe")]
        public void HexToByteTest_ValidSingleDigit()
        {
            string hex = "F";
            byte expected = 15;
            byte actual;
            actual = CNCRTools_Accessor.HexToByte(hex);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getMsgLenFromType
        ///</summary>
        [TestMethod()]
        public void getMsgLenFromTypeTest()
        {
            CNCRMSG_TYPE msgType = CNCRMSG_TYPE.MOVE;
            int expected = 11;
            int actual;
            actual = CNCRTools.getMsgLenFromType(msgType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getMsgFromBytes
        ///</summary>
        [TestMethod()]
        public void getMsgFromBytesTest()
        {
            byte[] msgBytes = {0x5C, 0x00, 0x2D, 0x05, 0x74};
            CNCRMessage expected = new CNCRMsgSetSpeed(msgBytes);
            CNCRMessage actual;
            actual = CNCRTools.getMsgFromBytes(msgBytes);
            Assert.AreEqual<CNCRMSG_TYPE>(expected.getMessageType(), actual.getMessageType());
            Assert.AreEqual(((CNCRMsgSetSpeed)expected).isX(), ((CNCRMsgSetSpeed)actual).isX());
            Assert.AreEqual(((CNCRMsgSetSpeed)expected).isY(), ((CNCRMsgSetSpeed)actual).isY());
            Assert.AreEqual(((CNCRMsgSetSpeed)expected).isZ(), ((CNCRMsgSetSpeed)actual).isZ());
            Assert.AreEqual(((CNCRMsgSetSpeed)expected).getSpeed(), ((CNCRMsgSetSpeed)actual).getSpeed());
        }

        /// <summary>
        ///A test for GetBytes
        ///</summary>
        [TestMethod()]
        public void GetBytesTest()
        {
            string hexString = "FF 01";
            int discarded = 0;
            int discardedExpected = 1;
            byte[] expected = {0xFF, 0x01};
            byte[] actual;
            actual = CNCRTools.GetBytes(hexString, out discarded);
            Assert.AreEqual(discardedExpected, discarded);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<byte>(expected[i], actual[i]);
            }
        }
    }
}
