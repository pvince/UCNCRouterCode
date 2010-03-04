
#include "Main.h"
#include "Comm.h"
#include "Queue.h"



//
int QueueAdd(PacketContainer* Message)  //Adds messages to the queue
{
  Linklist NewLink;
  if(QueueLength == 0)
  {
    StartPointer = &NewLink;
    WriteLocation = &NewLink;
  }
  else if(QueueLength > 0)
  {
    Linklist* temp = WriteLocation; 
    temp->NextLink = &NewLink;
    WriteLocation = &NewLink;
  }
  else return(-1);
  QueueLength++;
  NewLink.MessageLength = Message->length;
  NewLink.NextLink = NULL;
  for(int x = 0; x<Message->length-1; x++) //does not read in the parity
  {
    NewLink.Message[x]=Message->array[x];
  }
}

int QueueRead()  //Reads the oldest link off the queue and sends it to the required function.
{
  
}

int SetSpeed()  //Sends signal to disired ports
{
  
}

int Move()  //Sends signal to disired ports
{
  
}

int ToolCMD()  //Sends signal to disired ports
{
  
}
