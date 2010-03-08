/**
 * The basis of this section of the program was pulled from
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
using System.Threading;

namespace CNCRouterCommand
{
    public class CNCRCommCommand
    {
        #region Local Variables
        // Property Variables
        private string _baudRate = string.Empty;
        private string _portName = string.Empty;
        private SerialPort comPort = new SerialPort();

        // Ok, so this thing is the question, what is managing this.
        // And by "this" I mean the list of commands that need to be sent.  Do I
        // Just call this function and say, "Hey, here are all the commands you 
        // need to send, go and do work" or do I instead have another function
        // Managing this?  I say I should just and off everything to this class.
        // Have two Queues, a priority queue which is the "Send to router" queue
        // and a "Command Queue" which has a list of all the router commands.
        // When the router asks for some commands, I drop them in the "Send to Router"
        // queue.  If the router sends a message that requires immediate action,
        // Or the user needs to send an E-Stop, it gets jumped to the top of hte
        // "Send to Router" queue.
        // I could have a function called "Process Send to Router Queue".  This 
        // Function would just run down the queue sending messages constantly,
        // Waiting for the Ack, then send another message.

        /// <summary>
        /// This queue is used to store the commands needed to drive the router.
        /// 
        /// It is possible that once the build is started, this variable
        /// might be accessed from multiple threads.
        /// </summary>
        private Queue<CNCRMessage> _commCommandQueue = new Queue<CNCRMessage>();
        private static Semaphore _commCmdQLock = new Semaphore(1, 1);

        /// <summary>
        /// This queue is used for commands that need to be sent sooner rather
        /// than later.  It is sorted via two parameters.
        /// 1. Priority of message (Standard, Medium, High)
        /// 2. MsgID (Message created earlier have lower ID's)
        /// 
        /// It is accessed from multiple threads, therefore use the provided
        /// functions for enqueueing and dequeueing data.
        /// </summary>
        private PriorityQueue<CNCRMessage> _commPriorityQueue = new PriorityQueue<CNCRMessage>();
        private static Semaphore _commPriorityQLock = new Semaphore(1, 1);

        // Received Byte Queue.
        private Queue<byte> _commBufferQueue = new Queue<byte>();

        /// <summary>
        /// DO NOT ACCESS THESE VARIABLES DIRECTLY.  THEY ARE ACCESSED
        /// BY MULTIPLE THREADS. ONLY USE THEIR GETTER AND SETTER VARIABLES.
        /// </summary>
        /// 
        private CNCRMSG_TYPE _curType = CNCRMSG_TYPE.zNone;
        private CNCRMessage _lastMessage;
        private bool _discardingData = false;
        private bool _waitingOnAck = false;
        private bool _eStopActive = false;
        private int _numCmdsToSend = 0;

        // Semephores for accessing the above variables.
        private static Semaphore _curTypeLock = new Semaphore(1, 1);
        private static Semaphore _lastMessageLock = new Semaphore(1, 1);
        private static Semaphore _discardingDataLock = new Semaphore(1, 1);
        private static Semaphore _waitingOnAckLock = new Semaphore(1, 1);
        private static Semaphore _numCmdsToSendLock = new Semaphore(1, 1);
        private static Semaphore _eStopActiveLock = new Semaphore(1, 1);

        private Thread _processQueues;
        #endregion

        #region Getter // Setter Properties
        private CNCRMessage commCommandQueueDequeue()
        {
            CNCRMessage result;
            _commCmdQLock.WaitOne();
            result = _commCommandQueue.Dequeue();
            _commCmdQLock.Release();
            return result;
        }
        private void commCommandQueueEnqueue(CNCRMessage enqueue)
        {
            _commCmdQLock.WaitOne();
            _commCommandQueue.Enqueue(enqueue);
            _commCmdQLock.Release();
        }
        private int commCommandQueueCount()
        {
            int result;
            _commCmdQLock.WaitOne();
            result = _commCommandQueue.Count;
            _commCmdQLock.Release();
            return result;
        }

        private CNCRMessage commPriorityQueueDequeue()
        {
            CNCRMessage result;
            _commPriorityQLock.WaitOne();
            result = _commPriorityQueue.Dequeue();
            _commPriorityQLock.Release();
            return result;
        }
        private void commPriorityQueueEnqueue(CNCRMessage enqueue)
        {
            _commPriorityQLock.WaitOne();
            _commCommandQueue.Enqueue(enqueue);
            _commPriorityQLock.Release();
        }
        private int commPriorityQueueCount()
        {
            int result;
            _commPriorityQLock.WaitOne();
            result = _commPriorityQueue.Count;
            _commPriorityQLock.Release();
            return result;
        }

        private bool getWaitingOnAck()
        {
            bool result = false;
            _waitingOnAckLock.WaitOne();
            result = _waitingOnAck;
            _waitingOnAckLock.Release();
            return result;
        }
        private void setWaitingOnAck(bool waitingOnAck)
        {
            _waitingOnAckLock.WaitOne();
            _waitingOnAck = waitingOnAck;
            _waitingOnAckLock.Release();
        }

        private int getNumCmdsToSend()
        {
            int result = 0;
            _numCmdsToSendLock.WaitOne();
            result = _numCmdsToSend;
            _numCmdsToSendLock.Release();
            return result;
        }
        private void setNumCmdsToSend(int numCmdsToSend)
        {
            _numCmdsToSendLock.WaitOne();
            _numCmdsToSend = numCmdsToSend;
            _numCmdsToSendLock.Release();
        }

        private CNCRMSG_TYPE getCurType()
        {
            CNCRMSG_TYPE result = CNCRMSG_TYPE.zNone;
            _curTypeLock.WaitOne();
            result = _curType;
            _curTypeLock.Release();
            return result;
        }
        private void setCurType(CNCRMSG_TYPE curType)
        {
            _curTypeLock.WaitOne();
            _curType = curType;
            _curTypeLock.Release();
        }

        private bool getEStopActive()
        {
            bool result = false;
            _eStopActiveLock.WaitOne();
            result = _eStopActive;
            _eStopActiveLock.Release();
            return result;
        }
        private void setEStopActive(bool eStopActive)
        {
            _eStopActiveLock.WaitOne();
            _eStopActive = eStopActive;
            _eStopActiveLock.Release();
        }

        private CNCRMessage getLastMessage()
        {
            CNCRMessage result;
            _lastMessageLock.WaitOne();
            result = _lastMessage;
            _lastMessageLock.Release();
            return result;
        }
        private void setLastMessage(CNCRMessage lastMessage)
        {
            _lastMessageLock.WaitOne();
            _lastMessage = lastMessage;
            _lastMessageLock.Release();
        }

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
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        /// <summary>
        /// Safely retrieves the value for _discardingData.  Only one thread
        /// at a time can access the value of discardingdata.
        /// </summary>
        /// <returns>True if the communication program is currently discarding
        /// any received data.</returns>
        private bool getDiscardingData()
        {
            bool result = false;
            _discardingDataLock.WaitOne();
            result = _discardingData;
            _discardingDataLock.Release();
            return result;
        }

        /// <summary>
        /// Safely sets the value for _discardingData.
        /// </summary>
        /// <param name="discardingData">True if we should discard any received
        /// data.</param>
        private void setDiscardingData(bool discardingData)
        {
            _discardingDataLock.WaitOne();
            _discardingData = discardingData;
            _discardingDataLock.Release();
        }
        #endregion

        #region Constructors
        // <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="baud">Desired BaudRate</param>
        /// 
        /// 
        /// 
        /// <param name="name">Desired PortName</param>
        public CNCRCommCommand(string baud, string name)
        {
            _baudRate = baud;
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
            _baudRate = "9600";
            _portName = "COM1";
            //add event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }
        #endregion

        #region Send Data
        public void WriteString(string msg)
        {
            if (!(comPort.IsOpen == true)) comPort.Open();
            //send the message to the port
            comPort.Write(msg);
        }

        public void SendMsg(CNCRMessage msg)
        {
            //TODO: SendMsg: Should this function really be doing this check?  Shouldnt the function errorhandler do this?
            if (getDiscardingData()                                     // If we are discarding data.
                && msg.getMessageType() == CNCRMSG_TYPE.CMD_ACKNOWLEDGE // and sending an acknowledge
                && ((CNCRMsgCmdAck)msg).getError() == true)             // and that acknowledge is saying we have an error.
            {
                setDiscardingData(false);   // Then clear the discard data bit so we can see the response.
                DisplayData("Sent Ack\n");
            }
            if (msg.getMessageType() != CNCRMSG_TYPE.CMD_ACKNOWLEDGE)
            {
                setWaitingOnAck(true);
            }
            setLastMessage(msg);
            SendBytes(msg.toSerial());
        }

        public void SendBytes(byte[] bytes)
        {
            if (!(comPort.IsOpen == true)) OpenPort();
            if (comPort.IsOpen && (bytes.Length > 0))
            {
                comPort.Write(bytes, 0, bytes.Length);
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

        #region comPort_DataReceived
        // Process received messages.
        /// <summary>
        /// Builds messages from the received data.
        /// </summary>
        /// <param name="commBuffer">Array of bytes just received over serial.</param>
        [STAThread]
        public void handleData(byte[] commBuffer)
        {
            // Are we currently in the middle of a type?
            if (_commBufferQueue.Count == 0)
            {
                // No, so grab the type in the next byte.
                setCurType((CNCRMSG_TYPE)Enum.ToObject(typeof(CNCRMSG_TYPE), (commBuffer[0] & 0xF0) >> 4));
            }

            // TODO: handleData: this is a hack, figure out a better way to validate the type.
            if (getCurType() <= CNCRMSG_TYPE.TOOL_CMD)
            {
                // Drop all incoming bytes into the queue
                for (int i = 0; i < commBuffer.Length; i++)
                {
                    if (CNCRTools.validateParityBit(commBuffer[i]))
                        _commBufferQueue.Enqueue(commBuffer[i]);
                    else
                    {
                        // TODO: Bad Parity bit, Log an error, and discard data.
                        handleError("Invalid Parity Bit.");
                        return;
                    }
                }

                // Check how long of a message we are expecting
                int expectedLength = CNCRTools.getMsgLenFromType(getCurType());
                // Uh, Oh, what about expectedLength = 0, AKA, bad type?
                // - At this point, curType should be validated and curType
                //   should not be unknown, throw an error in getMsgLenFromType.
                if (expectedLength > 0)
                {
                    // Process the current Queue
                    while (_commBufferQueue.Count >= expectedLength)
                    {
                        // We have enough bytes, lets get the message for those bytes.
                        byte[] msgBytes = new byte[expectedLength];
                        for (int i = 0; i < msgBytes.Length; i++)
                        {
                            msgBytes[i] = _commBufferQueue.Dequeue();
                        }

                        if (CNCRTools.validateParityByte(msgBytes))
                        {
                            DisplayData("- Valid Parity\n");
                            CNCRMessage CommMsg = CNCRTools.getMsgFromBytes(msgBytes);
                            DisplayData("- Type: " + CommMsg.ToString() + "\n");
                            Thread runActOnMessage = new Thread(new ParameterizedThreadStart(actOnMessage));
                            runActOnMessage.Start(CommMsg);
                        }
                        else
                        {
                            handleError("Invalid Parity Byte.");
                            return;
                        }
                        // Now what do we need something like "Act On Message" that gets run Asynchronously.
                        // But what about the challenge response?  "WaitingForAck" flag, 
                        // gets set on message sent (except ack) and cleared when Ack is 
                        // received.  Can only send Ack while WaitingForAck
                        // I need to review this code when I am more awake.
                    }
                }
                else
                {
                    handleError("Expected length was 0 or less.");
                    return;
                }
            }
            else
            {
                // Bad type: log an error, discard bytes.
                handleError("Invalid message type.");
                return;
            }
        }

        /// <summary>
        /// Takes the passed in message and performs the nessessary actions
        /// required by that message.
        /// </summary>
        /// <param name="msg">Message to act on.</param>
        public void actOnMessage(object msgObj)
        {
            CNCRMessage msg = (CNCRMessage)msgObj;
            if (msg == null)
                throw new ArgumentNullException();

            switch (msg.getMessageType())
            {
                case CNCRMSG_TYPE.CMD_ACKNOWLEDGE:
                    if (((CNCRMsgCmdAck)msg).getError())
                    {
                        // if error, resend last message
                        CNCRMessage lastMsg = getLastMessage();
                        lastMsg.setPriority(CNCRMSG_PRIORITY.HIGH);
                        commPriorityQueueEnqueue(lastMsg);
                    }
                    else
                    {
                        // if not error, clear "Waiting for Ack"
                        setWaitingOnAck(false);
                    }
                    launchProcessQueues();
                    break;
                case CNCRMSG_TYPE.E_STOP:
                    // Send Ack.
                    CNCRMessage ack = new CNCRMsgCmdAck(false, 0);
                    ack.setPriority(CNCRMSG_PRIORITY.MEDIUM);
                    commPriorityQueueEnqueue(ack);

                    // Set the "Stop Sending Messages variable" 
                    setEStopActive(true);
                    break;
                case CNCRMSG_TYPE.REQUEST_COMMAND:
                    // Send Ack.
                    CNCRMessage ack2 = new CNCRMsgCmdAck(false, 0);
                    ack2.setPriority(CNCRMSG_PRIORITY.MEDIUM);
                    commPriorityQueueEnqueue(ack2);

                    // Set the "Send Commands" variable to the # of messages.
                    CNCRMsgRequestCommands msgRC = (CNCRMsgRequestCommands)msg;
                    setNumCmdsToSend(msgRC.getCommandCount());

                    // If it is not already started, kick off the "SendMessages" method.
                    launchProcessQueues();
                    break;
                case CNCRMSG_TYPE.MOVE:
                case CNCRMSG_TYPE.PING:
                case CNCRMSG_TYPE.SET_SPEED:
                case CNCRMSG_TYPE.START_QUEUE:
                case CNCRMSG_TYPE.TOOL_CMD:
                    // We should not be receiving any of these messages.
                    // TODO: ActOnMessage: The following is not really an error, look into how to throw "warnings"
                    throw new ArgumentException("Received a valid message that should not have been sent by router.");
                default:
                    throw new ArgumentException("CNCRMessage has an invalid type");
            }
        }

        private void launchProcessQueues()
        {

            if (_processQueues == null ||
                _processQueues.ThreadState == ThreadState.Stopped ||
                _processQueues.ThreadState == ThreadState.Unstarted)
            {
                _processQueues = new Thread(new ThreadStart(processQueues));
                _processQueues.Start();
            }
        }

        private void processQueues()
        {
            // If waiting for Ack or EStopped, exit thread, we cant send any commands.
            if (!getWaitingOnAck() && !getEStopActive())
            {
                int numCmdsToSend = getNumCmdsToSend();
                // Check the priority queue
                if (commPriorityQueueCount() > 0)
                    SendMsg(commPriorityQueueDequeue());
                else if (numCmdsToSend > 0 && commCommandQueueCount() > 0)
                {
                    // Check if we need to send some commands
                    SendMsg(commCommandQueueDequeue());
                    numCmdsToSend--;
                    setNumCmdsToSend(numCmdsToSend);
                }
            }

        }
        /// <summary>
        /// Performs operations nessessary to handle a new error.
        /// </summary>
        /// <param name="errorMsg">Error message to log.</param>
        private void handleError(string errorMsg)
        {
            Thread errorHandlerThread = new Thread(new ParameterizedThreadStart(asynchHandleError));
            errorHandlerThread.Name = "asynchHandleError";
            errorHandlerThread.Start(errorMsg);
        }

        /// <summary>
        /// This function is meant to be called only from "handleError".
        /// </summary>
        /// <param name="msg"></param>
        private void asynchHandleError(object msg)
        {
            // Set the "DiscardData" flag, hmm, do we need to move the check up to "Data Received"?
            setDiscardingData(true);
            // Empty the Queue
            _commBufferQueue.Clear();
            // Sleep 50 ms to allow serial data to finish arriving.
            Thread.Sleep(CNCRConstants.DISCARD_DELAY_MS);
            // Log the error
            DisplayData("error: " + (string)msg + "\n"); // TODO: asynchHandleError: Determine alternate method for logging errors.
            // Send the failed "Ack"
            SendMsg(new CNCRMsgCmdAck(true, 0));
        }
        /// <summary>
        /// method that will be called when there is data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = comPort.BytesToRead;
            byte[] comBuffer = new byte[bytes];
            comPort.Read(comBuffer, 0, bytes);

            if (getDiscardingData())
            {
                DisplayData("Discarded: " + CNCRTools.BytesToHex(comBuffer) + "\n");
            }
            else
            {
                DisplayData(CNCRTools.BytesToHex(comBuffer) + "\n");
                handleData(comBuffer);
                DisplayDataQueue();
            }
        }
        #endregion

        //* Display Data Stub
        #region DisplayData
        public RichTextBox _displayWindow;
        public Label _displayLabel;
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
        [STAThread]
        private void DisplayDataQueue()
        {
            _displayLabel.Invoke(new EventHandler(delegate
            {
                _displayLabel.Text = string.Empty;
                byte[] bytesInQ = _commBufferQueue.ToArray();
                _displayLabel.Text = CNCRTools.BytesToHex(bytesInQ);
            }));
        }
        #endregion//*/
    }
}
