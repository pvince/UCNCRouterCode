
#include "Main.h"
//#include "Comm.h"
//#include "Queue.h"
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
  else return(-1);
  QueueLength++;
  NewLink.MessageLength = Message->length;
  NewLink.NextLink = NULL;
  for(int x = 0; x<Message->length-1; x++) //does not read in the parity
  {
    NewLink.Message[x]=Message->array[x];
  }
  Serial.write(StartPointer->Message[0]);
  return(0);
}

void QueueRead()  //Reads the oldest link off the queue and sends it to the required function.
{
  Linklist* TempHolder;
  TempHolder = StartPointer;
  byte Type = StartPointer->Message[0]>>4;
  switch(Type)
  {
    case (5):
    SetSpeed(TempHolder);  //Reads packet and insterst speed into axis speed variables.
    break;
    case (6):
    Move(TempHolder);

    break;
    case (7):
    ToolCMD(TempHolder);
    break;
  }
  if(QueueLength>1)
  {
    StartPointer = StartPointer->NextLink;
    QueueLength--;
  }
  else
  {
    QueueLength =0;
    StartPointer = NULL;
    FlagStart = 0;
  }

}

void Calculations(int XDiff, int YDiff, int ZDiff)
{
  float XRatio;
  float YRatio;
  float ZRatio;
  //Set the speed of the motors using the speed input plus the max speed possible of the motors
  XSpeed = MaxMotorSpeed + XSpeedSet;
  YSpeed = MaxMotorSpeed + YSpeedSet;
  ZSpeed = MaxMotorSpeed + ZSpeedSet;
  XDiff = XDiff * Resolution;
  YDiff = YDiff * Resolution;
  ZDiff = ZDiff * Resolution;
  if((XDiff >= YDiff) && (XDiff >= ZDiff))
  {
    XRatio = 1;
    YRatio = (float)YDiff / (float)XDiff;
    ZRatio = (float)ZDiff / (float)XDiff;
  }
  else if((YDiff >= XDiff) && (YDiff >= ZDiff))
  {
    YRatio = 1;
    XRatio = (float)XDiff / (float)YDiff;
    ZRatio = (float)ZDiff / (float)YDiff;
  }
  else if((ZDiff >= XDiff) && (ZDiff >= YDiff))
  {
    ZRatio = 1;
    XRatio = (float)XDiff / (float)ZDiff;
    YRatio = (float)YDiff / (float)ZDiff;
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
  SetTimers();
}

int ToolCMD(Linklist* TempHolder)  //Sends signal to disired ports
{
  digitalWrite(PowerPort,TempHolder->Message[7]); //bit 7 holds the on or off signal
  return(0);
}


