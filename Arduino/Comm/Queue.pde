
#include "Main.h"
#include "Comm.h"
#include "Queue.h"
#include "Interrupts.h"




int QueueAdd(PacketContainer* Message)  //Adds messages to the queue
{
  QueueLength++;
  Queue[WriteLocation].MessageLength = Message->length;
  for(int x = 0; x<Message->length; x++) //does not read in the parity
  {
    Queue[WriteLocation].Message[x]=Message->array[x];
    Serial.write(Message->array[x]);
  }
  if (WriteLocation < MaxQueueLength-1)  //if it has not reached the top of the queue yet.
  {
    WriteLocation++;
  }
  else    //if it was on the last object in teh queue.
  {
    WriteLocation=0;
  }
  return(0);
}

void QueueRead()  //Reads the oldest link off the queue and sends it to the required function.
{
  byte temp = Queue[ReadLocation].Message[0];
  switch((temp & 0b11110000) >>4)
  {
    case (5):        //SetSpeed
      SetSpeed(&Queue[ReadLocation]);  //Reads packet and insterst speed into axis speed variables.
      break;
    case (6):        //Move
      Move(&Queue[ReadLocation]);
      break;
    case (7):        //ToolCMD
      ToolCMD(&Queue[ReadLocation]);
      break;
    default:          //Not Expected
      break;
  }
  QueueLength--;
  if(QueueLength>1)
  {
    if(ReadLocation < MaxQueueLength-1)
    {
      ReadLocation++;
    }
    else
    {
      ReadLocation=0;
    }
  }
  else    //The queue is empty so there is nothing to execute
  {
    QueueLength =0;
    ReadLocation = 0;
    WriteLocation = 0;
    FlagStart = 0;
  }
  return;
}

void Calculations(unsigned int XDiff, unsigned int YDiff, unsigned int ZDiff)
{//This section attempts to find the number of steps it takes each axis to get to it's location, 
  //the interval of the interupts to create the needed slopes of lines relative to the other axises
  float XRatio;
  float YRatio;
  float ZRatio;
  unsigned int XTime;
  unsigned int YTime;
  unsigned int ZTime;
  int unity = 2667;
  //Set the speed of the motors using the speed input plus the max speed possible of the motors
  XSpeed = MaxMotorSpeed + XSpeedSet;
  YSpeed = MaxMotorSpeed + YSpeedSet;
  ZSpeed = MaxMotorSpeed + ZSpeedSet;
  //Find the number of pulses to get to that location
  XDiff *= Resolution;
  YDiff *= Resolution;
  ZDiff *= Resolution;
  Serial.write(XDiff>>8);
  Serial.write(XDiff);
  Serial.write(YDiff>>8);
  Serial.write(YDiff);
  Serial.write(ZDiff>>8);
  Serial.write(ZDiff);
  //If the Xaxis is the largest distance traveled
  if((XDiff >= YDiff) && (XDiff >= ZDiff))
  {
    digitalWrite(22,HIGH);
    XRatio = 1;
    if(YDiff != 0)
    {
      YRatio = (float)XDiff / (float)YDiff;
    }
    else 
    {
      YRatio=0;
    }
    if(ZDiff != 0)
    {
      ZRatio = (float)XDiff / (float)ZDiff;
    }
    else
    {
      ZRatio=0;
    }
    TCNT1H = 245;  //62869 unity value causes 6000 pps and 0 clock scaling
    TCNT1L = 149;
    if (YRatio==0)
    {
      TCCR3B = 0;  //turn off interupt if there is no movement
    }
    else if(YRatio<24)
    {
      unsigned int time = 65536 - (2667 * YRatio);
      YTime = time;
      for(int x=0; x<8; x++)
      {
        bitWrite(TCNT3H,x,bitRead(time,x+8));
        bitWrite(TCNT3L,x,bitRead(time,x));
      }  
    }
    if (ZRatio==0)
    {
      TCCR4B = 0;  //turn off interupt if there is no movement.
    }
    else if(ZRatio<24)
    {
      unsigned int time = 65536 - (2667 * ZRatio);
      ZTime = time;
      for(int x=0; x<8; x++)
      {
        bitWrite(TCNT4H,x,bitRead(time,x+8));
        bitWrite(TCNT4L,x,bitRead(time,x));
      }
    }
  }
  
  
  
  //If the Yaxis is the largest distance traveled
  else if((YDiff >= XDiff) && (YDiff >= ZDiff))
  {
    YRatio = 1;
    if(XDiff != 0 )
    {
      XRatio = (float)YDiff / (float)XDiff;
    }
    else
    {
      XRatio=0;
    }
    if(ZDiff != 0)
    {
      ZRatio = (float)YDiff / (float)ZDiff;
    }
    else
    {
      ZRatio=0;
    }
    TCNT3H = 245;  //62869 unity value causes 6000 pps and 0 clock scaling
    TCNT3L = 149;
  }
  
  
  
  //If the Zaxis is the largest distance traveled
  else if((ZDiff >= XDiff) && (ZDiff >= YDiff))
  {
    digitalWrite(26,HIGH);
    ZRatio = 1;
    if(XDiff != 0 )
    {
      XRatio = (float)ZDiff / (float)XDiff;
    }
    else
    {
      XRatio=0;
    }
    if(YDiff != 0)
    {
      YRatio = (float)ZDiff / (float)YDiff;
    }
    else 
    {
      YRatio=0;
    }
    TCNT4H = 245;  //62869 unity value causes 6000 pps and 0 clock scaling
    TCNT4L = 149;
  }
  XPulseCount = XDiff;
  YPulseCount = YDiff;
  ZPulseCount = ZDiff;
  SetTimers(XTime,YTime,ZTime);
  return;
}

