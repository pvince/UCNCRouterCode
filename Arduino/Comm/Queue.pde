
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
    //Used for debuging, shows the message recieved on the serial buss
    //Serial.write(Message->array[x]);
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

void calcRatioTimes(unsigned int Gd, unsigned int Md, unsigned int Ld, 
                    float &Gr, float &Mr, float &Lr, 
                    unsigned int &Gt, unsigned int &Mt, unsigned int &Lt) {
  Gr = 1;
  if(Md != 0) 
    Mr = (float)Gd / (float)Md;
  else  
    Mr=0;
    
  if(Ld != 0) 
    Lr = (float)Gd / (float)Ld;
  else 
    Lr=0;

  Gt = 62869;
  if (Mr==0) 
    Mt = 0;  //turn off interupt if there is no movement
  else if(Lr<24) 
    Mt = 65536 - (2667 * Mr);
    
  if (Lr==0)  
    Lt = 0;  //turn off interupt if there is no movement.
  else if(Lr<24) 
    Lt = 65536 - (2667 * Lr);
}

void Calculations(unsigned int XDiff, unsigned int YDiff, unsigned int ZDiff)
{//This section attempts to find the number of steps it takes each axis to get to it's location, 
  //the interval of the interupts to create the needed slopes of lines relative to the other axises
  float XRatio;
  float YRatio;
  float ZRatio;
  unsigned int XPulseRate;
  unsigned int YPulseRate;
  unsigned int ZPulseRate;
  //If the Xaxis is the largest distance traveled
  if((XDiff >= YDiff) && (XDiff >= ZDiff))
  {
    calcRatioTimes(XDiff, YDiff, ZDiff, XRatio, YRatio, ZRatio, XTime, YTime, ZTime);
  }
  
  //If the Yaxis is the largest distance traveled
  else if((YDiff >= XDiff) && (YDiff >= ZDiff))
  {
    calcRatioTimes(YDiff, XDiff, ZDiff, YRatio, XRatio, ZRatio, YTime, XTime, ZTime);
  }
  //If the Zaxis is the largest distance traveled
  else if((ZDiff >= XDiff) && (ZDiff >= YDiff))
  {
    calcRatioTimes(ZDiff, XDiff, YDiff, ZRatio, XRatio, YRatio, ZTime, XTime, YTime);
  }
  SetTimers(1,1,1);
  return;
}

int SetSpeed(Linklist* TempHolder)  //Sends signal to desired ports
{
  int temp = 0;
  temp = ((TempHolder->Message[1] & 0b11111110) << 8) | ((TempHolder->Message[2] & 0b11111110) << 1) | ((TempHolder->Message[3] & 0b00000110) >> 1);
  if (TempHolder->Message[0] & 0b00001000)
  {
    XSpeedSet = temp;
  }
  if (TempHolder->Message[0] & 0b00000100)
  {
    YSpeedSet = temp;
  }
  if (TempHolder->Message[0] & 0b00000010)
  {
    ZSpeedSet = temp;
  }
  return(0);
}

int Move(Linklist* TempHolder)  //Sends signal to disired ports
{
  //converts the message into positions
  int XDestination = ((int) TempHolder->Message[1] & 0b11111110)<<8;
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

  //Set direction of each motor
  if (XDiff>=0) XDirection=1;
  else XDirection=0;
  if (YDiff>=0) YDirection=1;
  else YDirection=0;
  if (ZDiff>=0) ZDirection=1;
  else ZDirection=0;

  //Calculate interupt periods to get desired slopes
  Calculations(XDiff, YDiff, ZDiff);
  return(0);
}
int ToolCMD(Linklist* TempHolder)  //Sends signal to disired ports
{
  digitalWrite(PowerPort,TempHolder->Message[0] & 0b00000010); //bit 7 holds the on or off signal
  return(0);
}




