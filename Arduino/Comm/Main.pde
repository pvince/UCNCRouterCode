


#include "Main.h"
#include "Comm.h"
#include "Queue.h"


void setup()
{
   //disable overflow interupts
  //TIMSK1 |= ~_BV(TOIE1);
  //TIMSK3 |= ~_BV(TOIE3);
  //TIMSK4 |= ~_BV(TOIE4);
  Serial.begin(MessageRate);
  Serial.flush();
  pinMode(22,OUTPUT);  //using these pins for debuging
  pinMode(24,OUTPUT);
  pinMode(26,OUTPUT);
  pinMode(28,OUTPUT);
  pinMode(30,OUTPUT);
  pinMode(32,OUTPUT);
}

void loop()
{
  //Priority of actions: (EStop)->(Send next motor action)->(Read incomming Message)->(Request more messges if needed)
  if(FlagEStop)
  {
    cli();
  }
  
//Debugging: I don't think this is supposed to be here.
  if(FlagStart==1)
  {
    QueueRead();
  }

  
//  if(FlagStart) //if the queue us supposed to be executed.
//  {
//    if(ExecutionStep==3)
//    {
//      QueueRead();
//    }
//  }
  
  //Conditionals for when the controller has time to get more messages
 // if(QueueLength<250 && FlagStart==1 && Serial.available()==0 && MessageInProgress == 0)
  //{
//      Serial.print("MoureMessages");  //Ask computer for more messages.
//      PacketContainer Packet;
//      Packet.length=3;
//      Packet.array[0]=(0x30);
//      char Amount = RequestNumber<<2 + HorParityGen(RequestNumber<<2);
//      Packet.array[1]=Amount;
//      Packet.array[2]=VertParityGen(&Packet);
//      Serial.write(Packet.array[0]);
//      Serial.write(Packet.array[1]);
//      Serial.write(Packet.array[2]);
 // }
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


//  void Motor()
//  {
//    QueueRead();
//  }
