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
    /// [Type Error P] [Firmware P] [Parity]
    /// -     Type: 1
    /// -    Error: 4 bits, only the lowest bit really counts.  0 for no error, 1 for error.
    /// - Firmware: 7 bit number, number can range from 0 to 127
    /// </summary>
    public class CNCRMsgCmdAck : CNCRMessage
    {
        private bool _isError = false;
        private byte _firmware = 0;

        public bool getError() { return _isError; }
        public byte getFirmware() { return _firmware; }

        #region Constructors
        private CNCRMsgCmdAck()
            : base(CNCRMESSAGE_TYPE.CMD_ACKNOWLEDGE)
        { }

        /// <summary>
        /// Create a new CommandAcknowledge message.
        /// </summary>
        /// <param name="isError">True for there was an error in the previous
        ///                       message.</param>
        /// <param name="firmware">Current FW version.  Max value 254.</param>
        public CNCRMsgCmdAck(bool isError, byte firmware)
            : this()
        {
            if (firmware > 127)
                throw new ArgumentOutOfRangeException("firmware", 
                    "Firmware must be less than 128.");

            this._isError = isError;
            this._firmware = firmware;
        }

        public CNCRMsgCmdAck(byte[] msgBytes)
            : this()
        {
            // TODO: CNCRMsgCmdAck: Validate the passed in msgBytes.
            // Check if the 2nd bit flag is active.
            if ((msgBytes[0] & 0x02) != 0)
                _isError = true;

            // Grab the top 7 bits from the 2nd byte, and shift them right
            // once.
            _firmware = Convert.ToByte((msgBytes[1] & 254) >> 1);
        }
        #endregion


        /// <summary>
        /// Transfers CNCRMsgCmdAck data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Structure of result:
        ///     0001 001 0 | 0000 000 0 | Parity
        ///     type err P | firmware P | Parity
        /// </returns>
        public override byte[] toSerial()
        {
            // Set top 4 bits to "0001"
            byte TypeAndErr = getMsgTypeByte();
            // Set the error flag.
            if (_isError)
                TypeAndErr |= 0x02;

            byte[] result = { TypeAndErr, Convert.ToByte(_firmware << 1), 0};
            
            // Set the parity bits and byte.
            CNCRTools.generateParity(ref result);
            return result;
        }
    }
}
