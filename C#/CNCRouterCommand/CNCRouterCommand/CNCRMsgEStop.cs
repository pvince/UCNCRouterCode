﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    /// <summary>
    /// This message is sent from either the router or the PC and is used
    /// to stop the router and the software.
    /// 
    /// Command Structure:
    /// [Type 0000] [255]
    /// - Type: 2
    /// </summary>
    public class CNCRMsgEStop : CNCRMessage
    {
        public CNCRMsgEStop()
            : base(CNCRMESSAGE_TYPE.E_STOP)
        { }

        /// <summary>
        /// Transfers CNCRMsgEStop to a byte array for transfer.
        /// </summary>
        /// <returns>
        /// Command Structure:
        ///     [Type 0000] [255]
        /// </returns>
        public override byte[] toSerial()
        {
            // [0010 0000] [255]
            byte Type = Convert.ToByte(Convert.ToByte(MessageType) << 4);
            byte[] result = { Type, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}