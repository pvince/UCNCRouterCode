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
Linklist* StartPointer;
Linklist* WriteLocation;

void QueueAdd();
void QueueRead();
void Calculations(int, int, int);
void SetSpeed();
void Move();
void ToolCMD();


#endif
