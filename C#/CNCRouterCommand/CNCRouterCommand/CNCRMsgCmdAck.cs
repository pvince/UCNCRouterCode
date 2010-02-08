using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public class CNCRMsgCmdAck : CNCRMessage
    {
        //TODO: CNCRMsgCmdAck: Implement getters // setters // constructor for isError
        private bool isError = false;

        public CNCRMsgCmdAck()
            : base(CNCRMESSAGE_TYPE.CMD_ACKNOWLEDGE)
        { }

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

            byte[] result = { TypeAndErr, CNCRConstants.END_OF_MSG };
            return result;
        }
    }
}
