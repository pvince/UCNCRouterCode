using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    class CNCRConstants
    {
        // Constants for checking firmware compatibility.
        public const string[] strFWVersions = { "1" };
        public const string strCurFWVersion = strFWVersions[1];

        // Constants for error messages
        public const string strErr_IncompatFirmware = "The firmware version of the CNCRouter is not compatible with this version of the software.";


    }
}
