using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent to tell the router to begin processing
    /// its queue of commands.
    /// 
    /// Command Structure:
    /// [Type 000P] [Parity]
    /// - Type: 4
    /// </summary>
    public class CNCRMsgStartQueue : CNCRMessage
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CNCRMsgStartQueue()
            : base(CNCRMESSAGE_TYPE.START_QUEUE)
        { }

        /// <summary>
        /// Transfers the message data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type 000P] [Parity]
        /// - Type: 4
        /// </returns>
        public override byte[] toSerial()
        {
            byte type = getMsgTypeByte();
            byte[] result = { type, 0 };
            CNCRTools.generateParity(ref result);
            return result;
        }
    }
}
