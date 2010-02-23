#ifndef _Comm_h
#define _Comm_h

int MessageRate = 9600;

struct PacketContainer{
  
  
  
};
struct MessageAck{
  int Type : 4;
  int Error : 1;// = (Message & 15);
  int  Firmware : 8;
  int Blank : 17;
  int Checksum : 2;
};

struct MessageEStop{
  int Type :4;
  int Blank : 26;
  int Checksum : 2;  
};

struct MessageComReq{
  int Type : 4;
  int Blank : 26;
  int Checksum : 2;
};

struct MessageStart{
  int Type : 4;
  int Blank : 26;
  int Checksum : 2;
};

struct MessageSetSpeed{
  int Type : 4;
  int XSpeed : 8;
  int YSpeed : 8;
  int ZSpeed : 8;
  int Blank : 2;
  int Checksum : 2;
};

struct MessageToolCMD{
  int Type : 4;
  int Power : 1;
  int Blank : 25;
  int Checksum : 2;
};

struct MessageMove{
  int Type : 4;
  //I have no clue how we are going to do this so I'm skipping it for now. :P
  int Checksum : 2;
};
#endif

