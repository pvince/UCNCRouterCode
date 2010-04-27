﻿using System;
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
            rtbTraffic.AppendText("@ Opening " + commCmd.PortName + " @\n");
            rtbTraffic.ScrollToCaret();
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
            CNCRMessage sendMsg = null;
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
                    sendMsg = new CNCRMsgStartQueue(false);
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
            rtbTraffic.AppendText(CNCRTools.BytesToHex(sendBytes) + "\n");
            if (sendMsg == null)
                commCmd.SendBytes(sendBytes);
            else
                commCmd.SendMsg(sendMsg);
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            if (commCmd.ClosePort())
            {
                tsPortStatus.Text = commCmd.PortName + " Closed";
                btnOpenPort.Enabled = true;
                btnClosePort.Enabled = false;
                btnSndMsg.Enabled = false;

                rtbTraffic.AppendText("@@@@@@@@@@@@@@@@@@@@\n");
                rtbTraffic.ScrollToCaret();
            }

        }

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            cmbPorts.Items.Clear();
            cmbPorts.Items.AddRange(CNCRTools.GetCNCRouterPorts());
            cmbPorts.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CNCRMessage bob = new CNCRMessage(CNCRMSG_TYPE.E_STOP);

            /* For testing the priority Queue
            PriorityQueue<CNCRMessage> testQ = new PriorityQueue<CNCRMessage>();
            testQ.Enqueue(new CNCRMsgSetSpeed(true, true, false, 300));
            testQ.Enqueue(new CNCRMsgSetSpeed(false, false, true, 100));
            testQ.Enqueue(new CNCRMsgMove(0, 0, 5));
            testQ.Enqueue(new CNCRMsgToolCmd(true));
            testQ.Enqueue(new CNCRMsgMove(0, 0, 0));
            CNCRMessage testM = new CNCRMsgCmdAck(false, 0);
            testM.setPriority(CNCRMSG_PRIORITY.MEDIUM);
            testQ.Enqueue(testM);

            label1.Text = "";
            while (testQ.Count > 0)
            {
                label1.Text += testQ.Dequeue().ToString() + "\n";
            }//*/

            

            /* For testing queue joining.

            List<int> bob = new List<int>();
            bob.Add(1);
            bob.Add(2);
            bob.Add(3);
            bob.Add(4);

            List<int> bob2 = new List<int>();
            bob2.Add(5);
            bob2.Add(6);
            bob2.Add(7);
            bob2.Add(8);

            bob.AddRange(bob2);

            Queue<int> jim = new Queue<int>(bob);

            label1.Text = "";
            while (jim.Count > 0)
            {
                label1.Text += jim.Dequeue().ToString() + "\n";
            }//*/

        }

        private void tcInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Stuff to run when the tab changes.
        }

        private void btnLoadGCode_Click(object sender, EventArgs e)
        {
            //* For testing gcode reading.
            //C:\Users\vincenpt\Documents\SeniorDesign\trunk\Docs\Drawings\DXF_Drawings\Square_40x40mm.nc"
            string gcode = CNCRTools.readTextFile("C:/Users/vincenpt/Documents/SeniorDesign/trunk/Docs/Drawings/DXF_Drawings/SquareArc_40mm.nc");
            //string gcode = CNCRTools.readTextFile("C:/Users/vincenpt/Documents/SeniorDesignSVN/SeniorDesign/Docs/Drawings/DXF_Drawings/SquareArc_40mm.nc");

            string logMessages = "";
            CNCRTools.parseGCode(gcode, ref logMessages);
            rtbRCOutput.AppendText(logMessages);//*/
        }
    }
}
