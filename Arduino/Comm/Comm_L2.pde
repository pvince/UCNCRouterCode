#include "Main.h"
#include "Queue.h"
#include "Comm.h"

//**************************************************************************
//**************************************************************************
//**                     Message Functions                                **
//**************************************************************************
//**************************************************************************
int RecievePing(PacketContainer* Packet)
{
  digitalWrite(28,HIGH);
  delay(10);
  digitalWrite(28,LOW);
  if(ParityChecker(Packet)==0)
  {
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveAck(PacketContainer* Packet)
{
  digitalWrite(28,HIGH);
  delay(10);
  digitalWrite(28,LOW);
  if(Packet->array[0] == 18)
  {
     for (int x=0; x<AcknowledgeLength; x++)
     {
        Serial.write(PreviousPacket.array[x]);
     }
  }
  return(0);
}
int RecieveEStop(PacketContainer* Packet)
{
  digitalWrite(28,HIGH);
  delay(10);
  digitalWrite(28,LOW);
  if(ParityChecker(Packet)==0)
  {
    FlagEStop=1;
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveStartQueue(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    digitalWrite(28,HIGH);
  delay(10);
  digitalWrite(28,LOW);
    FlagStart=1;
    FlagEStop=0;
    //****************************************
    //**  Start Queue Logic                 **
    //****************************************
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveRequestCommands(PacketContainer* Packet)
{
  digitalWrite(28,HIGH);
  delay(10);
  digitalWrite(28,LOW);
  if(ParityChecker(Packet)==0)
  {
    //****************************************
    //**    Start Request Commands Logic    **
    //****************************************
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveSetSpeed(PacketContainer* Packet)
{
  digitalWrite(47,HIGH);
  delay(10);
  digitalWrite(47,LOW);
  if(ParityChecker(Packet)==0)
  {
    if(QueueAdd(Packet)==0)
    {
      AcknowledgeMessage(0);
    }
    else
    {
      AcknowledgeMessage(1);
    }
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveMove(PacketContainer* Packet)
{
  digitalWrite(53,HIGH);
      delay(100);
      digitalWrite(53,LOW);
  if(ParityChecker(Packet)==0)
  {
    if(QueueAdd(Packet)==0)
    {
      AcknowledgeMessage(0);
    }
    else
    {
      AcknowledgeMessage(1);
    }
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveToolCMD(PacketContainer* Packet)
{
  Serial.write(Packet->array[0]);
  digitalWrite(28,HIGH);
  delay(10);
  digitalWrite(28,LOW);
  if(ParityChecker(Packet)==0)
  {
    if(QueueAdd(Packet)==0)
    {
      AcknowledgeMessage(0);
    }
    else
    {
      AcknowledgeMessage(1);
    }
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

//**************************************************************************
//**                     End Message Functions                            **
//**************************************************************************


