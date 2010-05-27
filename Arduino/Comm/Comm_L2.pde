#include "Main.h"
#include "Queue.h"
#include "Comm.h"

//**************************************************************************
//**************************************************************************
//**                     Message Functions:                               **
//**      Controls what happons when this type of packet is recieved      **
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

//**************************************************************************

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

//**************************************************************************

int RecieveEStop(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    FlagStart=0;
    FlagEStop=1;
    
     //Stop the timers
    TCCR1B = 0;
    TCCR3B = 0;
    TCCR4B = 0;
  
    //shuts off the interupts that would move the motors
    TIMSK1 &= ~_BV(TOIE1);
    TIMSK3 &= ~_BV(TOIE3);
    TIMSK4 &= ~_BV(TOIE4);
    
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

//**************************************************************************

int RecieveStartQueue(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    //if the motors were stoped before they got to their location 
    //they need to be started again
    if((Packet->array[0] & 0b00000010)>>1==0)
    {
      NoMoreMessages=0;
      if(XPulseCount > 0)    
      {
        TIMSK1 |= _BV(TOIE1);
        TCCR1B = 1;
      }
      if(YPulseCount > 0)
      {
        TIMSK3 |= _BV(TOIE3);
        TCCR3B = 1;
      }
      if(ZPulseCount > 0)
      {
        TIMSK4 |= _BV(TOIE4);
        TCCR4B = 1;
      }
      FlagStart=1;
      FlagEStop=0;
    }
    else
    {
      //FlagStart = 0;
      RequestInProgress=0;
      NoMoreMessages=1;
      digitalWrite(52,LOW);
    }
    AcknowledgeMessage(0);
  }
  else
  {
    AcknowledgeMessage(1);
  }
  return(0);
}

//**************************************************************************

int RecieveRequestCommands(PacketContainer* Packet)
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

//**************************************************************************

int RecieveSetSpeed(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    if(QueueAdd(Packet)==0)
    {
      RequestInProgress--; //This was one of the messages that was requested
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

int RecieveMove(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    if(QueueAdd(Packet)==0)
    {
      RequestInProgress--; //This was one of the messages that was requested
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

int RecieveToolCMD(PacketContainer* Packet)
{
  if(ParityChecker(Packet)==0)
  {
    if(QueueAdd(Packet)==0)
    {
      RequestInProgress--; //This was one of the messages that was requested
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
