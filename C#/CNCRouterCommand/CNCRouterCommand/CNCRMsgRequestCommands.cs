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
        private byte commandCount = 0;

        /// <summary>
        /// Number of commands to send to the router.  This value can be from 4 to 64.
        /// </summary>
        public int getCommandCount()
        {
            return (commandCount + 1) * 4;
        }

        public CNCRMsgRequestCommands(byte[] msgBytes)
            : base(CNCRMESSAGE_TYPE.REQUEST_COMMAND)
        {
            // TODO: Validate byte constructor
            if (msgBytes.Length != CNCRConstants.MSG_LEN_RQST_COMM)
            {
                // Error: Incorrect length
            }
            else if ((msgBytes[0] & 0xF0) != Convert.ToByte(Convert.ToByte(MessageType) << 4))
            {
                // Error: Incorrect type
            }
            // CommandCount is stored in the lower 4 bits of the first
            // byte.  The number of commands to send is equal to
            // the (sent count + 1) * 4, giving it a range of
            // 4 to 64.
            this.commandCount = Convert.ToByte(msgBytes[0] & 0x0F);
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
            byte TypeCmdCount = Convert.ToByte(Convert.ToByte(MessageType) << 4);
            TypeCmdCount |= commandCount;           // Store the command count in the lower 4 bits.
            byte[] result = { TypeCmdCount, 255 };  // Build the result array.
            return result;

        }
    }
}
