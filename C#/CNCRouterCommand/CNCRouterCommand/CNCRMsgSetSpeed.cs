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
    /// [Type XYZ P] [SpeedUpper7 P] [SpeedLower7 P] [0000 0 SU1 SL1 P] [Parity]
    /// -  Type: 5
    /// -   XYZ: 110 would set the speed for X and Y, but not Z
    /// - Speed: 16 bit unsigned int, a range of about 64k
    /// </summary>
    public class CNCRMsgSetSpeed : CNCRMessage
    {
        private UInt16 _speed = 0;
        private bool _X = false;
        private bool _Y = false;
        private bool _Z = false;

        public UInt16 getSpeed() { return _speed; }
        public bool isX() { return _X; }
        public bool isY() { return _Y; }
        public bool isZ() { return _Z; }

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        private CNCRMsgSetSpeed()
            : base(CNCRMESSAGE_TYPE.SET_SPEED)
        {}

        /// <summary>
        /// Constructs a SetSpeed message with pre-defined parameters.  Speed may not equal 255.
        /// </summary>
        /// <param name="X">Set speed for X axis.</param>
        /// <param name="Y">Set speed for Y axis.</param>
        /// <param name="Z">Set Speed for Z axis.</param>
        /// <param name="speed">Speed the axis will move.  May not be 255.</param>
        public CNCRMsgSetSpeed(bool X, bool Y, bool Z, UInt16 speed)
            : this()
        {
            _X = X;
            _Y = Y;
            _Z = Z;

            _speed = speed;
        }

        public CNCRMsgSetSpeed(byte[] msgBytes)
            : this()
        {
            //TODO: CNCRMsgSetSpeed: msgBytes constructor needs to validate msgBytes.
            if ((msgBytes[0] & 0x04) == 0x08) { _X = true; }
            if ((msgBytes[0] & 0x02) == 0x04) { _Y = true; }
            if ((msgBytes[0] & 0x01) == 0x02) { _Z = true; }
            _speed = CNCRTools.generateUInt16FromThreeBytes(msgBytes, 1);
        }
        #endregion

        /// <summary>
        /// Transfers message data to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type XYZ P] [SpeedUpper7 P] [SpeedLower7 P] [0000 0 SU1 SL1 P] [Parity]
        /// -  Type: 5
        /// -   XYZ: 110 would set the speed for X and Y, but not Z
        /// - Speed: 16 bit unsigned int, a range of about 64k
        /// </returns>
        public override byte[] toSerial()
        {
            // Build first byte [Type 0 XYZ] --> [0101 0XYZ]
            byte TypeAndAxis = getMsgTypeByte();
            if (_X) { TypeAndAxis |= 0x08; } // Set X bit [0000 1000]
            if (_Y) { TypeAndAxis |= 0x04; } // Set Y bit [0000 0100]
            if (_Z) { TypeAndAxis |= 0x02; } // Set Z bit [0000 0010]

            byte[] speedBytes = CNCRTools.generateThreeBytesFromUInt16(_speed);

            byte[] result = { TypeAndAxis, speedBytes[0],
                              speedBytes[1], speedBytes[2], 0 };
            CNCRTools.generateParity(ref result);
            return result;
        }
    }
}
