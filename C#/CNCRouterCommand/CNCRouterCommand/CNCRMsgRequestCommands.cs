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
        // The lowest possible value for CommandCount is 4.
        private Int32 commandCount = 4;

        public Int32 CommandCount
        {
            get { return commandCount; }
        }

        public CNCRMsgRequestCommands(byte[] msgBytes)
            : base(CNCRMESSAGE_TYPE.REQUEST_COMMAND)
        {
            // TODO: Validate msgBytes
            if (msgBytes.Length != CNCRConstants.MSG_LEN_RQST_COMM)
            {
                // Error: Incorrect length
            }
            else if ((msgBytes[0] & 0xF0) != 0x30)
            {
                // Error: Incorrect type
            }
            // CommandCount is stored in the lower 4 bits of the first
            // byte.  The number of commands to send is equal to
            // the (sent count + 1) * 4, giving it a range of
            // 4 to 64.
            this.commandCount = ((msgBytes[0] & 0x0F) + 1) * 4;
        }

        /// <summary>
        /// This message (CNCRMsgRequestCommands) should not really need a "toSerial()" command, however
        /// I am including one because I can, and who knows?
        /// </summary>
        /// <returns></returns>
        public override byte[] toSerial()
        {
            byte tempByte = Convert.ToByte((commandCount / 4) - 1); // Compress command count down to the lower 4 bits.
            tempByte |= 0x30;                       // Store the command type (3) in the upper 4 bits.
            byte[] result = {tempByte, 255};        // Build the result array.
            return result;

        }
    }
}
