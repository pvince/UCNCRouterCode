using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Collections;

// TODO: Add credit for CodeProject Hex conversion code.
namespace CNCRouterCommand
{
    public static class CNCRTools
    {
        public static string[] GetCommPortList()
        {
            string[] temp = SerialPort.GetPortNames();
            Array.Sort(temp);
            return temp;
        }

        public static string[] GetCNCRouterPorts()
        {
            List<string> results = new List<string>();
            string[] temp = SerialPort.GetPortNames();
            Array.Sort(temp);
            // "Ping" each port and save the ones that "Acknowledge" 
            //    w/ a supported FW version to the "results" list.

            return temp;
        }

        public static string GetCNCRouterVersion(string SerialPortName)
        {
            throw new NotImplementedException();
        }

        #region Byte Conversion Functions
        /// <summary>
        /// Creates an int16 variable from three bytes in a parity byte array.
        /// Requires parityBytes.Length - startIndex to be greater than 3.
        /// </summary>
        /// <param name="parityBytes">Array of parity bytes containing an
        ///                           Int16.</param>
        /// <param name="startIndex">Index of parityBytes to start parsing.</param>
        /// <returns>An int16 contained in parityBytes.</returns>
        public static Int16 generateInt16FromThreeBytes(byte[] parityBytes, int startIndex)
        {
            return BitConverter.ToInt16(generateTwoBytesFromThree(parityBytes, startIndex), 0);
        }

