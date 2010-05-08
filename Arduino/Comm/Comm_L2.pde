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
  digitalWrite(22,HIGH);
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
  digitalWrite(24,HIGH);
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
  if(ParityChecker(Packet)==0)
  {
    FlagStart=0;
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
  digitalWrite(26,HIGH);
  if(ParityChecker(Packet)==0)
  {
    FlagStart=1;
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
  digitalWrite(26,HIGH);
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


