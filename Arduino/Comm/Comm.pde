


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

int MessageFilter(int* Message)
{
  //long* packet;
  int Packet = *Message;
  //***************************
  //Serial.println(Packet,BIN);  //uncomment to print message
  //***************************
  switch(Packet & 15) //4026531840 number to use to get the first 4bits of the message.
  {
    case (0):  //Ping
      Serial.println(" 0");
      if(HorParityCheck(Packet)==0)
      {
        Serial.println("Correct");
      }
      else Serial.println("Wrong");
      VertParityCheck(&Packet,1);
      break;
    case (1):  //Acknowledge
      Serial.println(" 1");
      if(HorParityCheck(Packet)==0)
      {
        Serial.println("Correct");
      }
      else Serial.println("Wrong");
      //Serial.println("Firmware = " && Firmware);
      break;
    case(2):  //EStop
      Serial.println(" 2");
      if(HorParityCheck(Packet)==0)
      {
        Serial.println("Correct");
      }
      else Serial.println("Wrong");
      //FlagEStop = 1;
      FlagStart = 0;
      break;
    case(3):  //Request Commands
      Serial.println(" 3");
      if(HorParityCheck(Packet)==0)
      {
        Serial.println("Correct");
      }
      else Serial.println("Wrong");
      break;
    case(4):  //Start Queue
      Serial.println(" 4");
      HorParityCheck(Packet);
      FlagEStop = 0;
      FlagStart = 1;
      break;
    case(5):  //SetSpeed
      Serial.println(" 5");
      HorParityCheck(Packet);
      MessageSetSpeed StructName;
      //QueueAdd(Message);
      break;
    case(6):  //ToolCMD
      Serial.println(" 6");
      HorParityCheck(Packet);
      //MessageToolCMD StructName;
      //QueueAdd(Message);
      break;
    case(7):  //Move
      Serial.println(" 7");
      HorParityCheck(Packet);
      //QueueAdd(Message);
      break;
   }
   
}

int HorParityCheck(int Message)
{
  boolean CheckBit=0;
  Serial.println(Message,BIN);
  for(int x=1; x<8; x++)
  {
    CheckBit = CheckBit + bitRead(lowByte(Message),x);
  }
  
  if(bitRead(CheckBit,0)==bitRead(Message,0))
  {
    return(0);
  }
  else
  {
    return(-1);
  }
  
}

int VertParityCheck(int *Packet ,int Length)
{
  int CheckByte = 0;
  Serial.println("**************"); 
  Serial.print("Packet = ");
  Serial.println((long) Packet);
  Serial.print("*Packet = ");
  Serial.println(*Packet);
  Serial.print("&Packet = ");
  Serial.println((long) &Packet);
  for(int x=0; x<8; x++)
  {
    int CheckBit = 0;
    for(int y=0; y<Length; y++)
    {
      CheckBit = CheckBit + bitRead(lowByte(*Packet),x);
    }
    bitWrite(CheckByte,x,bitRead(CheckBit,0));
  }
}
