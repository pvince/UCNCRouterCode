using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent after the receipt of a command.
    /// 
    /// Command Structure:
    /// [Type Error] [Firmware] [255]
    /// -     Type: 1
    /// -    Error: 4 bits, only the lowest bit really counts.  0 for no error, 1 for error.
    /// - Firmware: 8 bit number
    /// </summary>
    public class CNCRMsgCmdAck : CNCRMessage
    {
        private bool isError = false;
        private byte firmware = 0;

        public CNCRMsgCmdAck()
            : base(CNCRMESSAGE_TYPE.CMD_ACKNOWLEDGE)
        { }

        public CNCRMsgCmdAck(bool isError, byte firmware)
            : base(CNCRMESSAGE_TYPE.CMD_ACKNOWLEDGE)
        {
            this.isError = isError;
            this.firmware = firmware;
        }

        public CNCRMsgCmdAck(byte[] msgBytes)
            : base(CNCRMESSAGE_TYPE.CMD_ACKNOWLEDGE)
        {
            if (msgBytes.Length != 3)
                isError = true; // TODO: CNCRMsgCmdAck: Report an error in size of the message.
            byte errorBit = msgBytes[0] & 1;
            if (errorBit == 1)
                isError = true;

            firmware = msgBytes[1];
        }

        /// <summary>
        /// Gets and sets the error variable for this message.
        /// </summary>
        public bool Error
        {
            get { return isError; }
            set { isError = value; }
        }

        /// <summary>
        /// Gets and sets the firmware value.
        /// </summary>
        public byte Firmware
        {
            get { return firmware; }
            set { firmware = value; }
        }

        /// <summary>
        /// Transfers CNCRMsgCmdAck data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Structure of result:
        ///     0001 | 0001 | 1111 1111
        ///     type | err  | end
        /// </returns>
        public override byte[] toSerial()
        {
            byte TypeAndErr = 0x10; // Set top 4 bits to "0001"
            if (isError)
                TypeAndErr = TypeAndErr | 1;

            byte[] result = { TypeAndErr, firmware, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
