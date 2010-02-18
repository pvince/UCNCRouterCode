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

        public void generateParityByte(ref byte[] serialBytes)
        {
            // the result is serialBytes[serialBytes.length - 1]
            byte test = 255;
            BitArray bob = new BitArray(test);
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
            serialByte |= ((numOnes % 2) == 1) ? Convert.ToByte(0) : Convert.ToByte(1);

        }
    }
}
