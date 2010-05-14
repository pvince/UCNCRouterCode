//Comm.pde
//Discription:This takes care  of the lower level comunication tasks.


#include "Main.h"
#include "Queue.h"
#include "Comm.h"



int MessageFilter(PacketContainer* Packet)
{
  //Serial.write(Packet->array[0]);
  byte Message=Packet->array[0];
  digitalWrite(13,HIGH);  //indicate a message is in progress
  switch((Message & 0b11110000)>>4)  //looks at the type bits
  {    
    case (1):  //Acknowledge
    Packet->length=AcknowledgeLength;
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      Serial.flush();
      RecieveAck(Packet);
      MessageInProgress=0; 
    }
    else
    {
      MessageInProgress=1;
    }
    break;
    
    
    case(2):  //EStop
    Packet->length=EStopLength;
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      Serial.flush();
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
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      Serial.flush();
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
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      Serial.flush();
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
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      Serial.flush();
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
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();      
      }
      Serial.flush();
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
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      Serial.flush();
      RecieveToolCMD(Packet);
      MessageInProgress=0;
    }
    else
    {
      MessageInProgress=1;
    }
    break;
    
    
    case (8):  //Ping
    Packet->length=PingLength;
    if(Serial.available()>=Packet->length-1)
    {
      for(int x=1; x<Packet->length; x++)
      {
        Packet->array[x]=Serial.read();          
      }
      RecievePing(Packet);
      MessageInProgress=0;
      Serial.flush();
    }
    else
    {
      MessageInProgress=1;
    }
    break;
    
  }
  digitalWrite(13,LOW);
  
  return(0);
}


//*************************************************************
//*************************************************************
//*************************************************************


int AcknowledgeMessage(boolean Error)
{
  Serial.flush();    //Needed to get rid of junk bits that shouldn't be there.
  if( Error==1)
  {
    Serial.flush();
    for (int x=0; x<AcknowledgeLength; x++)
    {
      Serial.write(PreviousPacket.array[x]);
    }
  }
  else
  {
    char FW=Firmware<<1;
    char type = 0x10 | (2*Error);
    AckPacket.length=AcknowledgeLength;
    AckPacket.array[0]=(type | HorParityGen(type));
    AckPacket.array[1]=FW | HorParityGen(FW);    
    AckPacket.array[2]=VertParityGen(&AckPacket);
    PreviousPacket = AckPacket;
    for (int x=0; x<AcknowledgeLength; x++)
    {
      Serial.write(AckPacket.array[x]);
    }
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
    return(-1);
  }
}
//**************************************************************************
//**                    End Parity Checks                                 **
//**************************************************************************


