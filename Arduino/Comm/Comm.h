#ifndef _Comm_h
#define _Comm_h

int MessageRate = 9600;

int PingLength=2;
int EStopLength=2;
int RequestCommandsLength=3;
int StartQueueLength=2;
int SetSpeedLength=5;
int MoveLength=11;
int ToolCMDLength=2;

struct PacketContainer{
  char array[8];
  unsigned char length;
};


#endif

