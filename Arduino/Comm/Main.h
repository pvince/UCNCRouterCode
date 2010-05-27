#ifndef _Main_h
#define _Main_h

//USE TO DEBUG

//USE TO DEBUG

int Firmware = 3;
int FlagStart = 0;
int FlagEStop = 0;
int ReplyWait = 0;
int FlagMotorRunning = 0; //1=Motors busy  0=Motors ready
int RequestInProgress = 0; //Keeps track of how many requested messages have yet to come.
int MessageInProgress=0;
int NoMoreMessages = 0;
unsigned char RequestNumber = 5; //has to be between 1-127
unsigned int  PowerPort = 20;

//Provides pulse signal
unsigned int  XPulsePort = 22;  
unsigned int  YPulsePort = 24;
unsigned int  ZPulsePort = 26;
unsigned int  EStopInPort = 21;
unsigned int  XInPulsePort = 20;
unsigned int  YInPulsePort = 19;
unsigned int  ZInPulsePort = 18;


//Port that the direction signal will be provided
unsigned int  XDirectionPort = 23;  
unsigned int  YDirectionPort = 25;
unsigned int  ZDirectionPort = 27;
unsigned int  XInDirection = 53;
unsigned int  YInDirection = 51;
unsigned int  ZInDirection = 49;

unsigned long Frequency = 16000000;


//Bad Programing! work the following variables into the code when I have free time

 
#endif
