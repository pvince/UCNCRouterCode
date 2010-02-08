using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public class CNCRMsgStartQueue : CNCRMessage
    {
        public CNCRMsgStartQueue()
            : base(CNCRMESSAGE_TYPE.START_QUEUE)
        { }

        public override byte[] toSerial()
        {
            byte[] result = { 0x40, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
