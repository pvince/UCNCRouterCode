using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Collections;

namespace CNCRouterCommand
{
    public static class CNCRTools
    {
        public static string[] GetCommPortList()
        {
            string[] temp = SerialPort.GetPortNames();
            Array.Sort(temp);
            return temp;
        }

        public static string[] GetCNCRouterPorts()
        {
            List<string> results = new List<string>();
            string[] temp = SerialPort.GetPortNames();
            // "Ping" each port and save the ones that "Acknowledge" to the array.

            return results.ToArray();
        }

        public static string GetCNCRouterVersion(string SerialPortName)
        {
            CNCRConstants.strCurFWVersion;
            return "Not Implemented";
        }

    }
}
