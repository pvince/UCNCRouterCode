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
        private CNCRCommCommand commCmd = new CNCRCommCommand();
        public CNCRouterCommand()
        {
            InitializeComponent();
        }

        private void CNCRouterCommand_Load(object sender, EventArgs e)
        {
            string[] ports = CNCRTools.GetCNCRouterPorts();
            cmbPorts.Items.AddRange(ports);
            cmbPorts.SelectedIndex = 0;
            cmbMsgs.SelectedIndex = 0;
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            commCmd.BaudRate = "9600";
            commCmd.PortName = cmbPorts.SelectedItem.ToString();
            //commCmd.DisplayWindow = richTextBox1;
            //commCmd.CurrentTransmissionType = CNCRCommCommand.TransmissionType.Text;
            if (commCmd.OpenPort())
            {
                btnOpenPort.Enabled = false;
                btnClosePort.Enabled = true;
                btnSndMsg.Enabled = true;
            }
            
            //TODO: add CommCmd.isOpen();
        }

        private void btnSndMsg_Click(object sender, EventArgs e)
        {
            int discarded = 0;
            byte[] bytes = CNCRTools.GetBytes(txtHex.Text, out discarded);
            lblDbgOut.Text = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                lblDbgOut.Text += bytes[i].ToString() + " ";
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            if (commCmd.ClosePort())
            {
                btnOpenPort.Enabled = true;
                btnClosePort.Enabled = false;
                btnSndMsg.Enabled = false;
            }
        }
    }
}
