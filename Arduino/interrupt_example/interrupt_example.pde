#include "TimerOne.h"
int ledPin =  13;

int test;

void setup()
{
  Timer1.initialize(20000);
  //  Timer1.pwm(9,1);
  Timer1.attachInterrupt(Test);
  test = 0;
}




void loop()
{
  //digitalWrite(13,HIGH); 
  //delay(1000);
  if (test == 0)
  {
    digitalWrite(13,LOW);
  }
  else
  {
    digitalWrite(13,HIGH);
  }

  //Priority of actions: (EStop)->(Send next motor action)->(Read incomming Message)->(Request more messges if needed)
}

void Test()
{
  if (test==1) {
    test=0;
  }
  else
  {
    test=1;
  }

  reti();
}

