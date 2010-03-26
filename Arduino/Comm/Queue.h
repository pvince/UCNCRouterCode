#ifndef Queue_h
#define Queue_h
#include "WProgram.h"

struct Linklist
{
  byte MessageLength;
  byte Message[11];
  Linklist* NextLink;
};

int QueueLength = 0;
Linklist* StartPointer = 0;
Linklist* WriteLocation = 0;



#endif
