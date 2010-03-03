//Comm.pde
//Discription:This takes care  of the lower level comunication tasks.


#include "Main.h"
#include "Queue.h"
#include "Comm.h"



int MessageFilter(PacketContainer* Packet)
{
  //Serial.write(Packet->array[0]);
  byte Message=Packet->array[0];
  switch(Message & 0b11110000)  //looks at the type bits
  {
    case (0):  //Ping
      Packet->length=PingLength;
      //AcknowledgeMessage(0);
      if(Serial.available()==Packet->length-1)
      {
        for(int x=1; x<Packet->length; x++)
        {
          Packet->array[x]=Serial.read();          
        }
        RecievePing(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case (1):  //Acknowledge
      Packet->length=2;
      if(Serial.available()==Packet->length-1)
      {
        RecievePing(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case(2):  //EStop
      Packet->length=EStopLength;
      Serial.write(0xDD);
      if(Serial.available()==Packet->length-1)
      {
        RecieveEStop(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case(3):  //Request Commands
      Packet->length=RequestCommandsLength;
      if(Serial.available()==Packet->length-1)
      {
        RecieveRequestCommands(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case(4):  //Start Queue
      Packet->length=StartQueueLength;
      if(Serial.available()==Packet->length-1)
      {
        RecieveStartQueue(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case(5):  //SetSpeed
      Packet->length=SetSpeedLength;
      if(Serial.available()==Packet->length-1)
      {
        RecieveSetSpeed(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case(6):  //Move
      Packet->length=MoveLength;
      if(Serial.available()==Packet->length-1)
      {
        RecieveMove(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
    case(7):  //ToolCMD
      Packet->length=ToolCMDLength;
      if(Serial.available()==Packet->length-1)
      {
        RecieveToolCMD(Packet);
        MessageInProgress=0;
      }
      else
      {
        MessageInProgress=1;
      }
      break;
  }

}
int AcknowledgeMessage(boolean Error)
{
  if(Error==0)
  {
    Serial.write(0x11);  //No error
  }
  else
  {
    Serial.write(0x12);  //Error
    Serial.flush();
  }
  return(0);
}

int RequestCMD(byte Number)
{
  PacketContainer Packet;
  Packet.array[0]=48;
  Packet.array[1]=HorParityGen(Number<<1);
  
}

boolean HorParityGen(char Message)
{
  boolean CheckBit=0;
  for(int x=1; x<8; x++)
  {
    CheckBit = CheckBit + bitRead(Message,x);
  }
  return bitRead(CheckBit,0);
}

unsigned char VertParityGen(PacketContainer* Packet)
{
  unsigned char CheckByte = 0;
  for(int x=0; x<8; x++)
  {
    unsigned char CheckBit = 0;
    for(int y=0; y<Packet->length-1; y++)
    {
      CheckBit = CheckBit + bitRead(Packet->array[y],x);
    }
    bitWrite(CheckByte,x,bitRead(CheckBit,0));
  }
  return(CheckByte);
}

unsigned char ParityChecker(PacketContainer* Packet)
{
   if(HorParityCheck(Packet->array[0]) !=0)
  {
    return(-1);
  }
  
  for(int x=1; x<Packet->length; x++)
  {
    if(HorParityCheck(Packet->array[x]) !=0)
    {
      return(-1);
    }
  }
  return(VertParityCheck(Packet));
}


//**************************************************************************
//**************************************************************************
//**                     Parity Checks                                    **
//**************************************************************************
//**************************************************************************
int HorParityCheck(char Message)
{
  if(HorParityGen(Message)==bitRead(Message,0))
  {
    return(0);
  }
  else
  {
    Serial.write(0xFF);
    Serial.write(Message);
    delay(10);
    Serial.write(bitRead(Message,0));
    delay(10);
    Serial.write(HorParityGen(Message));
    return(-1);
  }

}

int VertParityCheck(PacketContainer *Packet)  //Checks the parity packet with the rest of the message
{
  if(Packet->array[Packet->length-1]==VertParityGen(Packet))  
  {
    return(0);
  }
  else  
  {
    Serial.write(0xEE);
    Serial.write(Packet->length-1);
    delay(10);
    Serial.write(Packet->array[(Packet->length)-1]);
    delay(10);
    Serial.write(VertParityGen(Packet));
    return(-1);
  }
}
//**************************************************************************
//**                    End Parity Checks                                 **
//**************************************************************************