int SetSpeed(Linklist* TempHolder)  //Sends signal to desired ports
{
  int temp;
  temp = ((TempHolder->Message[1] & 0b11111110) << 8) | ((TempHolder->Message[3] & 0b00000100) << 6) | (TempHolder->Message[2] & 0b11111110) | ((TempHolder->Message[3] & 0b00000010) >> 1);
  if (TempHolder->Message[0] & 0b00001000)
  {
    XSpeedSet = temp;
  }
  if (TempHolder->Message[0] & 0b00000100)
  {
    XSpeedSet = temp;
  }
  if (TempHolder->Message[0] & 0b00000010)
  {
    ZSpeedSet = temp;
  }
  return(0);
}

int Move(Linklist* TempHolder)  //Sends signal to disired ports
{
  //int done;
  int XDestination = ((int) TempHolder->Message[1] & 0b11111110)<<8;    //converts the message into positions
  XDestination = XDestination | ((int) TempHolder->Message[2] & 0b11111110)<<1;
  XDestination = XDestination | ((int) TempHolder->Message[3] & 0b00000110)>>1;
  int YDestination = ((int) TempHolder->Message[4] & 0b11111110)<<8;
  YDestination = YDestination | ((int) TempHolder->Message[5] & 0b11111110)<<1;
  YDestination = YDestination | ((int) TempHolder->Message[6] & 0b00000110)>>1;
  int ZDestination = ((int) TempHolder->Message[7] & 0b11111110)<<8;
  ZDestination = ZDestination | ((int) TempHolder->Message[8] & 0b11111110)<<1;
  ZDestination = ZDestination | ((int) TempHolder->Message[9] & 0b00000110)>>1;  
  unsigned int XDiff = XDestination - XPosition;
  unsigned int YDiff = YDestination - YPosition;
  unsigned int ZDiff = ZDestination - ZPosition;
  if (XDiff>=0)
  {
    XDirection=1;
  }
  else
  {
    XDirection=0;
  }
  if (YDiff>=0)
  {
    YDirection=1;
  }
  else
  {
    YDirection=0;
  }
  if (ZDiff>=0)
  {
    ZDirection=1;
  }
  else
  {
    ZDirection=0;
  }
  Calculations(XDiff, YDiff, ZDiff);
  return(0);
}
int ToolCMD(Linklist* TempHolder)  //Sends signal to disired ports
{
  digitalWrite(PowerPort,TempHolder->Message[7]); //bit 7 holds the on or off signal
  return(0);
}




