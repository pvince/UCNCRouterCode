#ifndef _Main_h
#define _Main_h



int Firmware = 3;
int FlagStart = 0;
int FlagEStop = 0;
int FlagMotorRunning = 0; //1=Motors busy  0=Motors ready
int MessageInProgress=0;
unsigned char RequestNumber = 5; //has to be between 1-127

#endif
