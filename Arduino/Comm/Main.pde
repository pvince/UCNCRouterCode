


#include "Main.h"
#include "Comm.h"
#include "Queue.h"
#include "TimerOne.h"

void setup()
{
  Timer1.initialize(MotorSpeed);
  Timer1.attachInterrupt(QueueRead);
  Serial.begin(MessageRate);
  pinMode(12,OUTPUT);
  attachInterrupt(0,Motor,RISING);
}

void loop()
{
  //Priority of actions: (EStop)->(Send next motor action)->(Read incomming Message)->(Request more messges if needed)
  long Message = 0;   
  
  if(FlagEStop)
  {
    noInterrupts();
    FlagStart = 0;  //Stop proccessing the queue
    FlagMotorRunning = 0; //The motors are not running anymore
    //**************************
    //Stop Everything
    //**************************
  }
    if(!FlagEStop ) //if the queue us supposed to be executed.
  {
    //********************************
    //Process Queue
    //somehow figure out when the motors are done, delay?
    //FlagMotorDelay=0;
    //********************************
    interrupts();
    //FlagMotorRunning=1;  //set the motor flag as not being ready.
  }
  if(QueueLength<250)
  {
      Serial.print("MoreMessages");  //Ask computer for more messages.
      PacketContainer Packet;
      Packet.length=3;
      Packet.array[0]=(0x30);
      char Amount = RequestNumber<<2 + HorParityGen(RequestNumber<<2);
      Packet.array[1]=Amount;
      Packet.array[2]=VertParityGen(&Packet);
      Serial.write(Packet.array[0]);
      Serial.write(Packet.array[1]);
      Serial.write(Packet.array[2]);
  }
  if(Serial.available()) //get message on serial buffer if one exists
  {
    PacketContainer Packet;
    if(MessageInProgress==0)  //if this is a "type" message read it into the array at index 0
    {
      Packet.array[0] = Serial.read();
    }
      MessageFilter(&Packet);
  }
}


void Motor()
{
  QueueRead();
}
