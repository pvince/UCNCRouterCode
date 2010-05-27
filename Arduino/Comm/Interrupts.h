


#ifndef Interrupts_h
#define Interrupts_h

void SetTimers();
void XAxisISR();
void YAxisISR();
void ZAxisISR();
void ManualEStop();
void XAxisManual();
void YAxisManual();
void ZAxisManual();

unsigned int XTime;
unsigned int YTime;
unsigned int ZTime;

int UnityScale = 1000;
int LowScaleOne = 350;
int LowScaleTwo = 50;
int LowScaleThree = 10;
int LowScaleFour = 1;

#endif
