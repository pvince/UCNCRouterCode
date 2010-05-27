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

        private CNCRCommCommand commCmd = new CNCRCommCommand();

        private void CNCRouterCommand_Load(object sender, EventArgs e)
        {
#if !DEBUG
            tcInterface.TabPages.Remove(tpGenDebug);
            tcInterface.TabPages.Remove(tpCommDebug);
#endif

            // Setup the debug tab.
            string[] ports = CNCRTools.GetCNCRouterPorts();
            cmbPorts.Items.AddRange(ports);
            if(cmbPorts.Items.Count > 0)
                cmbPorts.SelectedIndex = 0;

            cmbMsgs.SelectedIndex = 0;

            // Setup the Auto Tab
            commCmd._rtbOutputCommDbg = rtbTraffic;
            commCmd._rtbOutputAuto = rtbRCOutput;

            commCmd._displayLabel = lblQueue;

            // Setup the manual tab.
            for (int i = 0; i < distances.Length; i++)
            {
                cmbMoveDistance.Items.Add(distances[i].ToString() + " mm");
            }
            cmbMoveDistance.SelectedIndex = 0;

            // Setup the Settings tab.
            cmbRouterPort.Items.AddRange(ports);
            if(cmbRouterPort.Items.Count > 0)
                cmbRouterPort.SelectedIndex = 0;

        }

        private void tcInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Stuff to run when the tab changes.
        }

        // Events and cuntions related to the "Comm Debug" tab.
        #region Comm Debug Tab
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
                    sendMsg = new CNCRMsgMove(Int16.MinValue, Int16.MaxValue, 0);
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
            //                      center     start     stop
            //                      (0, 0)    (0, -1)    (0, 1)
          //int[] a180 = new int[] { 0, 0,/**/ 0, -1,/**/ 0, 1 }; // -180 // CCW
          //int[] a180 = new int[] { 0, 0,/**/ 0, -1,/**/ -1, 0 };// -270 // CCW
          //int[] a180 = new int[] { 0, 0,/**/ -1, 0,/**/ 0, -1 };// 270 // CW (360 - 270) = 90 
          //int[] a180 = new int[] { 0, 0,/**/ 1, 0,/**/ -1, 0 };// -180
            int[] a180 = new int[] { 0, 0,/**/ 1, 0,/**/ 0, -1 };// -90

            // From (0, -1) to (0, 1) = -180
            double angle = CNCRTools.getAngleFromLines(a180[0], a180[1], a180[2], a180[3], a180[0], a180[1], a180[4], a180[5]);

            label1.Text = angle.ToString();
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
        #endregion

        // Events and functions related to the "Auto" tab.
        #region Auto Tab

        private void btnLoadGCode_Click(object sender, EventArgs e)
        {
            string eventLog = "";
            if (ofdGcodeBrowse.ShowDialog() == DialogResult.OK)
            {
                rtbRCOutput.Clear();

                eventLog += "Opening " + ofdGcodeBrowse.SafeFileName + "\n";

                System.IO.StreamReader readFile = new System.IO.StreamReader(ofdGcodeBrowse.OpenFile());
                string gcode = readFile.ReadToEnd();
                CNCRTools.arcRes = Double.Parse(txtArcAccuracy.Text);
                Queue<CNCRMessage> tempQueue = CNCRTools.parseGCode(gcode, ref eventLog);
                commCmd.commCommandQueueSet(tempQueue);
                eventLog += "Finished Loading. Created " + tempQueue.Count().ToString() + " messages.\n";
                rtbRCOutput.AppendText(eventLog);
                rtbRCOutput.ScrollToCaret();
                lblStatusFile.Text = ofdGcodeBrowse.SafeFileName;

                /* Debug output of the commands to a text file.
                string outputText = "";

                while (tempQueue.Count() > 0)
                {
                    CNCRMessage curMsg = tempQueue.Dequeue();
                    if (curMsg.getMessageType() == CNCRMSG_TYPE.MOVE)
                    {
                        outputText += ((CNCRMsgMove)curMsg).getX().ToString() +
                            "\t" + ((CNCRMsgMove)curMsg).getY().ToString() + "\n";
                    }
                }
                System.IO.TextWriter tw = new System.IO.StreamWriter("output2.txt");
                tw.Write("");
                tw.Close();//*/
            }

        }

        private void btnStartBuild_Click(object sender, EventArgs e)
        {
            startBuild();
        }

        private void btnAbortBuild_Click(object sender, EventArgs e)
        {
            CNCRMessage stopBuild = new CNCRMsgStartQueue(true);
            stopBuild.setPriority(CNCRMSG_PRIORITY.HIGH);

            commCmd.commPriorityQueueEnqueue(stopBuild);
            commCmd.launchProcessQueues();

        }

        private void btnClearEventOutput_Click(object sender, EventArgs e)
        {
            rtbRCOutput.Clear();
        }

        /// <summary> Start the build processes.
        /// This function starts the build processes by doing the following:
        /// - Enqueue a "startBuild" comand 
        /// </summary>
        private void startBuild()
        {
            commCmd.BaudRate = "9600";
            commCmd.PortName = cmbPorts.SelectedItem.ToString();

            rtbRCOutput.AppendText("Starting build...\n" +
                "Opening Port: " + commCmd.PortName + "....");
            rtbRCOutput.ScrollToCaret();

            if (commCmd.OpenPort())
            {
                rtbRCOutput.AppendText(" Opened.\nStarting Build...\n");
                tsPortStatus.Text = commCmd.PortName + " Open";

                CNCRMessage startBuild = new CNCRMsgStartQueue(false);
                startBuild.setPriority(CNCRMSG_PRIORITY.HIGH);

                commCmd.commPriorityQueueEnqueue(startBuild);
                commCmd.launchProcessQueues();
            }
        }
        #endregion

        // Events and functions related to the "Manual" tab.
        #region Manual
        private double[] distances = new double[] { 0.1, 0.2, 0.5, 1, 5, 10, 20, 50, 100 };

        private void btnYp_Click(object sender, EventArgs e)
        {
            Int16 distance = Convert.ToInt16(distances[cmbMoveDistance.SelectedIndex] * 10);
            sendShortMove(new CNCRMsgMove(0, distance, 0), true);
        }
        private void btnYm_Click(object sender, EventArgs e)
        {

            Int16 distance = Convert.ToInt16(-1 * distances[cmbMoveDistance.SelectedIndex] * 10);
            sendShortMove(new CNCRMsgMove(0, distance, 0), true);
        }
        
        private void btnXp_Click(object sender, EventArgs e)
        {
            Int16 distance = Convert.ToInt16(distances[cmbMoveDistance.SelectedIndex] * 10);
            sendShortMove(new CNCRMsgMove(distance, 0, 0), true);
        }
        private void btnXm_Click(object sender, EventArgs e)
        {
            Int16 distance = Convert.ToInt16(-1 * distances[cmbMoveDistance.SelectedIndex] * 10);
            sendShortMove(new CNCRMsgMove(distance, 0, 0), true);
        }

        private void btnZp_Click(object sender, EventArgs e)
        {
            Int16 distance = Convert.ToInt16(distances[cmbMoveDistance.SelectedIndex] * 10);
            sendShortMove(new CNCRMsgMove(0, 0, distance), false);
        }
        private void btnZm_Click(object sender, EventArgs e)
        {
            Int16 distance = Convert.ToInt16(-1 * distances[cmbMoveDistance.SelectedIndex] * 10);
            sendShortMove(new CNCRMsgMove(0, 0, distance), false);
        }
        
        private void sendShortMove(CNCRMsgMove msg, bool isXY)
        {
            float fSpeed;
            // To be safe, set the speed to 30.0 mm/min
            UInt16 uSpeed = CNCRConstants.DEFAULT_Z_FEEDRATE;

            if (isXY)
            {
                if (!float.TryParse(txtXYFeedrate.Text, out fSpeed))
                    fSpeed = CNCRConstants.DEFAULT_XY_FEEDRATE / 10;
            }
            else
            {
                if (!float.TryParse(txtZFeedrate.Text, out fSpeed))
                    fSpeed = CNCRConstants.DEFAULT_Z_FEEDRATE / 10;
            }

            try
            {
                uSpeed = Convert.ToUInt16(fSpeed);
            }
            catch (Exception ex)
            {
                // eat my shorts.
            }

            //commCmd.PortName = cmbRouterPort.SelectedItem.ToString();
            // Enque the "startQueue" in the priority queue.
            commCmd.commPriorityQueueEnqueue(new CNCRMsgStartQueue(false));
            commCmd.commCommandQueueEnqueue(new CNCRMsgSetSpeed(uSpeed));
            commCmd.commCommandQueueEnqueue(msg);
            commCmd.commCommandQueueEnqueue(new CNCRMsgStartQueue(true));
            commCmd.launchProcessQueues();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbTraffic.Clear();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            cmbRouterPort.Items.Clear();
            cmbRouterPort.Items.AddRange(CNCRTools.GetCNCRouterPorts());
            cmbRouterPort.SelectedIndex = 0;
        }




    }
}
