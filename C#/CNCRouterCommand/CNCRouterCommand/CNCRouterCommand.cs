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

        CNCRCommCommand commCmd = new CNCRCommCommand();
        private void button1_Click(object sender, EventArgs e)
        {

            byte testByte = 254;
            CNCRTools.generateParityBit(ref testByte);
            testByte = 252;
            CNCRTools.generateParityBit(ref testByte);
            testByte = 255;
            CNCRTools.generateParityBit(ref testByte);
            byte testTwo = 0;

            byte[] testArray = { 171, 84, 254, 0 };
            CNCRTools.generateParityByte(ref testArray);
            byte testThree = 0;

            //commCmd.SendMsg(sendMsg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commCmd.BaudRate = "9600";
            commCmd.PortName = "COM3";
            //commCmd.DisplayWindow = richTextBox1;
            //commCmd.CurrentTransmissionType = CNCRCommCommand.TransmissionType.Text;
            commCmd.OpenPort();
            //TODO: add CommCmd.isOpen();
        }
    }
}
