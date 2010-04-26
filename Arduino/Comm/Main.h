#ifndef _Main_h
#define _Main_h



int Firmware = 3;
int FlagStart = 0;
int FlagEStop = 0;
int FlagMotorRunning = 0; //1=Motors busy  0=Motors ready
int MessageInProgress=0;
unsigned char RequestNumber = 5; //has to be between 1-127
unsigned int  PowerPort = 20;
unsigned int  XPort = 22;
unsigned int  XDirection = 23;
unsigned int  YPort = 24;
unsigned int  YDirection = 25;
unsigned int  ZPort = 26;
unsigned int  ZDirection = 27;
unsigned int  XPosition = 0;  //Keeps track of the current position
unsigned int  YPosition = 0;
unsigned int  ZPosition = 0;
unsigned int  MotorSpeed = 20000;
#endif
