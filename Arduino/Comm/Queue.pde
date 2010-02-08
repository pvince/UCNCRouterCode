
#include "Main.h"
#include "Comm.h"
#include "Queue.h"




int QueueAdd(long* Message)
{
  if(QueueLength < 500)
  {
    Queue[WriteLocation] = *Message;
    WriteLocation++;
    QueueLength++;
    if (WriteLocation == 500)  WriteLocation = 0;
  }
  else return(-1);
}

int QueueRead()
{
  if(QueueLength > 0)
  {
    //send to interpriter
    ReadLocation++;
    QueueLength--;
    if (ReadLocation == 500) ReadLocation = 0;
  }
}
