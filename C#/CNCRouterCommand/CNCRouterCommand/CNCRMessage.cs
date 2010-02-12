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

        //TODO: Change MessageType to getMessageType()
        public CNCRMESSAGE_TYPE MessageType
        {
            get { return _msgType; }
        }

        //TODO: Make messages use this instead of doing the conversion every time.
        public byte getMsgTypeByte()
        {
            return _msgTypeByte;
        }
    }
}
