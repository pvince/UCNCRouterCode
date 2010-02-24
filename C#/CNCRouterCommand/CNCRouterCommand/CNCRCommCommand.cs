/**
 * The majority of this section of the program was pulled from
 * an article on www.dreamincode.com by PsychoCoder
 * http://www.dreamincode.net/forums/showtopic35775.htm
 * 
 * It is distributed by PsychoCoder under the GNU General
 * Public License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;

namespace CNCRouterCommand
{
    public class CNCRCommCommand
    {
        #region Local Variables
        private string _baudRate = string.Empty;
        private string _parity = string.Empty;
        private string _stopBits = string.Empty;
        private string _dataBits = string.Empty;
        private string _portName = string.Empty;
        //TODO: Figure out what method will be called by "received"
        
        private Queue<byte> CommBufferQueue = new Queue<byte>();
        private CNCRMSG_TYPE curType = CNCRMSG_TYPE.zNone;

        private SerialPort comPort = new SerialPort();
        #endregion

        #region Getter // Setter Properties
        /// <summary>
        /// Property to hold the BaudRate
        /// of our manager class
        /// </summary>
        public string BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        /// <summary>
        /// property to hold the Parity
        /// of our manager class
        /// </summary>
        public string Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        /// <summary>
        /// property to hold the StopBits
        /// of our manager class
        /// </summary>
        public string StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        /// <summary>
        /// property to hold the DataBits
        /// of our manager class
        /// </summary>
        public string DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        /// <summary>
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }
        #endregion

