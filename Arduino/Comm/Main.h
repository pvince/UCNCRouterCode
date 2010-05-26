#ifndef _Main_h
#define _Main_h

//USE TO DEBUG

//USE TO DEBUG

int Firmware = 3;
int FlagStart = 0;
int FlagEStop = 0;
int ReplyWait = 0;
int NoMoreMessages = 0; //Signals when the computer has no more messages to send
int FlagMotorRunning = 0; //1=Motors busy  0=Motors ready
int RequestInProgress = 0; //Keeps track of how many requested messages have yet to come.
int MessageInProgress=0;
unsigned char RequestNumber = 5; //has to be between 1-127
unsigned int  PowerPort = 20;

//Provides pulse signal
unsigned int  XPort = 22;  
unsigned int  YPort = 24;
unsigned int  ZPort = 26;
unsigned int  XInPulse = 52;
unsigned int  YInPulse = 50;
unsigned int  ZInPulse = 48;

//Port that the direction signal will be provided
unsigned int  XDirectionPort = 23;  
unsigned int  YDirectionPort = 25;
unsigned int  ZDirectionPort = 27;
unsigned int  XInDirection = 53;
unsigned int  YInDirection = 55;
unsigned int  ZInDirection = 57;

unsigned long Frequency = 16000000;


//Bad Programing! work the following variables into the code when I have free time

 
#endif
