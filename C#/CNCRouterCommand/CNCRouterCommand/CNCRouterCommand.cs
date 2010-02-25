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
            commCmd._displayWindow = rtbTraffic;
            commCmd._displayLabel = lblQueue;
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            commCmd.BaudRate = "9600";
            commCmd.PortName = cmbPorts.SelectedItem.ToString();
            //commCmd.DisplayWindow = richTextBox1;
            //commCmd.CurrentTransmissionType = CNCRCommCommand.TransmissionType.Text;
            if (commCmd.OpenPort())
            {
                tsPortStatus.Text = commCmd.PortName + " Open";
                btnOpenPort.Enabled = false;
                btnClosePort.Enabled = true;
                btnSndMsg.Enabled = true;
            }
            
            //TODO: add CommCmd.isOpen();
        }

        private void btnSndMsg_Click(object sender, EventArgs e)
        {
            /*
            int discarded = 0;
            byte[] bytes = CNCRTools.GetBytes(txtHex.Text, out discarded);
            lblDbgOut.Text = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                lblDbgOut.Text += bytes[i].ToString() + " ";
            }//*/

            byte[] sendBytes = {0};
            CNCRMessage sendMsg;
            switch (cmbMsgs.SelectedIndex)
            {
                case 0:
                    int discarded = 0;
                    sendBytes = CNCRTools.GetBytes(txtHex.Text, out discarded);
                    break;
                case 1:
                    sendMsg = new CNCRMsgPing();
                    sendBytes = sendMsg.toSerial();
                    break;
                case 2:
                    sendMsg = new CNCRMsgCmdAck(false, 127);
                    sendBytes = sendMsg.toSerial();
                    break;
                case 3:
                    sendMsg = new CNCRMsgEStop();
                    sendBytes = sendMsg.toSerial();
                    break;
                case 4:
                    sendMsg = new CNCRMsgRequestCommands(128);
                    sendBytes = sendMsg.toSerial();
                    break;
                case 5:
                    sendMsg = new CNCRMsgStartQueue();
                    sendBytes = sendMsg.toSerial();
                    break;
                case 6:
                    sendMsg = new CNCRMsgSetSpeed(true, true, false, 40000);
                    sendBytes = sendMsg.toSerial();
                    break;
                case 7:
                    sendMsg = new CNCRMsgMove(-32768, 32767, 0);
                    sendBytes = sendMsg.toSerial();
                    break;
                case 8:
                    sendMsg = new CNCRMsgToolCmd(true);
                    sendBytes = sendMsg.toSerial();
                    break;
            }
            rtbTraffic.AppendText(CNCRTools.ToString(sendBytes) + "\n");
            commCmd.SendBytes(sendBytes);
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            if (commCmd.ClosePort())
            {
                tsPortStatus.Text = commCmd.PortName + " Closed";
                btnOpenPort.Enabled = true;
                btnClosePort.Enabled = false;
                btnSndMsg.Enabled = false;
            }
        }
    }
}
