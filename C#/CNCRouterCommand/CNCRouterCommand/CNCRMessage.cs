using System;
using System.Collections.Generic;
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
        }
    }
}
