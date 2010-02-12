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
            // TODO: CNCRMsgCmdAck: Validate the passed in msgBytes.
            if (msgBytes == null)
                throw new ArgumentNullException("msgBytes", "msgBytes may not be null");
            else if (msgBytes.Length != CNCRConstants.MSG_LEN_CMD_ACK)
                throw new ArgumentOutOfRangeException("msgBytes", "Incorrect number of bytes.");
            else if (((msgBytes[0] & 0xF0) >> 4) != getMsgTypeByte())
                throw new ArgumentException("Passed in msgBytes has the wrong type.", "msgBytes");
            else if ((msgBytes[0] & 0x0F) != 0x00 || (msgBytes[0] & 0x0F) != 0x01)
                throw new ArgumentOutOfRangeException("msgBytes",
                    "Error value in msgBytes is neither 1 or 0");
            else if (msgBytes[2] == CNCRConstants.END_OF_MSG)
                throw new ArgumentException("Message dooes not end in End-of-Message byte", "msgBytes");

            int errorBit = msgBytes[0] & 0x01;
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
        /// Gets and sets the firmware value. Must be less than 255.  If it is
        /// equal to 255 it will be seen as the router as an "End of Message"
        /// byte.
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
            // Set top 4 bits to "0001"
            byte TypeAndErr = getMsgTypeByte();
            if (isError)
                TypeAndErr |= 0x01;

            byte[] result = { TypeAndErr, firmware, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
