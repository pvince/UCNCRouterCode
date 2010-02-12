using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent to turn the routers tool on and off.
    /// 
    /// Command Structure:
    /// [Type OnOff][255]
    /// -  Type: 7
    /// - OnOff: 0 for off, 1 for on
    /// </summary>
    public class CNCRMsgToolCmd : CNCRMessage
    {
        private bool _toolOn = false;
        public bool isToolOn() { return _toolOn; }
        public void setToolOn(bool toolOn) { _toolOn = toolOn; }

        public CNCRMsgToolCmd(bool toolOn)
            : base(CNCRMESSAGE_TYPE.TOOL_CMD)
        {
            _toolOn = toolOn;
        }

        /// <summary>
        /// Transfers the message data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// [Type OnOff][255]
        /// -  Type: 7
        /// - OnOff: 0 for off, 1 for on
        /// </returns>
        public override byte[] toSerial()
        {
            byte TypeOnOff = getMsgTypeByte();
            if (_toolOn) { TypeOnOff |= 0x01; } // If tool on, set the bit.

            byte[] result = { TypeOnOff, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
