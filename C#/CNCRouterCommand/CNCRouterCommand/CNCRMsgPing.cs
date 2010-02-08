using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public class CNCRMsgPing : CNCRMessage
    {
        public CNCRMsgPing() : base(CNCRMESSAGE_TYPE.PING)
        {
        }

        public override long toSerial()
        {
            return 0;
        }
    }
}
