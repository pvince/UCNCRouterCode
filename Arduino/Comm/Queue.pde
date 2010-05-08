
#include "Main.h"
#include "Comm.h"
#include "Queue.h"
#include "Interrupts.h"




int QueueAdd(PacketContainer* Message)  //Adds messages to the queue
{
  Linklist NewLink;
  if(QueueLength == 0)
  {
    StartPointer = &NewLink;
    WriteLocation = &NewLink;
  }
  else if(QueueLength > 0)
  {
    Linklist* temp = WriteLocation; 
    temp->NextLink = &NewLink;
    WriteLocation = &NewLink;
    
  }
  else 
  {
    return(-1);
  }
  QueueLength++;
  NewLink.MessageLength = Message->length;
  NewLink.NextLink = NULL;
  for(int x = 0; x<Message->length-1; x++) //does not read in the parity
  {
    NewLink.Message[x]=Message->array[x];
  }
  return(0);
}

void QueueRead()  //Reads the oldest link off the queue and sends it to the required function.
{
  digitalWrite(30,HIGH);
  //Linklist *bob = new Linklist();
  Linklist* TempHolder;
  TempHolder = StartPointer;
  digitalWrite(49,HIGH);
  Serial.flush();
  byte temp = TempHolder->Message[0];
  switch((temp & 0b11110000) >>4)
  {
    case (5):        //SetSpeed
    digitalWrite(47,HIGH);
    delay(100);
    digitalWrite(47,LOW);
    SetSpeed(TempHolder);  //Reads packet and insterst speed into axis speed variables.
    break;
    case (6):        //Move
    digitalWrite(53,HIGH);
    delay(100);
    digitalWrite(53,LOW);
    Move(TempHolder);
    break;
    case (7):        //ToolCMD
    digitalWrite(28,HIGH);
    delay(100);
    digitalWrite(28,LOW);
    ToolCMD(TempHolder);
    break;
  default:          //Not Expected
    digitalWrite(22,HIGH);
    delay(100);
    digitalWrite(22,LOW);
    break;
  }
  digitalWrite(49,HIGH); 
  if(QueueLength>1)
  {
    StartPointer = StartPointer->NextLink;   //Uncomment when first link works
    QueueLength--;
  }
  else    //The queue is empty so there is nothing to execute
  {
    QueueLength =0;
    StartPointer = NULL;
    WriteLocation = NULL;
    FlagStart = 0;
  }
  return;
}

void Calculations(int XDiff, int YDiff, int ZDiff)
{//This section attempts to find the number of steps it takes each axis to get to it's location, 
  //the interval of the interupts to create the needed slopes of lines relative to the other axises
  float XRatio;
  float YRatio;
  float ZRatio;
  int unity = 2667;
  //Set the speed of the motors using the speed input plus the max speed possible of the motors
  XSpeed = MaxMotorSpeed + XSpeedSet;
  YSpeed = MaxMotorSpeed + YSpeedSet;
  ZSpeed = MaxMotorSpeed + ZSpeedSet;
  //Find the number of pulses to get to that location
  XDiff = XDiff * Resolution;
  YDiff = YDiff * Resolution;
  ZDiff = ZDiff * Resolution;
  //If the Yaxis is the largest distance traveled
  if((XDiff >= YDiff) && (XDiff >= ZDiff))
  {
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
      TCCR3B = 0;
    }
    else if(YRatio<24)
    {
      unsigned int time = 65536 - (2667 * YRatio);
      for(int x=0; x<8; x++)
      {
        bitWrite(TCNT3H,x,bitRead(time,x+8));
        bitWrite(TCNT3L,x,bitRead(time,x));
      }
    }
    if (ZRatio==0)
    {
      TCCR4B = 0;
    }
    else if(ZRatio<24)
    {
      unsigned int time = 65536 - (2667 * ZRatio);
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
  //Needs to scale timers
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

  int done;
  int XDestination = (int) TempHolder->Message[1]<<13;              //converts the message into positions
  XDestination = XDestination & (int) TempHolder->Message[2]<<6;
  XDestination = XDestination & (int) TempHolder->Message[3]>>1;
  int YDestination = (int) TempHolder->Message[4]<<13;
  YDestination = XDestination & (int) TempHolder->Message[5]<<6;
  YDestination = XDestination & (int) TempHolder->Message[6]>>1;
  int ZDestination = (int) TempHolder->Message[7]<<13;
  ZDestination = XDestination & (int) TempHolder->Message[8]<<6;
  ZDestination = XDestination & (int) TempHolder->Message[9]>>1;  
  int XDiff = XDestination - XPosition;
  int YDiff = YDestination - YPosition;
  int ZDiff = ZDestination - ZPosition;
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
  //SetTimers();
  return(0);
}

int ToolCMD(Linklist* TempHolder)  //Sends signal to disired ports
{
  digitalWrite(PowerPort,TempHolder->Message[7]); //bit 7 holds the on or off signal
  return(0);
}




