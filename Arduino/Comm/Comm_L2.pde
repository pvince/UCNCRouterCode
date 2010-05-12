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

int RecieveAck(PacketContainer* Packet)
{
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
    if(ExecutionStep < 3)
    {
      TIMSK1 |= _BV(TOIE1);
      TIMSK3 |= _BV(TOIE3);
      TIMSK4 |= _BV(TOIE4);
    }
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

void EStop()
{
 //Priority of actions: (EStop)->(Send next motor action)->(Read incomming Message)->(Request more messges if needed)
  
    //shuts off the interupts that would move the motors
    TIMSK1 &= ~_BV(TOIE1);
    TIMSK3 &= ~_BV(TOIE3);
    TIMSK4 &= ~_BV(TOIE4);
}
