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
        { }

        public override byte[] toSerial()
        {
            // 0010 0000 | 255
            byte[] result = { 0x20, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
