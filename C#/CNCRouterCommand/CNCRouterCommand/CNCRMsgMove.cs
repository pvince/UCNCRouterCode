using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    //TODO: CNCRMsgMove: Implement this class (CNCRouterCommand).
    //TODO: CNCRMsgMove: Design this class.
    public class CNCRMsgMove : CNCRMessage
    {
        public CNCRMsgMove()
            : base(CNCRMESSAGE_TYPE.MOVE)
        {
        }

        public override byte[] toSerial()
        {
            byte[] result = { 0x60, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
