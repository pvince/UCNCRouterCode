using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public abstract class CNCRMessage
    {
        private CNCRMESSAGE_TYPE _msgType;
        private byte _msgTypeByte;

        public CNCRMessage(CNCRMESSAGE_TYPE msgType)
        {
            _msgType = msgType;
            _msgTypeByte = Convert.ToByte(Convert.ToByte(_msgType) << 4);
        }

        public abstract byte[] toSerial();

        public CNCRMESSAGE_TYPE getMessageType()
        {
            return _msgType;
        }

        public byte getMsgTypeByte()
        {
            return _msgTypeByte;
        }

        private byte[] getParityBytes(byte[] serialBytes)
        {
            // newByteCount =   the number of additional bytes needed for a parity
            //                  msg.  Every 8 bytes adds an additional byte to the
            //                  parity result.  It is +2 because we need at least
            //                  one new byte because of the shifting 
            // Hmm, this function is fail.  What about the type bit? Questions.
            int newByteCount = (serialBytes.Length / 8 + 2);
            byte[] result = new byte[serialBytes.Length + newByteCount];
            for (int i = 0; i < result.Length; i++)
            {

            }
            return result;
        }

        /// <summary>
        /// Generates the final parity byte by counting the number of bits in
        /// each column.
        /// </summary>
        /// <param name="serialBytes">
        /// This parameter is passed by reference, the final byte in the array
        /// is used for the parity byte.  serialBytes must be at least two bytes.
        /// </param>
        public void generateParityByte(ref byte[] serialBytes)
        {
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
                        ++numOnes;
                }
                // Check to see if numOnes is odd, if so ignore it (its already 0)
                //              if numOnes is even, set that bit to 1.
                if ((numOnes & 1) == 0)
                {
                    serialBytes[z] |= Convert.ToByte(i);
                }
            }
        }

        /// <summary>
        /// Counts the number of '1' bits in the passed in byte, then fills in
        /// the final parity bit.
        /// 
        /// Odd number of 1's  = 0 parity
        /// Even number of 1's = 1 parity
        /// </summary>
        /// <param name="serialByte">Byte to generate and fill parity.</param>
        public void generateParityBit(ref byte serialByte)
        {
            int numOnes = 0;
            // Clear the lowest bit.
            serialByte = Convert.ToByte( serialByte & 254);
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
            serialByte |= ((numOnes & 1) == 1) ? Convert.ToByte(0) : Convert.ToByte(1);

        }
    }
}
