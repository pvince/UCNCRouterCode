


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

int MessageFilter(void &Message)
{
  switch(*Message & 15)
  {
    case (0):  //Ping
          
      break;
    case (1):  //Acknowledge
      Serial.print(Firmware);
      break;
    case(2):  //EStop
      break;
    case(3):  //Request Commands
      break;
    case(4):  //Start Queue
      break;
    case(5):  //SetSpeed
      MessageSetSpeed Packet;
      //Packet = (void) *Message;
      break;
    case(6):  //ToolCMD
      MessageToolCMD Packet2;
      break;
    case(7):  //Move
      MessageMove Packet3;
      break;
   }
  
}

