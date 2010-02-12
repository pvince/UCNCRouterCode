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
    /// [Type 0000] [255]
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
        /// [Type 0000] [255]
        /// - Type: 4
        /// </returns>
        public override byte[] toSerial()
        {
            byte type = Convert.ToByte(Convert.ToByte(MessageType) << 4);
            byte[] result = { type, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
