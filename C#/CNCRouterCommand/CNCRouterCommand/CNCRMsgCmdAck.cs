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
    /// - Firmware: 8 bit number, may not be 255
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

        public CNCRMsgCmdAck(bool isError, byte firmware)
            : this()
        {
            if (firmware == 255)
                throw new ArgumentOutOfRangeException("firmware", "Firmware may not be equal to 255.");

            this._isError = isError;
            this._firmware = firmware;
        }

        public CNCRMsgCmdAck(byte[] msgBytes)
            : this()
        {
            // TODO: CNCRMsgCmdAck: Validate the passed in msgBytes.
            int errorBit = msgBytes[0] & 0x01;
            if (errorBit == 1)
                _isError = true;

            _firmware = msgBytes[1];
        }
        #endregion


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
            if (_isError)
                TypeAndErr |= 0x01;

            byte[] result = { TypeAndErr, _firmware, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