        /// <summary>
        /// Generate three bytes from an int16 variable.
        /// </summary>
        /// <param name="value">Int16 to convert to three bytes.</param>
        /// <returns>Three bytes for an int16.</returns>
        public static byte[] generateThreeBytesFromInt16(Int16 value)
        {
            return generateThreeBytesFromTwo(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Generate three bytes from a Uint16 variable.
        /// </summary>
        /// <param name="value">UInt16 to convert to three bytes.</param>
        /// <returns>Three bytes for a uint16.</returns>
        public static byte[] generateThreeBytesFromUInt16(UInt16 value)
        {
            return generateThreeBytesFromTwo(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Creates a uint16 variable from three bytes in a parity byte array.
        /// Requires parityBytes.Length - startIndex to be greater than 3.
        /// </summary>
        /// <param name="parityBytes">Array of parity bytes containing a
        ///                           UInt16.</param>
        /// <param name="startIndex">Index of parityBytes to start parsing.</param>
        /// <returns>A uint16 contained in parityBytes.</returns>
        public static UInt16 generateUInt16FromThreeBytes(byte[] parityBytes, int startIndex)
        {
            return BitConverter.ToUInt16(generateTwoBytesFromThree(parityBytes, startIndex), 0);
        }

        /// <summary>
        /// Generate three bytes from two bytes.
        /// </summary>
        /// <param name="value">Int16 to convert to parity bytes.</param>
        /// <returns>Parity bytes for an int16.</returns>
        public static byte[] generateThreeBytesFromTwo(byte[] bytes)
        {
            if (bytes.Length != 2)
                throw new ArgumentOutOfRangeException("bytes",
                    "Input byte array must have a length of exactly two.");

            byte[] result = { 0, 0, 0 }; // Takes 3 bytes to hold a paritized int16
            result[0] = Convert.ToByte(bytes[1] & 254); // grab top 7 bits.
            result[1] = Convert.ToByte(bytes[0] & 254); // Grab to 7 bits.
            // Grab the lowest bit for both bytes and place them in the final byte.
            // [0000 0 tB0_lowest tB1_Lowest Parity]
            result[2] = Convert.ToByte(((bytes[1] & 1) << 2) | ((bytes[0] & 1) << 1));
            return result;

        }

        /// <summary>
        /// Condences a 3-byte parity array down to a 2 byte standard array.
        /// Requires parityBytes.Length - startIndex to be greater than 3.
        /// </summary>
        /// <param name="parityBytes">Array containing 3 consequtive bytes
        ///                           that will be condensed down to 2 bytes.</param>
        /// <param name="startIndex">Index of byteArray to start parsing.</param>
        /// <returns>A 2-byte array condensed from byteArray</returns>
        public static byte[] generateTwoBytesFromThree(byte[] byteArray, int startIndex)
        {
            if ((byteArray.Length - startIndex) < 3)
                throw new ArgumentOutOfRangeException("startIndex",
                    "startIndex does not allow for 3 bytes.");

            byte[] result = { 0, 0 };
            // The microcontroller is expecting the bytes to be inverted.
            // The computer writes out          [Lower Bits] [Upper Bits]
            // The microcontroller is expecting [Upper Bits] [Lower Bits]
            result[1] = Convert.ToByte(byteArray[startIndex] & 254); // Grab top 7 bits
            result[0] = Convert.ToByte(byteArray[startIndex + 1] & 254); // Grab top 7 bits
            result[1] |= Convert.ToByte((byteArray[startIndex + 2] & 4) >> 2); // Grab bot bit
            result[0] |= Convert.ToByte((byteArray[startIndex + 2] & 2) >> 1); // Grab bot bit
            return result;
        }
        #endregion

        #region Parity Generation Functions
        public static void generateParity(ref byte[] serialBytes)
        {
            // Look at creating a combined ParityBits parityByte
            generateParityBits(ref serialBytes, serialBytes.Length - 1);
            generateParityByte(ref serialBytes);
        }
        /// <summary>
        /// Generates the final parity byte by counting the number of bits in
        /// each column.
        /// </summary>
        /// <param name="serialBytes">
        /// This parameter is passed by reference, the final byte in the array
        /// is used for the parity byte.  serialBytes must be at least two bytes.
        /// </param>
        public static void generateParityByte(ref byte[] serialBytes)
        {
            //TODO: generateParityByte: Look into generating both the row & column parity bits here.
            if (serialBytes.Length < 2)
                throw new ArgumentOutOfRangeException("serialBytes",
                    "serialBytes must be at least two bytes long.");

            // Set the final byte, the parity byte, to 0.
            int z = serialBytes.Length - 1;
            serialBytes[z] = 0;

            // Create a sliding binary '1' to filter for binary
            // digits in each byte.
            for (UInt16 i = 1; i <= 255; i <<= 1)
            {
                // Count the number of '1' digits in each column.
                // z is the number of actual data bytes (no parity byte)
                int numOnes = 0;
                for (int j = 0; j < z; j++)
                {
                    if ((serialBytes[j] & i) != 0)
                        numOnes++;
                }
                // Check to see if numOnes is odd, if so ignore it (its already 0)
                //              if numOnes is even, set that bit to 1.
                if ((numOnes & 1) == 1)
                {
                    serialBytes[z] |= Convert.ToByte(i);
                }
            }
        }

        /// <summary>
        /// Takes an array of bytes and generates their parity bits.
        /// </summary>
        /// <param name="serialBytes">Array of bytes to generate parity bits for.</param>
        private static void generateParityBits(ref byte[] serialBytes, int numberBytes)
        {
            for (int i = 0; i < numberBytes; i++)
            {
                generateParityBit(ref serialBytes[i]);
            }
        }

        /// <summary>
        /// Takes an array of bytes and generates their parity bits.
        /// </summary>
        /// <param name="serialBytes">Array of bytes to generate parity bits for.</param>
        public static void generateParityBits(ref byte[] serialBytes)
        {
            generateParityBits(ref serialBytes, serialBytes.Length);
        }

        /// <summary>
        /// Counts the number of '1' bits in the passed in byte, then fills in
        /// the final parity bit.
        /// 
        /// Odd number of 1's  = 0 parity
        /// Even number of 1's = 1 parity
        /// </summary>
        /// <param name="serialByte">Byte to generate and fill parity.</param>
        public static void generateParityBit(ref byte serialByte)
        {
            int numOnes = 0;
            // Clear the lowest bit.
            serialByte = Convert.ToByte(serialByte & 254);
            // Set the temp byte.
            byte tempByte = serialByte;
            // Count the 1 bits
            while (tempByte != 0)
            {
                numOnes += tempByte & 0x1;
                tempByte >>= 1;
            }
            // Set the parity bit.
            // If numOnes is odd
            serialByte |= Convert.ToByte(numOnes & 1);
        }
        #endregion

        #region Parity Validation Functions
        /// <summary>
        /// Checks the value of the parity bit.  Returns false if the check fails.
        /// </summary>
        /// <param name="serialByte">Serial Byte to validate.</param>
        /// <returns>True if check succeeds, false if check fails.</returns>
        public static bool validateParityBit(byte serialByte)
        {
            // Create copy of the passed in byte.
            byte tempByte = serialByte;
            // Generate the parity for the received byte.
            generateParityBit(ref tempByte);
            // Validate the generated is the same as the received.
            if (serialByte != tempByte)
                return false;   // if it is not, fail the parity check.

            return true;    // Passed parity check.
        }

        /// <summary>
        /// Checks the value of all of the parity bits in the passed in array.
        /// Array must be at least two bytes long.  Returns true if it passes
        /// the parity check.
        /// </summary>
        /// <param name="serialBytes">Array of serial data with parity bits.
        /// Must be at least two bytes long.</param>
        /// <returns>True if it passes the parity check, false if it fails.</returns>
        public static bool validateParityBytes(byte[] serialBytes)
        {
            //TODO: validateParityBytes: Validate that this works.
            // serialBytes must be at least two bytes long.
            if (serialBytes.Length < 2)
                throw new ArgumentOutOfRangeException("serialBytes",
                    "serialBytes must be at least two bytes long.");

            // Create a copy of the passed in serial data.
            byte[] tempBytes = new byte[serialBytes.Length];
            Array.Copy(serialBytes, tempBytes, serialBytes.Length);

            // Validate the parity values of each byte in the array.
            // (length - 1 because last byte is a parity byte)
            for (int i = 0; i < serialBytes.Length - 1; i++)
            {
                if (!validateParityBit(serialBytes[i])) // Check the parity of the byte.
                    return false; // Parity check failed.
            }

            // Re-generate the parity byte on the copy.
            generateParityByte(ref tempBytes);

            // Validate the generated = the received
            if (tempBytes[tempBytes.Length - 1] != serialBytes[serialBytes.Length - 1])
                return false; // Parity check failed.

            return true; // Parity check passed.
        }

        //TODO: validateParityByte - Write test case.
        /// <summary>
        /// Validates ONLY the parity byte of a serialData array.
        /// </summary>
        /// <param name="serialBytes">Array of serial data for which to validate
        /// the parity byte.</param>
        /// <returns>True if the parity byte passes the check.</returns>
        public static bool validateParityByte(byte[] serialBytes)
        {
            // Create a copy of the passed in array.
            byte[] tempBytes = new byte[serialBytes.Length];
            Array.Copy(serialBytes, tempBytes, serialBytes.Length);

            // Generate the parity byte for the copy.
            generateParityByte(ref tempBytes);

            // Check to make sure copy and original have same parity byte.
            if (serialBytes[serialBytes.Length - 1] != tempBytes[tempBytes.Length - 1])
                return false;
            return true;
        }

        #endregion

        #region Message Handling Tools
        //TODO: getMsgLenFromType: check test runs for this function
        /// <summary>
        /// Returns the expected byte message length for a specific message type.
        /// </summary>
        /// <param name="msgType">Message type to find length of.</param>
        /// <returns>The expected byte length of the passed in message type.</returns>
        public static int getMsgLenFromType(CNCRMSG_TYPE msgType)
        {
            switch (msgType)
            {
                case CNCRMSG_TYPE.CMD_ACKNOWLEDGE:
                    return CNCRConstants.MSG_LEN_CMD_ACK;
                case CNCRMSG_TYPE.E_STOP:
                    return CNCRConstants.MSG_LEN_ESTOP;
                case CNCRMSG_TYPE.MOVE:
                    return CNCRConstants.MSG_LEN_MOVE;
                case CNCRMSG_TYPE.PING:
                    return CNCRConstants.MSG_LEN_PING;
                case CNCRMSG_TYPE.REQUEST_COMMAND:
                    return CNCRConstants.MSG_LEN_RQST_COMM;
                case CNCRMSG_TYPE.SET_SPEED:
                    return CNCRConstants.MSG_LEN_SETSPD;
                case CNCRMSG_TYPE.START_QUEUE:
                    return CNCRConstants.MSG_LEN_STARTQ;
                case CNCRMSG_TYPE.TOOL_CMD:
                    return CNCRConstants.MSG_TOOL_CMD;
                default:
                    throw new ArgumentException("msgType of "
                        + msgType.ToString() + " has no defined length.",
                        "msgType");
            }
        }

        //TODO: Should this be in CNCRMessage?
        //TODO: getMsgFromBytes: Generate more test cases for this function, preferable edge cases.
        /// <summary>
        /// Returns the message contained in the passed in byte array.
        /// </summary>
        /// <param name="msgBytes">Array of bytes containing a CNCRMessage</param>
        /// <returns>The message contained in the bytes.</returns>
        public static CNCRMessage getMsgFromBytes(byte[] msgBytes)
        {
            // Byte 0 should be 
            CNCRMSG_TYPE msgType = (CNCRMSG_TYPE)Enum.ToObject(typeof(CNCRMSG_TYPE), (msgBytes[0] & 0xF0) >> 4);
            int msgLen = getMsgLenFromType(msgType);

            // Validate the message length.
            if (msgLen != msgBytes.Length)
                throw new RankException("MsgCommandAcknowledge is "
                    + CNCRConstants.MSG_LEN_CMD_ACK + " not "
                    + msgBytes.Length + " bytes long.");

            // Build the correct message.
            CNCRMessage resultMsg;
            switch (msgType)
            {
                case CNCRMSG_TYPE.CMD_ACKNOWLEDGE:
                    resultMsg = new CNCRMsgCmdAck(msgBytes);
                    break;
                case CNCRMSG_TYPE.E_STOP:
                    resultMsg = new CNCRMsgEStop();
                    break;
                case CNCRMSG_TYPE.MOVE:
                    resultMsg = new CNCRMsgMove(msgBytes);
                    break;
                case CNCRMSG_TYPE.PING:
                    resultMsg = new CNCRMsgPing();
                    break;
                case CNCRMSG_TYPE.REQUEST_COMMAND:
                    resultMsg = new CNCRMsgRequestCommands(msgBytes);
                    break;
                case CNCRMSG_TYPE.SET_SPEED:
                    resultMsg = new CNCRMsgSetSpeed(msgBytes);
                    break;
                case CNCRMSG_TYPE.START_QUEUE:
                    resultMsg = new CNCRMsgStartQueue(msgBytes);
                    break;
                case CNCRMSG_TYPE.TOOL_CMD:
                    resultMsg = new CNCRMsgToolCmd(msgBytes);
                    break;
                default:
                    throw new FormatException("getMsgFromBytes: Unknown message type");
            }
            return resultMsg;
        }
        #endregion

        #region General Tools
        /// <summary>
        /// Creates a byte array from the hexadecimal string. Each two characters are combined
        /// to create one byte. First two hexadecimal characters become first byte in returned array.
        /// Non-hexadecimal characters are ignored. 
        /// </summary>
        /// <param name="hexString">string to convert to byte array</param>
        /// <param name="discarded">number of characters in string ignored</param>
        /// <returns>byte array, in the same left-to-right order as the hexString</returns>
        public static byte[] GetBytes(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }
            // if odd number of characters, pad start with a 0
            if (newString.Length % 2 != 0)
            {
                newString = "0" + newString;
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }

        /// <summary>
        /// Converts a byte array to a hex string.
        /// </summary>
        /// <param name="bytes">Array of bytes to convert.</param>
        /// <returns>String of 0-9, A-F characters representing a Hex number.</returns>
        public static string BytesToHex(byte[] bytes)
        {
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            return hexString;
        }

        /// <summary>
        /// Returns true is c is a hexadecimal digit (A-F, a-f, 0-9)
        /// </summary>
        /// <param name="c">Character to test</param>
        /// <returns>true if hex digit, false if not</returns>
        private static bool IsHexDigit(Char c)
        {
            int numChar;
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
                return true;
            if (numChar >= num1 && numChar < (num1 + 10))
                return true;
            return false;
        }

        //TODO: HexToByte check test runs for this function.
        /// <summary>
        /// Converts 1 or 2 character string into equivalant byte value
        /// </summary>
        /// <param name="hex">1 or 2 character string</param>
        /// <returns>byte</returns>
        private static byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }

        public static double getAngleFromLines(int x11, int y11, int x12, int y12, int x21, int y21, int x22, int y22)
        {
            Double radianAngle = Math.Atan2(y12 - y11, x12 - x11)
                                    - Math.Atan2(y22 - y21, x22 - x21);
            Double degreeAngle = (radianAngle * (180 / Math.PI));
            return degreeAngle;
        }
        #endregion

        #region GCode Parsing
        public static string readTextFile(string path)
        {
            TextReader tr = new StreamReader(path);
            string result = tr.ReadToEnd();
            tr.Close();
            return result;
        }

        public static Queue<CNCRMessage> parseGCode(string gcode, ref string LogMessages)
        {
            // Split the gCode into liines
            char[] charDelimiters = { '\r', '\n' };
            string[] gcodeLines = gcode.Split(charDelimiters,
                                        StringSplitOptions.RemoveEmptyEntries);

            // Split the lines into words
            List<string[]> gcodeLineWords = new List<string[]>();
            for (int i = 0; i < gcodeLines.Length; i++)
            {
                string[] lineSplit = gcodeLines[i].Split(null);

                bool inComment = false;
                string curCodeLetter = "";
                int curCodeNumber = -1;

                for (int j = 0; j < lineSplit.Length; j++)
                {
                    // Check if we are starting a comment.
                    if (lineSplit[j].StartsWith("("))
                    {
                        // If so, mark that we are in a comment and start discarding.
                        inComment = true;
                        LogMessages += "Line " + (i + 1) + ": Discarding Comment.\n";
                    }

                    // Check if we are out of a comment.
                    if (lineSplit[j].Contains(")"))
                        inComment = false;  // Stop discarding.
                    else if (!inComment)
                    {
                        if (curCodeLetter == "")
                        {
                            curCodeLetter = lineSplit[j].Substring(0, 1);
                            curCodeNumber = Int32.Parse(lineSplit[j].Substring(1));
                            continue;
                        }

                        switch (curCodeLetter)
                        {
                            case "G":
                                switch (curCodeNumber)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        break;
                                    case 2:
                                        break;
                                    case 3:
                                        break;
                                    default:
                                        // Log the error.
                                        LogMessages += "Line " + (i + 1) + ": Error:" +
                                            " Unknown command '" + curCodeLetter +
                                            curCodeNumber + "'\n";

                                        // Clear the current codes
                                        curCodeLetter = "";
                                        curCodeNumber = -1;
                                        break;
                                }
                                break;
                            case "T":
                                break;
                            case "M":
                                break;
                            default:
                                // Log an error.
                                LogMessages += "Line " + (i + 1) + ": Error: Unknown command letter '" + curCodeLetter + "'.\n";

                                // Clear the current codes
                                curCodeLetter = "";
                                curCodeNumber = -1;
                                break;
                        }
                    }
                }

                // Do other parsing things
                //lineSplit[j] will be of the format [Letter][Number]
                // - [Letter] can be G, T, M, S, X, Y, Z, I, J, K, F
                // - [Number] can be either an int or a float.
                // We need a flow chart, G -> G# -> parameters for G# <- Back to start
                //                                  -> X -> X# -> Set new X coord
                //                                  -> Y -> Y# -> Set new Y Coord
                //                                  -> F -> F# -> Add set Speed command
                //                                             -> Add Move command
                //                            <------------------
                /*
                // Discard comment lines, more complicated than this, comments can be anywhere.
                if (lineSplit[0].StartsWith("("))
                {
                    LogMessages += "Line " + (i + 1) + ": Discarding Comment.\n";
                }
                else
                {
                    gcodeLineWords.Add(lineSplit);
                }//*/
            }

            Queue<CNCRMessage> testbob = null;
            return testbob;
        }
        #endregion
    }
}
