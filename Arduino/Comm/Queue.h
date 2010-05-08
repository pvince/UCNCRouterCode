#ifndef Queue_h
#define Queue_h
#include "WProgram.h"

struct Linklist
{
  byte MessageLength;
  byte Message[11];
};
int MaxQueueLength=500;
Linklist Queue[500];
int QueueLength = 0;
int ReadLocation= 0;
int WriteLocation = 0;

void QueueAdd();
void QueueRead();
void Calculations(int, int, int);
void SetSpeed();
void Move();
void ToolCMD();


#endif
