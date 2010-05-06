


#include "Main.h"
#include "Comm.h"
#include "Queue.h"


void setup()
{
  Serial.begin(MessageRate);
  Serial.flush();
  pinMode(12,OUTPUT);
  pinMode(52,OUTPUT);
  pinMode(53,OUTPUT);
  pinMode(47,OUTPUT);
  pinMode(22,OUTPUT);
  pinMode(28,OUTPUT);
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
  }
  if(FlagStart==1)
  {
    QueueRead();
  }
  digitalWrite(52,LOW);
  if(!FlagEStop ) //if the queue us supposed to be executed.
  {
    if(ExecutionStep==3)
    {
      QueueRead();
    }
  }
  if(QueueLength<250 && FlagStart==1 && Serial.available()==0 && MessageInProgress == 0)
  {
//      Serial.print("MoreMessages");  //Ask computer for more messages.
//      PacketContainer Packet;
//      Packet.length=3;
//      Packet.array[0]=(0x30);
//      char Amount = RequestNumber<<2 + HorParityGen(RequestNumber<<2);
//      Packet.array[1]=Amount;
//      Packet.array[2]=VertParityGen(&Packet);
//      Serial.write(Packet.array[0]);
//      Serial.write(Packet.array[1]);
//      Serial.write(Packet.array[2]);
  }
  if(Serial.available()) //get message on serial buffer if one exists
  {
    digitalWrite(52,HIGH);
    PacketContainer Packet;
    if(MessageInProgress==0)  //if this is a "type" message read it into the array at index 0
    {
      int TempType = Serial.read();
      
      if(TempType > 0)
      {
        Packet.array[0] = TempType;
      }
      
    }
    digitalWrite(52,LOW);
    digitalWrite(53,HIGH);
    if(Packet.array[0] !=0)
    {
      MessageFilter(&Packet);
    }
    digitalWrite(53,LOW);
    
  }
}


void Motor()
{
  QueueRead();
}
