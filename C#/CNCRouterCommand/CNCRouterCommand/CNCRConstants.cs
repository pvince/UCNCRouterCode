using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{

    public enum CNCRMESSAGE_TYPE
    {
        PING,           //0 - Computer  -> Router
        CMD_ACKNOWLEDGE,//1 - Computer <-> Router
        E_STOP,         //2 - Computer <-> Router
        REQUEST_COMMAND,//3 - Computer <-  Router
        START_QUEUE,    //4 - Computer  -> Router
        SET_SPEED,      //5 - Computer  -> Router
        MOVE            //6 - Computer  -> Router
    };

    public class CNCRConstants
    {
        // Constants for checking firmware compatibility.
        public const int CNCROUTER_CURRENTFW = 0;

        // Constants for error messages
        public const string ERRMESG_INCOMPATFIRMWARE = "The firmware version of the CNCRouter is not compatible with this version of the software.";

        public const byte END_OF_MSG = 255;
    }
}
