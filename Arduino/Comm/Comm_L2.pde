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
  if(ParityChecker(Packet, PingLength)==0)
  {
    
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

int RecieveEStop(PacketContainer* Packet)
{
  if(ParityChecker(Packet, EStopLength)==0)
  {
    FlagEStop=1;
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

int RecieveStartQueue(PacketContainer* Packet)
{
  if(ParityChecker(Packet, StartQueueLength)==0)
  {
    FlagStart=1;
    FlagEStop=0;
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

int RecieveRequestCommands(PacketContainer* Packet)
{
  //*******************TESTING********************
  Packet->array[1]=(18);
  Packet->array[2]=(33);            //used to test VirtParityCheck()
  //***************END TESTING********************
  if(ParityChecker(Packet, RequestCommandsLength)==0)
  {
    
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

int RecieveSetSpeed(PacketContainer* Packet)
{
  if(ParityChecker(Packet, SetSpeedLength)==0)
  {
    
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

int RecieveMove(PacketContainer* Packet)
{
  if(ParityChecker(Packet, MoveLength)==0)
  {
    
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

int RecieveToolCMD(PacketContainer* Packet)
{
  if(ParityChecker(Packet, ToolCMDLength)==0)
  {
    
  }
  else
  {
    AcknowledgeMessage(1);
  }
}

//**************************************************************************
//**                     End Message Functions                            **
//**************************************************************************


