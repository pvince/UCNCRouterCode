using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// Message object sent to router to change the feedrate.
    /// 
    /// Command Structure:
    /// [Type 0 XYZ] [Speed] [255]
    /// -  Type: 5
    /// -   XYZ: 110 would set the speed for X and Y, but not Z
    /// - Speed: 0 to 254, may NOT be 255. 0 = Stop, 255 = Full speed
    /// </summary>
    public class CNCRMsgSetSpeed : CNCRMessage
    {
        private byte _speed = 0;
        private bool _X = false;
        private bool _Y = false;
        private bool _Z = false;

        /// <summary>
        /// Get the speed that will be sent to the router.
        /// </summary>
        /// <returns></returns>
        public byte getSpeed() { return _speed; }

        /// <summary>
        /// Set the speed that the message will contain.  Speed may not be equal to 255.
        /// </summary>
        /// <param name="speed">New speed for this message.  May not equal 255.</param>
        public void setSpeed(byte speed)
        {
            if (speed == 255)
                throw new ArgumentOutOfRangeException("speed", "Speed may not be equal to 255.");
            _speed = speed;
        }

        public bool isX() { return _X; }
        public void setX(bool X) { _X = X; }
        public bool isY() { return _Y; }
        public void setY(bool Y) { _Y = Y; }
        public bool isZ() { return _Z; }
        public void setZ(bool Z) { _Z = Z; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CNCRMsgSetSpeed()
            : base(CNCRMESSAGE_TYPE.SET_SPEED)
        {}

        /// <summary>
        /// Constructs a SetSpeed message with pre-defined parameters.  Speed may not equal 255.
        /// </summary>
        /// <param name="X">Set speed for X axis.</param>
        /// <param name="Y">Set speed for Y axis.</param>
        /// <param name="Z">Set Speed for Z axis.</param>
        /// <param name="speed">Speed the axis will move.  May not be 255.</param>
        public CNCRMsgSetSpeed(bool X, bool Y, bool Z, byte speed)
            : base(CNCRMESSAGE_TYPE.SET_SPEED)
        {
            if (speed == 255)
                throw new ArgumentOutOfRangeException("speed", "Speed may not be equal to 255.");
            
            _X = X;
            _Y = Y;
            _Z = Z;

            _speed = speed;
        }

        /// <summary>
        /// Transfers message data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type 0 XYZ] [Speed] [255]
        /// -  Type: 5
        /// -   XYZ: 110 would set the speed for X and Y, but not Z
        /// - Speed: 0 to 254, may NOT be 255. 0 = Stop, 255 = Full speed
        /// </returns>
        public override byte[] toSerial()
        {
            // Build first byte [Type 0 XYZ] --> [0101 0XYZ]
            byte TypeAndAxis = Convert.ToByte(Convert.ToByte(MessageType) << 4);
            if (_X) { TypeAndAxis |= 0x04; } // Set X bit [0000 0100]
            if (_Y) { TypeAndAxis |= 0x02; } // Set Y bit [0000 0010]
            if (_Z) { TypeAndAxis |= 0x01; } // Set Z bit [0000 0001]

            byte[] result = { TypeAndAxis, _speed, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
