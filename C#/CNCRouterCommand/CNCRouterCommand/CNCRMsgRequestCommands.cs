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
    /// [Type #Cmds] [255]
    /// -     Type: 3
    /// -    #Cmds: 4 bits, interpreted as (#Cmds + 1) * 4, giving a 4 - 64 range
    /// </summary>
    public class CNCRMsgRequestCommands : CNCRMessage
    {
        private byte _commandCount = 0;

        /// <summary>
        /// Number of commands to send to the router.  This value can be from 4 to 64.
        /// </summary>
        public int getCommandCount()
        {
            return (_commandCount + 1) * 4;
        }

        private CNCRMsgRequestCommands() : base(CNCRMESSAGE_TYPE.REQUEST_COMMAND) { }

        public CNCRMsgRequestCommands(byte commandCount)
            : this()
        {
            if (commandCount > 64)
                throw ArgumentOutOfRangeException("commandCount", "Command count must be 64 or less");
            else if (commandCount < 4)
                commandCount = 4;
            _commandCount = (commandCount >> 2) - 1;
            if (_commandCount < 0) _commandCount = 0;
        }
        public CNCRMsgRequestCommands(byte[] msgBytes)
            : this()
        {
            // TODO: Validate byte constructor
            // CommandCount is stored in the lower 4 bits of the first
            // byte.  The number of commands to send is equal to
            // the (sent count + 1) * 4, giving it a range of
            // 4 to 64.
            this._commandCount = Convert.ToByte(msgBytes[0] & 0x0F);
        }

        /// <summary>
        /// Converts message data to a byte array for transfer over serial.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type #Cmds] [255]
        /// -     Type: 3
        /// -    #Cmds: 4 bits, interpreted as (#Cmds + 1) * 4, giving a 4 - 64 range
        /// </returns>
        public override byte[] toSerial()
        {
            byte TypeCmdCount = getMsgTypeByte();
            TypeCmdCount |= _commandCount;           // Store the command count in the lower 4 bits.
            byte[] result = { TypeCmdCount, 255 };  // Build the result array.
            return result;

        }
    }
}
