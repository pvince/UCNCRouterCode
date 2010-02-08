using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{

    public enum CNCRMESSAGE_TYPE
    {
        PING,
        CMD_ACKNOWLEDGE,
        E_STOP,
        REQUEST_COMMAND,
        START_QUEUE,
        SET_SPEED
    };

    public class CNCRConstants
    {
        // Constants for checking firmware compatibility.
        public const int CNCROUTER_CURRENTFW = 0;

        // Constants for error messages
        public const string ERRMESG_INCOMPATFIRMWARE = "The firmware version of the CNCRouter is not compatible with this version of the software.";


    }
}
