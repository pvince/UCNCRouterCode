#ifndef Queue_h
#define Queue_h
#include "WProgram.h"

struct Linklist
{
  byte MessageLength;
  byte Message[11];
};

struct MoveDetails
{
  unsigned int XDiff;
  unsigned int YDiff;
  unsigned int ZDiff;
  float XRatio;
  float YRatio;
  float ZRatio;
  unsigned int XPulseRate;
  unsigned int YPulseRate;
  unsigned int ZPulseRate;

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
