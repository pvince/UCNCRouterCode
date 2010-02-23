using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public abstract class CNCRMessage
    {
        private CNCRMSG_TYPE _msgType;
        private byte _msgTypeByte;

        public CNCRMessage(CNCRMSG_TYPE msgType)
        {
            _msgType = msgType;
            _msgTypeByte = Convert.ToByte(Convert.ToByte(_msgType) << 4);
        }

        public abstract byte[] toSerial();

        public CNCRMSG_TYPE getMessageType()
        {
            return _msgType;
        }

        public byte getMsgTypeByte()
        {
            return _msgTypeByte;
        }
    }
}
