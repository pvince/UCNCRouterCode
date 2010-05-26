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
  int XScalar;
  int YScalar;
  int ZScalar;
  unsigned int XSpeed;
  unsigned int YSpeed;
  unsigned int ZSpeed;

};
int MaxQueueLength=500;
Linklist Queue[500];
int QueueLength = 0;
int ReadLocation= 0;
int WriteLocation = 0;

//Keeps track of the current position and destination
unsigned int  XPosition = 0;  
unsigned int  YPosition = 0;
unsigned int  ZPosition = 0;
unsigned int  XDestination = 0;  
unsigned int  YDestination = 0;
unsigned int  ZDestination = 0;
unsigned int  XDirection = 0;  
unsigned int  YDirection = 0;
unsigned int  ZDirection = 0;
//Number of times for the motor to move
unsigned int  XPulseCount = 0;  
unsigned int  YPulseCount = 0;
unsigned int  ZPulseCount = 0;
//counts up to three once all axises are done.
unsigned int  ExecutionStep = 0; 

void QueueAdd();
void QueueRead();
void calcRatio(unsigned int&, unsigned int&, unsigned int&);
void calcPulseRate(MoveDetails*);
void calcScalar(unsigned int&, int&);
void calcTimes(MoveDetails*);
void Calculations(int, int, int);
int SetSpeed(Linklist*, MoveDetails&);
int Move(Linklist*, MoveDetails&);
int ToolCMD();

#endif
