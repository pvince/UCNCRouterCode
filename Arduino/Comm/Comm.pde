


#include "Main.h"
#include "Queue.h"
#include "Comm.h"



int MessageFilter(char Message)
{
  //long* packet;
  PacketContainer Packet;
  Packet.array[0] =  Message;
  switch(Packet.array[0] & 15) //4026531840 number to use to get the first 4bits of the message.
  {
    case (0):  //Ping
      Serial.println("***** Case 0*****");
      Packet.length=PingLength;
      RecievePing(&Packet);
      break;
    case (1):  //Acknowledge
      Serial.println("***** Case 1*****");
      Packet.length=2;
      RecievePing(&Packet);
      break;
    case(2):  //EStop
      Serial.println("***** Case 2*****");
      Packet.length=EStopLength;
      RecieveEStop(&Packet);
      break;
    case(3):  //Request Commands
      Serial.println("***** Case 3*****");
      Packet.length=RequestCommandsLength;
      RecieveRequestCommands(&Packet);
      break;
    case(4):  //Start Queue
      Serial.println("***** Case 4*****");
      Packet.length=StartQueueLength;
      RecieveStartQueue(&Packet);
      break;
    case(5):  //SetSpeed
      Serial.println("***** Case 5*****");
      Packet.length=SetSpeedLength;
      RecieveSetSpeed(&Packet);
      break;
    case(6):  //Move
      Serial.println("***** Case 6*****");
      Packet.length=MoveLength;
      RecieveMove(&Packet);
      break;
    case(7):  //ToolCMD
      Serial.println("***** Case 7*****");
      Packet.length=ToolCMDLength;
      RecieveToolCMD(&Packet);
      break;
  }

}
int AcknowledgeMessage(boolean Error)
{
  if(Error==0)
  {
    Serial.println(17,BIN);
  }
  else
  {
    Serial.println(18,BIN);
  }
}

int RequestCMD(byte Number)
{
  PacketContainer Packet;
  Packet.array[0]=48;
  Packet.array[1]=HorParityGen(Number<<1);
  
}

byte HorParityGen(byte Message)
{
  boolean CheckBit=0;
  for(int x=1; x<8; x++)
  {
    CheckBit = CheckBit + bitRead(Message,x);
  }
  return CheckBit;
}

byte VertParityGen(PacketContainer* Packet)
{
  int CheckByte = 0;
  for(int x=0; x<8; x++)
  {
    int CheckBit = 0;
    for(int y=0; y<Packet->length-1; y++)
    {
      CheckBit = CheckBit + bitRead(lowByte(Packet->array[y]),x);
    }
    bitWrite(CheckByte,x,bitRead(CheckBit,0));
  }
  return(CheckByte);
}

int ParityChecker(PacketContainer* Packet, int Length)
{
   if(HorParityCheck(Packet->array[0]) !=0)
  {
    return(-1);
  }
  for(int x=1; x<Packet->length; x++)
  {
    Packet->array[x]=Serial.read();
    if(HorParityCheck(Packet->array[x]) !=0)
    {
      Serial.println("Horizontal Parity Bit Check Failed");
      return(-1);
    }
  }
    //*******************TESTING********************
    Packet->array[1]=(114);            //used to test message structure
    Packet->array[2]=(65);            //used to test VirtParityCheck()
    //***************END TESTING********************
  return(VertParityCheck(Packet));
}


//**************************************************************************
//**************************************************************************
//**                     Parity Checks                                    **
//**************************************************************************
//**************************************************************************
int HorParityCheck(int Message)
{
  if(HorParityGen(Message)==bitRead(Message,0))
  {
    return(0);
  }
  else
  {
    Serial.println(Message,BIN);
    Serial.println("Horizontal Parity Bit Check Failed");
    return(-1);
  }

}

int VertParityCheck(PacketContainer *Packet)  //Checks the parity packet with the rest of the message
{
  //*****************Not needed but shows that the virt sync function is working******************
  for(int z=0; z<=Packet->length-1; z++)
  {
    Serial.print("Message ");
    Serial.print(z);
    Serial.print("= ");
    Serial.println(Packet->array[z],BIN);
  }
  //*****************^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^*****************
  if(Packet->array[Packet->length-1]==VertParityGen(Packet))  
  {
    Serial.println("Vertical Parity Byte Check Correct");
    return(0);
  }
  else  
  {
    Serial.println("Vertical Parity Byte Check Failed");
    return(-1);
  }
}
//**************************************************************************
//**                    End Parity Checks                                 **
//**************************************************************************
