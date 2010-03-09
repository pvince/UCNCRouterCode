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
    /// [Type 00 isStopQueue P] [Parity]
    /// -        Type: 4
    /// - isStopQueue: Set to 1 to indicate an end of the build process.
    /// </summary>
    public class CNCRMsgStartQueue : CNCRMessage
    {
        private bool _isEndQueue = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        private CNCRMsgStartQueue()
            : base(CNCRMSG_TYPE.START_QUEUE)
        { }

        public CNCRMsgStartQueue(bool isEndQueue)
            : this()
        {
            _isEndQueue = isEndQueue;
        }

        public CNCRMsgStartQueue(byte[] msgBytes)
            : this()
        {
            //TODO: CNCRMsgStartQueue: msgBytes constructor needs to validate msgBytes.
            if ((msgBytes[0] & 0x02) != 0)
                _isEndQueue = true;
        }

        public bool isEndQueue()
        {
            return _isEndQueue;
        }
        /// <summary>
        /// Transfers the message data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type 00 isEndQueue P] [Parity]
        /// -        Type: 4
        /// - isStopQueue: Set to 1 to indicate an end of the build process.
        /// </returns>
        public override byte[] toSerial()
        {
            byte typeEndQueue = getMsgTypeByte();
            if (_isEndQueue)
                typeEndQueue |= 0x02; // Set the EndQueue bit.

            byte[] result = { typeEndQueue, 0 };
            CNCRTools.generateParity(ref result);
            return result;
        }
    }
}
