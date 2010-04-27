  
#include "Main.h"
#include "Comm.h"
#include "Queue.h"
#include "TimerOne.h"


//
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

int SetSpeed(Linklist* TempHolder)  //Sends signal to desired ports
{
  int temp;
  temp = ((TempHolder->Message[1] & 0b11111110) << 8) | ((TempHolder->Message[3] & 0b00000100) << 6) | (TempHolder->Message[2] & 0b11111110) | ((TempHolder->Message[3] & 0b00000010) >> 1);
  if (TempHolder->Message[0] & 0b00001000)
  {
    XSpeed = temp;
  }
  if (TempHolder->Message[0] & 0b00000100)
  {
    XSpeed = temp;
  }
  if (TempHolder->Message[0] & 0b00000010)
  {
    ZSpeed = temp;
  }
  return(0);
}

int Move(Linklist* TempHolder)  //Sends signal to disired ports
{
  Timer2.initialize(65000);
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
}

int ToolCMD(Linklist* TempHolder)  //Sends signal to disired ports
{
  digitalWrite(PowerPort,TempHolder->Message[7]); //bit 7 holds the on or off signal
  return(0);
}

void XAxisISR()
{
  if(XPulseCount>0)
  {
    XPulseCount--;
    digitalWrite(XDirectionPort,XDirection);
    digitalWrite(XPort,HIGH);
    digitalWrite(XPort,HIGH);
    digitalWrite(XPort,HIGH);
    digitalWrite(XPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    digitalWrite(XPort,LOW);
  }
  else
  {
    ExecutionStep++;
    detachInterrupt(0);
  }
  
}

void YAxisISR()
{
  if(YPulseCount>0)
  {
    YPulseCount--;
    digitalWrite(YDirectionPort,YDirection);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    digitalWrite(YPort,LOW);
  }
  else
  {
    ExecutionStep++;
    detachInterrupt(1);
  }
  
}

void ZAxisISR()
{
  if(ZPulseCount>0)
  {
    ZPulseCount--;
    digitalWrite(ZDirection,ZDirection);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    digitalWrite(ZPort,LOW);
  }
  else
  {
    ExecutionStep++;
    detachInterrupt(2);
  }
  
}
