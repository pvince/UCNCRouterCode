#ifndef _Queue_h
#define _Queue_h
#endif

#include "Main.h"
#include "Comm.h"

int ErrorCheck(long* Message)
{
  switch(*Message)
  {
    case(SpeedSet):
      QueueAdd(*Message);
    case(ToolCmd):
      QueueAdd(*Message);
    case(Move):
      QueueAdd(*Message):
    default
      Serial.print("Resend");
}

QueueAdd(long* Message)
{
}
