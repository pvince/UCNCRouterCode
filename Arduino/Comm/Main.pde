


#include "Main.h"
#include "Comm.h"
#include "Queue.h"
#include "Interrupts.h"

void setup()
{
  FlagEStop = 1;
  FlagStart = 0;
  ExecutionStep=3;
  Serial.begin(MessageRate);
  Serial.flush();
  //Inputes
  pinMode(XInPulsePort,INPUT);
  pinMode(YInPulsePort,INPUT);
  pinMode(ZInPulsePort,INPUT);
  pinMode(XInDirection,INPUT);
  pinMode(YInDirection,INPUT);
  pinMode(ZInDirection,INPUT);
  EICRA = 255;
  EIMSK = 15;
  
//  attachInterrupt(2,ManualEStop,RISING);  //pin 21
//  attachInterrupt(3,XAxisManual,RISING);  //pin 20
//  attachInterrupt(4,YAxisManual,RISING);  //pin 19
//  attachInterrupt(5,ZAxisManual,RISING);  //pin 18

  //Outputes
  pinMode(XPulsePort,OUTPUT);
  pinMode(YPulsePort,OUTPUT);
  pinMode(ZPulsePort,OUTPUT);
  pinMode(XDirectionPort,OUTPUT);
  pinMode(YDirectionPort,OUTPUT);
  pinMode(ZDirectionPort,OUTPUT);
  pinMode(13,OUTPUT);
  pinMode(52,OUTPUT);
  //Used for debugging the ask for messages function
  pinMode(47,OUTPUT);
  pinMode(45,OUTPUT);
  pinMode(43,OUTPUT);
  pinMode(41,OUTPUT);
  pinMode(39,OUTPUT);
}

void loop()
{
  if(FlagStart && QueueLength > 0) //if the queue us supposed to be executed.
  {
    if(ExecutionStep==3)
    {
      digitalWrite(52,HIGH);
      QueueRead();
    }
  }
  //Conditionals for when the controller has time to get more messages
 if(FlagStart==1){
   digitalWrite(47,HIGH);
   digitalWrite(52,LOW);}
 else{
   digitalWrite(47,LOW);}
 if(QueueLength<250){
   digitalWrite(45,HIGH);}
 else{
   digitalWrite(45,LOW);}
 if(Serial.available()==0){
   digitalWrite(43,HIGH);}
 else{
   digitalWrite(43,LOW);}
 if(RequestInProgress <=0){
   digitalWrite(41,HIGH);}
 else{
   digitalWrite(41,LOW);}
 if(NoMoreMessages == 0){
   digitalWrite(39,HIGH);}
 else{
   digitalWrite(39,LOW);}

 if(FlagStart == 1 && QueueLength<250 && Serial.available()==0 && RequestInProgress <= 0 && NoMoreMessages==0)
 {
   RequestInProgress = RequestNumber;
   Packet.length=RequestCommandsLength;
   Packet.array[0]=(0x30);
   Packet.array[1]= RequestNumber<<1 + HorParityGen(RequestNumber<<1);
   Packet.array[2]=VertParityGen(&Packet);
   PreviousPacket = Packet;
   for (int x=0; x<RequestCommandsLength; x++)
    {
      Serial.write(Packet.array[x]);
    }
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

