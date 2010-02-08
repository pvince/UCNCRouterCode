using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public class CNCRMsgEStop : CNCRMessage
    {
        public CNCRMsgEStop()
            : base(CNCRMESSAGE_TYPE.E_STOP)
        {
        }

        public override long toSerial()
        {
            // 0010 | 0...0
            return 2^29;
        }
    }
}