        #region Constructors
        // <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="baud">Desired BaudRate</param>
        /// <param name="par">Desired Parity</param>
        /// <param name="sBits">Desired StopBits</param>
        /// <param name="dBits">Desired DataBits</param>
        /// <param name="name">Desired PortName</param>
        public CNCRCommCommand(string baud, string par, string sBits, string dBits, string name)
        {
            _baudRate = baud;
            _parity = par;
            _stopBits = sBits;
            _dataBits = dBits;
            _portName = name;

            //now add an event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }

        /// <summary>
        /// Comstructor to set the properties of our
        /// serial port communicator to nothing
        /// </summary>
        public CNCRCommCommand()
        {
            _baudRate = string.Empty;
            _parity = string.Empty;
            _stopBits = string.Empty;
            _dataBits = string.Empty;
            _portName = "COM1";
            //add event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }
        #endregion

        #region Send Data
        public void WriteData(string msg)
        {
            if (!(comPort.IsOpen == true)) comPort.Open();
            //send the message to the port
            comPort.Write(msg);
        }

        public void SendMsg(CNCRMessage msg)
        {
            if (!(comPort.IsOpen == true)) OpenPort();
            if (comPort.IsOpen)
            {
                byte[] newMsg = msg.toSerial();
                comPort.Write(newMsg, 0, newMsg.Length);
            }
            else
            {
                //TODO: SendMsg: Log an error
            }

        }
        #endregion

        #region OpenClosePort
        public bool OpenPort()
        {
            try
            {
                //first check if the port is already open
                //if its open then close it
                if (comPort.IsOpen == true) ClosePort();

                //set the properties of our SerialPort Object
                comPort.BaudRate = int.Parse(_baudRate);    //BaudRate
                //comPort.DataBits = int.Parse(_dataBits);    //DataBits
                //comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBits);    //StopBits
                //comPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity);    //Parity
                comPort.PortName = _portName;   //PortName
                //now open the port
                comPort.Open();
                //return true
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO: OpenPort: Figure out what we are doing for error reporting
                //return false;
            }
        }

        /// <summary>
        /// Closes an open Comm port.
        /// </summary>
        /// <returns>Returns true if the comm port is successfully closed w/o any errors</returns>
        public bool ClosePort()
        {
            bool result = false;

            try
            {
                comPort.Close();
                result = !comPort.IsOpen;
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO: ClosePort: Figure out what we are doing for error reporting
                //return false;
            }
            return result;
        }
        #endregion

        #region GetParityValues
        public string[] GetParityValues()
        {
            return Enum.GetNames(typeof(Parity));
        }
        #endregion

        #region GetStopBitValues
        public string[] GetStopBitValues()
        {
            return Enum.GetNames(typeof(StopBits));
        }
        #endregion

        #region comPort_DataReceived
        // Process received messages.
        [STAThread]
        public void handleData(byte[] commBuffer)
        {
            // Are we currently in the middle of a type?
            if (CommBufferQueue.Count == 0)
            {
                // No, so grab the type in the next byte.
                curType = (CNCRMSG_TYPE)Enum.ToObject(typeof(CNCRMSG_TYPE), (commBuffer[0] & 0xF0) >> 4);
            }

            // Drop all incoming bytes into the queue
            for (int i = 0; i < commBuffer.Length; i++)
            {
                CommBufferQueue.Enqueue(commBuffer[i]);
            }

            // Check how long of a message we are expecting
            int expectedLength = CNCRTools.getMsgLenFromType(curType);
            // Uh, Oh, what about expectedLength = 0, AKA, bad type?

            // Process the current Queue
            while (CommBufferQueue.Count >= expectedLength)
            {
                // We have enough bytes, lets get the message for those bytes.

                byte[] msgBytes = CommBufferQueue.Take<byte>(expectedLength).ToArray<byte>();
                CNCRMessage CommMsg = CNCRTools.getMsgFromBytes(msgBytes);
                
                // Now what do we need something like "Act On Message" that gets run Asynchronously.
                // But what about the challenge response?  "WaitingForAck" flag, 
                // gets set on message sent (except ack) and cleared when Ack is 
                // received.  Can only send Ack while WaitingForAck
            }

            if (CommBufferQueue.Count > 0)
            {
                // We still have bytes in the queue, but not enough for a complete message.
            }
            
        }

        /// <summary>
        /// method that will be called when there is data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //TODO: comPort_DataReceived: Think about receiving messages.
            /* What are the possiblities here?
             * 1. These are the first bytes we are receiving.  Need to add them to a buffer queue.
             * 2. We have already received some bytes of the command, but not all of them.
             * 3. We have received multiple commands.
             * 4. Some combination of 1, 2, or 3.
             * 
             * How to handle this?  Pass off all bytes to an apartment thread that
             * will keep track of the current status of the incoming bytes.
             * 1. First data arrives, check type.
             * 2. Using type, check length of bytes in buffer.
             * 3. If length of bytes in buffer is not long enough, drop current bytes into a 2nd queue.
             * 3.1 More data arrives.
             * 3.2 Add data to queue, check length again.  If length is > number of bytes needed, read
             *     the needed number of bytes, and check to make sure that the final byte is 255.
             * 
             * For now, for simple testing I am keeping the code below.
             * 
             * Also, we need 
             */
            int bytes = comPort.BytesToRead;
            byte[] comBuffer = new byte[bytes];
            comPort.Read(comBuffer, 0, bytes);

            handleData(comBuffer);

            /*
            //TODO: Repurpose this method to work for me.
            //determine the mode the user selected (binary/string)
            switch (CurrentTransmissionType)
            {
                //user chose string
                case TransmissionType.Text:
                    //read data waiting in the buffer
                    string msg = comPort.ReadExisting();
                    //display the data to the user
                    //DisplayData(MessageType.Incoming, msg + "\n");
                    break;
                //user chose binary
                case TransmissionType.Hex:
                    //retrieve number of bytes in the buffer
                    int bytes = comPort.BytesToRead;
                    //create a byte array to hold the awaiting data
                    byte[] comBuffer = new byte[bytes];
                    //read the data and store it
                    comPort.Read(comBuffer, 0, bytes);
                    //display the data to the user
                    //DisplayData(MessageType.Incoming, ByteToHex(comBuffer) + "\n");
                    break;
                default:
                    //read data waiting in the buffer
                    string str = comPort.ReadExisting();
                    //display the data to the user
                    //DisplayData(MessageType.Incoming, str + "\n");
                    break;
            }//*/
        }
        #endregion

        //* Display Data Stub
        //TODO: Review how DisplayData works, primarily what STAthread refers too.
        #region DisplayData
        public RichTextBox _displayWindow;
        /// <summary>
        /// method to display the data to & from the port
        /// on the screen
        /// </summary>
        /// <param name="type">MessageType of the message</param>
        /// <param name="msg">Message to display</param>
        [STAThread]
        private void DisplayData(string msg)
        {
            _displayWindow.Invoke(new EventHandler(delegate
            {
                _displayWindow.SelectedText = string.Empty;
                _displayWindow.SelectionFont = new Font(_displayWindow.SelectionFont, FontStyle.Bold);
                _displayWindow.AppendText(msg);
                _displayWindow.ScrollToCaret();
            }));
        }
        #endregion//*/
    }
}
