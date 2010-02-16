using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent by the computer to send X, Y, Z coordinates
    /// to the router.
    /// 
    /// Command Structure:
    /// [Type 000] [UpperX] [LowerX]
    ///            [UpperY] [LowerY]
    ///            [UpperZ] [LowerZ]
    ///            [255]
    /// </summary>
    public class CNCRMsgMove : CNCRMessage
    {
        private Int16 _X = 0;
        private Int16 _Y = 0;
        private Int16 _Z = 0;

        public Int16 getX() { return _X; }
        public Int16 getY() { return _Y; }
        public Int16 getZ() { return _Z; }

        #region Constructors
        private CNCRMsgMove()
            : base(CNCRMESSAGE_TYPE.MOVE)
        { }

        public CNCRMsgMove(Int16 X, Int16 Y, Int16 Z)
            : this()
        {
            this._X = X;
            this._Y = Y;
            this._Z = Z;
        }

        public CNCRMsgMove(byte[] msgBytes)
            : this()
        {
            //TODO: CNCRMsgMove: Error check the bytes
            //Convert Bytes to X, Y, Z
            this._X = BitConverter.ToInt16(msgBytes, 1);
            this._Y = BitConverter.ToInt16(msgBytes, 3);
            this._Z = BitConverter.ToInt16(msgBytes, 5);
        }
        #endregion

        /// <summary>
        /// Outputs the message in a format that can be sent over the
        /// serial connection.
        /// </summary>
        /// <returns>
        /// Command Structure:
        /// [Type 000] [UpperX] [LowerX]
        ///            [UpperY] [LowerY]
        ///            [UpperZ] [LowerZ]
        ///            [255]
        /// </returns>
        public override byte[] toSerial()
        {
            byte Type = getMsgTypeByte();
            byte[] xBits = BitConverter.GetBytes(getX());
            byte[] yBits = BitConverter.GetBytes(getY());
            byte[] zBits = BitConverter.GetBytes(getZ());
            byte[] result = { Type,
                              xBits[0], xBits[1],
                              yBits[0], yBits[1],
                              zBits[0], zBits[1],
                              CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
