


#include "Main.h"
#include "Queue.h"

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
  switch(*Message & 15)
  {
    case (0):  //Ping
      FlagEStop = 1;    
      return(0);
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
      break;
    case(6):  //ToolCMD
      
      break;
   }
  
}

