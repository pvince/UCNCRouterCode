


#include "Main.h"
#include "Comm.h"
#include "Queue.h"

void setup()
{
  Serial.begin(MessageRate);
  pinMode(12,OUTPUT);
  for (int x=0;x<500;x++)  //Makes sure the Queue is blank.
  {
    Queue[x]=0;
  }
}

void loop()
{
  //Priority of actions: (EStop)->(Send next motor action)->(Read incomming Message)->(Request more messges if needed)
  long Message = 0;   
  if(FlagEStop)
  {
    FlagStart = 0;  //Stop proccessing the queue
    //**************************
    //Stop Everything
    //**************************
  }
  else if(!FlagMotorDelay &&  FlagStart) //if the motors are not doing anything and are ready to move again.
  {
    FlagMotorDelay=1;  //set the motor flag as not being ready.
    //********************************
    //Process Queue
    //somehow figure out when the motors are done, delay?
    //FlagMotorDelay=0;
    //********************************
  }
  else if(Serial.available()) //get message on serial buffer if one exists
  {
    int* Message;
    *Message = Serial.read();
    //    ErrorCheck((long*) Serial.read(),int Serial.available());
     MessageFilter(Message);
  }
  else if(QueueLength<250)
  {
    //Serial.print("MoreMessages");  //Ask computer for more messages.
  }
  //Serial.println(1);
  //delay(3000);
}

