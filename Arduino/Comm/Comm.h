#ifndef _Comm_h
#define _Comm_h

int MessageRate = 9600;

struct MessageAck{
  boolean Error;// = (Message & 15);
  int  Firmware;
  int Checksum;
};

struct MessageEStop{
    int Checksum;  
};

struct MessageComReq{
    int Checksum;
};

struct MessageStart{
   int Checksum;
};

struct MessageSetSpeed{
  int XSpeed;
  int YSpeed;
  int ZSpeed;
  int Checksum;
};

struct MessageToolCMD{
  boolean Power;
  int Checksum;
};

struct MessageMove{
   int Checksum;
};


