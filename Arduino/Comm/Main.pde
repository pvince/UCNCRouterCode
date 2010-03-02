


#include "Main.h"
#include "Comm.h"
#include "Queue.h"
static PacketContainer Packet;

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
    if(MessageInProgress==0)  //if this is a "type" message read it into the array at index 0
    {
      Packet.array[0] = Serial.read();
    }
      MessageFilter(&Packet);
  }
  else if(QueueLength<250 && FlagMotorDelay)
  {
    //Serial.print("MoreMessages");  //Ask computer for more messages.
    PacketContainer* Packet;
    Packet->length=3;
    Packet->array[0]=(0x30);
    char Amount = RequestNumber*2 + HorParityGen(RequestNumber*2);
    Packet->array[1]=Amount;
    Packet->array[2]=VertParityGen(Packet);
    Serial.write(Packet->array[0]);
    Serial.write(Packet->array[1]);
    Serial.write(Packet->array[2]);
  }
}

