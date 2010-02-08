using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/**
 * Some thoughts on the program
 * - "Communication" interface classes
 *  - A parent "Comm" class that has the standard communication methods
 *  - Children classes that implement the parent's functions for standardization.
 * - Serial communication method.
 *  - Look online for this
 * - G-Code interpretor
 *  - How are we going to store this in the program?
 */
namespace CNCRouterCommand
{
    public partial class CNCRouterCommand : Form
    {
        public CNCRouterCommand()
        {
            InitializeComponent();
        }

        private void CNCRouterCommand_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CNCRMsgPing pingMsg = new CNCRMsgPing();
            CNCRCommCommand commCmd = new CNCRCommCommand();
            commCmd.BaudRate = "9600";
            commCmd.PortName = "COM5";

            commCmd.SendMsg(pingMsg);
        }
    }
}
