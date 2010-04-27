#ifndef _Main_h
#define _Main_h



int Firmware = 3;
int FlagStart = 0;
int FlagEStop = 0;
int FlagMotorRunning = 0; //1=Motors busy  0=Motors ready
int MessageInProgress=0;
unsigned char RequestNumber = 5; //has to be between 1-127
unsigned int  PowerPort = 20;

//Provides pulse signal
unsigned int  XPort = 22;  
unsigned int  YPort = 24;
unsigned int  ZPort = 26;

//Port that the direction signal will be provided
unsigned int  XDirectionPort = 23;  
unsigned int  YDirectionPort = 25;
unsigned int  ZDirectionPort = 27;

//Slowest the motor can go
unsigned int  MotorSpeed = 20000; 



//Bad Programing! work the following variables into the code when I have free time

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

//higher number is faster
unsigned int  XSpeed = 1;  
unsigned int  YSpeed = 1;
unsigned int  ZSpeed = 1;
unsigned int  ExecutionStep = 0;  
#endif
