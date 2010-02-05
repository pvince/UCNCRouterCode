#ifndef _Comm_h
#define _Comm_h
#endif

#include "Main.h"
#include "Queue.h"

int MessageFilter(long* Message)
{
  switch(*Message)
  {
    case 'EStop':
      FlagEStop = 1;    
      return(0);
      break;
    case 'Ping':
      Serial.print(Firmware);
      break;
    default:
      ErrorCheck(Message);
      break;
   }
  
}
