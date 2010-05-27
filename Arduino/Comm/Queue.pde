
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
  MoveDetails MD;
  MD.XDiff = 0;
  MD.YDiff = 0;
  MD.ZDiff = 0;
  MD.XRatio = 0;
  MD.YRatio = 0;
  MD.ZRatio = 0;
  MD.XPulseRate = 0;
  MD.YPulseRate = 0;
  MD.ZPulseRate = 0;
  MD.XScalar = 0;
  MD.YScalar = 0;
  MD.ZScalar = 0;
  MD.XSpeed = 4;
  MD.YSpeed = 4;
  MD.ZSpeed = 4;
  switch((temp & 0b11110000) >>4)
  {
    case (5):        //SetSpeed
      SetSpeed(&Queue[ReadLocation], MD);  //Reads packet and insterst speed into axis speed variables.
      break;
    case (6):        //Move
      Move(&Queue[ReadLocation], MD);
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
    //FlagStart=0;
  }
  return;
}

void calcRatio(unsigned int& DiffBig, unsigned int& DiffOne, unsigned int& DiffTwo, 
                    float& RatioBig, float& RatioOne, float& RatioTwo) {
  RatioBig = 1;
  if(DiffOne != 0) 
    RatioOne = (float)DiffBig / (float)DiffOne;
  else  
    RatioOne=0;
    
  if(DiffTwo != 0) 
    RatioTwo = (float)DiffBig / (float)DiffTwo;
  else 
    RatioTwo=0;
//  Serial.write(RatioOne);
//  Serial.write(250);
}

void calcPulseRate(MoveDetails& MD){
  if(MD.XRatio==1)
    MD.XPulseRate = 1000/MD.XSpeed; 
  else
    MD.XPulseRate = 1000/(MD.XRatio*MD.XSpeed);
  
  if(MD.YRatio==1)
    MD.YPulseRate = 1000/MD.YSpeed; 
  else
    MD.YPulseRate = 1000/(MD.YRatio*MD.YSpeed);
  
  if(MD.YRatio==1)
    MD.ZPulseRate = 1000/MD.ZSpeed; 
  else
    MD.ZPulseRate = 1000/(MD.ZRatio*MD.ZSpeed);
//  Serial.write(MD.XRatio);
//  Serial.write(MD.XSpeed>>8);
//  Serial.write(MD.XSpeed);
//  Serial.write(MD.XPulseRate>>8);
//  Serial.write(MD.XPulseRate);
//  Serial.write(251);
}

void calcScalar(unsigned int& pulseRate, int& scalar) {
  if(pulseRate > 350) {
      scalar = 1;
  } else if(pulseRate > 50) {
      scalar = 2;
  } else if(pulseRate > 10){
      scalar = 3;
  } else if(pulseRate > 1){
      scalar = 5;
  }else{
      scalar = 0;
  }
//  Serial.write(pulseRate);
//  Serial.write(scalar);
//  Serial.write(252);
}

void calcTimes(MoveDetails MD){
   XTime = 256 - ((1/MD.XPulseRate)*Frequency/256/MD.XScalar);
   YTime = 256 - ((1/MD.YPulseRate)*Frequency/256/MD.YScalar);
   ZTime = 256 - ((1/MD.ZPulseRate)*Frequency/256/MD.ZScalar);
//   Serial.write(XTime>>8);
//   Serial.write(XTime);
//   Serial.write(253);
}

void Calculations(MoveDetails MD)
{//This section attempts to find the number of steps it takes each axis to get to it's location, 
  //the interval of the interupts to create the needed slopes of lines relative to the other axises
  
  //If the Xaxis is the largest distance traveled
  if((MD.XDiff >= MD.YDiff) && (MD.XDiff >= MD.ZDiff))
  {
    //Turn diffs to ratios and time intervals
    calcRatio(MD.XDiff, MD.YDiff, MD.ZDiff, MD.XRatio, MD.YRatio, MD.ZRatio);
  }
  //If the Yaxis is the largest distance traveled
  else if((MD.YDiff >= MD.XDiff) && (MD.YDiff >= MD.ZDiff))
  {
    calcRatio(MD.YDiff, MD.XDiff, MD.ZDiff, MD.YRatio, MD.XRatio, MD.ZRatio);
  }
  //If the Zaxis is the largest distance traveled
  else if((MD.ZDiff >= MD.XDiff) && (MD.ZDiff >= MD.YDiff))
  {
    calcRatio(MD.ZDiff, MD.XDiff, MD.YDiff, MD.ZRatio, MD.XRatio, MD.YRatio);
  }
  calcPulseRate(MD);
  calcScalar(MD.XPulseRate,MD.XScalar);
  calcScalar(MD.YPulseRate,MD.YScalar);
  calcScalar(MD.ZPulseRate,MD.ZScalar);
  calcTimes(MD);
//  Serial.write(MD.XScalar);
//  Serial.write(MD.YScalar);
//  Serial.write(MD.ZScalar);
  //Serial.write(255);
  SetTimers(MD);
  return;
}

int SetSpeed(Linklist* TempHolder, MoveDetails& MD)  //Sends signal to desired ports
{
  int temp = 0;
  temp = ((TempHolder->Message[1] & 0b11111110) << 8) | ((TempHolder->Message[2] & 0b11111110) << 1) | ((TempHolder->Message[3] & 0b00000110) >> 1);
  if (TempHolder->Message[0] & 0b00001000)
  {
    MD.XSpeed = temp/60; //converts it from distance/minute to distance/second
  }
  if (TempHolder->Message[0] & 0b00000100)
  {
    MD.YSpeed = temp/60; //converts it from distance/minute to distance/second
  }
  if (TempHolder->Message[0] & 0b00000010)
  {
    MD.ZSpeed = temp/60; //converts it from distance/minute to distance/second
  }
  return(0);
}

int Move(Linklist* TempHolder, MoveDetails& MD)  //Sends signal to disired ports
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
  MD.XDiff = XDestination - XPosition;
  MD.YDiff = YDestination - YPosition;
  MD.ZDiff = ZDestination - ZPosition;
  XPulseCount = MD.XDiff;
  YPulseCount = MD.YDiff;
  ZPulseCount = MD.ZDiff;
  //Set direction of each motor
  if (MD.XDiff>=0) XDirection=1;
  else XDirection=0;
  if (MD.YDiff>=0) YDirection=1;
  else YDirection=0;
  if (MD.ZDiff>=0) ZDirection=1;
  else ZDirection=0;

  //Calculate interupt periods to get desired slopes
  Calculations(MD);
  return(0);
}
int ToolCMD(Linklist* TempHolder)  //Sends signal to disired ports
{
  digitalWrite(PowerPort,TempHolder->Message[0] & 0b00000010); //bit 7 holds the on or off signal
  return(0);
}




