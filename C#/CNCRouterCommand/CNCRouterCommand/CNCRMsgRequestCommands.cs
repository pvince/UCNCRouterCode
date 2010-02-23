using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent by the router to the computer to ask the computer
    /// to send more commands to the router.
    /// 
    /// Command Structure:
    /// [Type 000 P] [#Cmds P] [255]
    /// -     Type: 3
    /// -    #Cmds: 7 bits, gives a range of 1 to 128.
    /// </summary>
    public class CNCRMsgRequestCommands : CNCRMessage
    {
        // Stored internally in the class as 0 to 127 for bit reasons.
        private byte _commandCount = 0;

        /// <summary>
        /// Number of commands to send to the router.  This value can be from 1
        /// to 128
        /// </summary>
        public int getCommandCount()
        {
            return _commandCount + 1;
        }

        private CNCRMsgRequestCommands() : base(CNCRMSG_TYPE.REQUEST_COMMAND) { }

        /// <summary>
        /// Initialize a command request message.
        /// </summary>
        /// <param name="commandCount">Byte, has a range of 1 to 128.</param>
        public CNCRMsgRequestCommands(byte commandCount)
            : this()
        {
            if (commandCount > 128)
                throw new ArgumentOutOfRangeException("commandCount", 
                    "Command count must be 128 or less");
            else if (commandCount < 1)
                commandCount = 1;
            _commandCount = --commandCount; //TODO: CNCRMsgRequestCommands: validate --commandCount works.
        }
        public CNCRMsgRequestCommands(byte[] msgBytes)
            : this()
        {
            // TODO: Validate byte constructor
            // CommandCount is stored in the top 7 bits of the 2nd byte.
            this._commandCount = Convert.ToByte(msgBytes[1] >> 1);
        }

        /// <summary>
        /// Converts message data to a byte array for transfer over serial.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type 000 P] [#Cmds P] [255]
        /// -     Type: 3
        /// -    #Cmds: 7 bits, gives a range of 1 to 128.
        /// </returns>
        public override byte[] toSerial()
        {
            byte Type = getMsgTypeByte();
            byte CmdCount = Convert.ToByte(_commandCount << 1);
            byte[] result = { Type, CmdCount, 0 };   // Build the result array.
            // generate parity
            CNCRTools.generateParity(ref result);
            return result;

        }
    }
}
