


#include "Main.h"
#include "Queue.h"
#include "Comm.h"

//int ErrorCheck(long* Message,int len)
//{
//        long sum1 = 0xffff, sum2 = 0xffff;
// 
//        while (len) {
//                unsigned tlen = len > 360 ? 360 : len;
//                len -= tlen;
//                do {
//                        sum1 += *Message++;
//                        sum2 += sum1;
//                } while (--tlen);
//                sum1 = (sum1 & 0xffff) + (sum1 >> 16);
//                sum2 = (sum2 & 0xffff) + (sum2 >> 16);
//        }
//        /* Second reduction step to reduce sums to 16 bits */
//        sum1 = (sum1 & 0xffff) + (sum1 >> 16);
//        sum2 = (sum2 & 0xffff) + (sum2 >> 16);
//        return sum2 << 16 | sum1;
//
//}

int MessageFilter(long* Message)
{
  long* packet;
  switch(*Message & 15) //4026531840 number to use to get the first 4bits of the message.
  {
    case (0):  //Ping
      break;
    case (1):  //Acknowledge
      Serial.print(Firmware);
      break;
    case(2):  //EStop
      FlagEStop = 1;
      FlagStart = 0;
      break;
    case(3):  //Request Commands
      break;
    case(4):  //Start Queue
      FlagEStop = 0;
      FlagStart = 1;
      break;
    case(5):  //SetSpeed
      QueueAdd(Message);
      break;
    case(6):  //ToolCMD
      QueueAdd(Message);
      break;
    case(7):  //Move
      QueueAdd(Message);
      break;
   }
}

