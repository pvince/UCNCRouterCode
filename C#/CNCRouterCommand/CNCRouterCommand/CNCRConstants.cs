using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{

    public enum CNCRMSG_TYPE
    {
        PING,           //0 - Computer  -> Router
        CMD_ACKNOWLEDGE,//1 - Computer <-> Router
        E_STOP,         //2 - Computer <-> Router
        REQUEST_COMMAND,//3 - Computer <-  Router
        START_QUEUE,    //4 - Computer  -> Router
        SET_SPEED,      //5 - Computer  -> Router
        MOVE,           //6 - Computer  -> Router
        TOOL_CMD,       //7 - Computer  -> Router
        zNone           //If we ever get this type, it is an error.
    };

    public enum CNCRMSG_PRIORITY
    {
        HIGH,           // Messages that need to be sent right now, E-Stop
        MEDIUM,         // Messages that can wait a couple milliseconds, CmdAck, Ping
        STANDARD        // Standard messages, Move, ToolCmd, SetSpeed
    };

    public class CNCRConstants
    {
        // Constants for checking to ensure correct serial message length.
        public const int MSG_LEN_PING = 2;
        public const int MSG_LEN_CMD_ACK = 3;
        public const int MSG_LEN_ESTOP = 2;
        public const int MSG_LEN_RQST_COMM = 3;
        public const int MSG_LEN_STARTQ = 2;
        public const int MSG_LEN_SETSPD = 5;
        public const int MSG_LEN_MOVE = 11;
        public const int MSG_TOOL_CMD = 2;

        // Constants for the serial communication

        /// <summary>
        /// After receiving an error, how long should we ignore messages?
        /// </summary>
        public const int DISCARD_DELAY_MS = 50;

        /// <summary>
        /// How long should we wait after sending a message before reporting an 
        /// error?
        /// </summary>
        public const int TIMEOUT_MS = 500; //TODO: TIMEOUT_MS: tweak this setting

        // Constants for checking firmware compatibility.
        public const int CNCROUTER_CURRENTFW = 3;



        // Constants for error messages
        public const string ERRMESG_INCOMPATFIRMWARE = "The firmware version of the CNCRouter is not compatible with this version of the software.";

        public const byte END_OF_MSG = 255;
    }
}
