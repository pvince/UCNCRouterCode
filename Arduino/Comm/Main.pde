


#include "Main.h"
#include "Comm.h"
#include "Queue.h"


void setup()
{
  FlagEStop = 1;
  FlagStart = 0;
  ExecutionStep=3;
  Serial.begin(MessageRate);
  Serial.flush();
  //Inputes
  pinMode(XInPulse,INPUT);
  pinMode(YInPulse,INPUT);
  pinMode(ZInPulse,INPUT);
  pinMode(XInDirection,INPUT);
  pinMode(YInDirection,INPUT);
  pinMode(ZInDirection,INPUT);
  attachInterrupt(2,ManualEStop,RISING);  //pin 21
  attachInterrupt(3,XAxisManual,RISING);  //pin 20
  attachInterrupt(4,YAxisManual,RISING);  //pin 19
  attachInterrupt(5,ZAxisManual,RISING);  //pin 18

  //Outputes
  pinMode(XPort,OUTPUT);
  pinMode(YPort,OUTPUT);
  pinMode(ZPort,OUTPUT);
  pinMode(XDirectionPort,OUTPUT);
  pinMode(YDirectionPort,OUTPUT);
  pinMode(ZDirectionPort,OUTPUT);
  //using these pins for debuging
  pinMode(22,OUTPUT);  
  pinMode(24,OUTPUT);
  pinMode(26,OUTPUT);
  pinMode(28,OUTPUT);
  pinMode(30,OUTPUT);
  pinMode(32,OUTPUT);
}

void loop()
{
 
  //Used for debugging
  //digitalWrite(30,LOW);
  //digitalWrite(28,LOW);
  //digitalWrite(26,LOW);
  if(FlagStart && QueueLength > 0) //if the queue us supposed to be executed.
  {
    if(ExecutionStep==3)
    {
      QueueRead();
    }
  }
  
  //Conditionals for when the controller has time to get more messages
 if(FlagStart == 1 && QueueLength<250 && Serial.available()==0 && RequestInProgress <= 0 && NoMoreMessages == 0)
 {
   RequestInProgress = RequestNumber;
   Packet.length=RequestCommandsLength;
   Packet.array[0]=(0x30);
   Packet.array[1]= RequestNumber<<2 + HorParityGen(RequestNumber<<2);
   Packet.array[2]=VertParityGen(&Packet);
   Serial.write(Packet.array[0]);
   Serial.write(Packet.array[1]);
   Serial.write(Packet.array[2]);
   PreviousPacket = Packet;
 }
  if(Serial.available())  //get message on serial buffer if one exists
  {
    if(MessageInProgress==0)  //if this is a "type" message read it into the array at index 0
    {
      int TempType = Serial.read();
      
      if(TempType > 0)
      {
        Packet.array[0] = TempType;
      }  
    }
    if(Packet.array[0] !=0)
    {
      MessageFilter(&Packet);
    }
  }
}

