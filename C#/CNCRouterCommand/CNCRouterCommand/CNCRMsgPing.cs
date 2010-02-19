using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent by the computer to the router.  It is used to ensure
    /// that the router is still connected.
    /// 
    /// Command Structure:
    /// [Type 0000] [Parity]
    /// -   Type: 0
    /// </summary>
    public class CNCRMsgPing : CNCRMessage
    {
        public CNCRMsgPing() : base(CNCRMESSAGE_TYPE.PING)
        { }

        /// <summary>
        /// Transfers message data to a byte array for transfer.
        /// </summary>
        /// <returns>
        ///  Command Structure:
        ///     [Type ----] [Parity]
        ///     [0000 0000] [Parity]
        /// </returns>
        public override byte[] toSerial()
        { 
            // 0000 0000
            byte Type = getMsgTypeByte();
            byte[] result = { Type, 0 };
            CNCRTools.generateParity(ref result);
            return result;
        }
    }
}
