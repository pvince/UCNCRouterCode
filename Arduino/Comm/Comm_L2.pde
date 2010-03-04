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

int RecieveEStop(PacketContainer* Packet)
{
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
    QueueAdd(Packet);
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

int RecieveMove(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    QueueAdd(Packet);
    AcknowledgeMessage(0);
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
    QueueAdd(Packet);
    AcknowledgeMessage(0);
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


