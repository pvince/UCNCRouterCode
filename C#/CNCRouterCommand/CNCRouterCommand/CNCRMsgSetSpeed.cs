using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    //TODO: CNCRMsgSetSpeed: Z-speed, X-speed, Y-speed? we should somehow denote the axis.
    /// <summary>
    /// Message object sent to router to change the feedrate.
    /// </summary>
    public class CNCRMsgSetSpeed : CNCRMessage
    {
        private byte _speed = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CNCRMsgSetSpeed()
            : base(CNCRMESSAGE_TYPE.SET_SPEED)
        {
        }

        /// <summary>
        /// This constructure allows you to specify the speed upon creation.
        /// </summary>
        /// <param name="speed"></param>
        public CNCRMsgSetSpeed(byte speed)
            : base(CNCRMESSAGE_TYPE.SET_SPEED)
        {
            this._speed = speed;
        }

        public override byte[] toSerial()
        {
            byte[] result = { 0x50, _speed, CNCRConstants.END_OF_MSG };
            return result;
        }

        public byte getSpeed()
        {
            return _speed;
        }

        public void setSpeed(byte speed)
        {
            this._speed = speed;
        }
    }
}
