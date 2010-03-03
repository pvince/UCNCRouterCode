using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace CNCRouterCommand
{
    public abstract class CNCRMessage : IComparable
    {
        private CNCRMSG_TYPE _msgType;
        private CNCRMSG_PRIORITY _priority = CNCRMSG_PRIORITY.STANDARD;

        private byte _msgTypeByte;
        private static ulong msg_counter = 0;
        private ulong _msgID;

        public CNCRMessage(CNCRMSG_TYPE msgType, CNCRMSG_PRIORITY priority)
        {
            _msgType = msgType;
            _msgTypeByte = Convert.ToByte(Convert.ToByte(_msgType) << 4);
            _msgID = msg_counter++;
        }

        public CNCRMessage(CNCRMSG_TYPE msgType) 
            : this(msgType, CNCRMSG_PRIORITY.STANDARD) { }

        #region Getter // Setter
        public CNCRMSG_TYPE getMessageType()
        {
            return _msgType;
        }

        public byte getMsgTypeByte()
        {
            return _msgTypeByte;
        }

        public CNCRMSG_PRIORITY getPriority()
        {
            return _priority;
        }
        public void setPriority(CNCRMSG_PRIORITY priority)
        {
            _priority = priority;
        }

        public ulong getMsgID()
        {
            return _msgID;
        }
        #endregion

        public override string ToString()
        {
            return "[ " + _msgType.ToString() +
                   " - " + _priority.ToString() +
                   " - ID: " + _msgID + " ]";
        }

        public abstract byte[] toSerial();

        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        // @@@@@ IComparable  Functions @@@@@
        public int CompareTo(object obj)
        {
            CNCRMessage msg = (CNCRMessage)obj;
            if (msg != null)
            {
                int compare = getPriority().CompareTo(msg.getPriority());
                switch (compare)
                {
                    case 0:
                        return _msgID.CompareTo(msg.getMsgID());
                    default:
                        return compare;
                }
            }
            else
                throw new ArgumentException("Object is not a message");
        }
    }
}
