using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public class CNCRMsgPing : CNCRMessage
    {
        public CNCRMsgPing() : base(CNCRMESSAGE_TYPE.PING)
        { }

        public override byte[] toSerial()
        { 
            // 0000 0000
            byte Type = Convert.ToByte(Convert.ToByte(MessageType) << 4);
            byte[] result = { Type, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
