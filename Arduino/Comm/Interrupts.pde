
#include "Interrupts.h"
#include "Queue.h"


//ESR's

//ISR's
ISR(TIMER1_OVF_vect)
{
  XAxisISR();
}
ISR(TIMER3_OVF_vect)
{
  YAxisISR();
}
ISR(TIMER4_OVF_vect)
{
  ZAxisISR();
}

void SetTimers(MoveDetails& MD)
{
  //Tells the main function that the router is currently moving, so don't execute another queue command.
  ExecutionStep = 0;
  Serial.write(MD.XScalar);
  Serial.write(MD.YScalar);
  Serial.write(MD.ZScalar);
  //Stop interupts while setting them up
  cli();
  //These registers should be 0 before starting the counters
  //Timers 1, 3 and 4 are used because they are 16 bit and timers 0 and 2 are only 8 bit.
  TCCR1A=0;
  TCCR3A=0;
  TCCR4A=0;
  
  setXTimer();
  setYTimer();
  setZTimer();
  
  
  //start the timers
  TCCR1B = MD.XScalar;
  TCCR3B = MD.YScalar;
  TCCR4B = MD.ZScalar;

  TCCR3B = 1;
  TCCR4B = 1;
  
  //Turn on interupts
  digitalWrite(52,HIGH);
  sei();
  return;
}

void setXTimer() {
  //Set the values in the timers to get the correct slopes
  TCNT1H=XTime>>8 & 0b11111111;
  TCNT1L=XTime & 0b11111111;
  //Clear the overflow flags
  TIFR1 &= ~_BV(TOV1);
  //enable overflow interupts
  TIMSK1 |= _BV(TOIE1);
  digitalWrite(13,HIGH);
}

void setYTimer() {
  //Set the values in the timers to get the correct slopes
  TCNT3H=YTime>>8 & 0b11111111;
  TCNT3L=YTime & 0b11111111;
  //Clear the overflow flags
  TIFR1 &= ~_BV(TOV3);
  //enable overflow interupts
  TIMSK3 |= _BV(TOIE3);
  digitalWrite(13,LOW);
}

void setZTimer() {
  //Set the values in the timers to get the correct slopes
  TCNT4H=ZTime>>8 & 0b11111111;
  TCNT4L=ZTime & 0b11111111;
  //Clear the overflow flags
  TIFR1 &= ~_BV(TOV4);
  //enable overflow interupts
  TIMSK4 |= _BV(TOIE4);
}

//*****************************************************************
//*****************************************************************
//**    Interupts for messaging with the computer                **
//*****************************************************************
//*****************************************************************
void XAxisISR()
{
  if(XPulseCount>0)
  {
    XPulseCount--;
    digitalWrite(XDirectionPort,XDirection);
    digitalWrite(XPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    digitalWrite(XPort,LOW);
    digitalWrite(13,HIGH);
  }
  else
  {
    TCCR1B = 0;
    TIMSK1 &= ~_BV(TOIE1);
    ExecutionStep++;
  }
  return;
}

void YAxisISR()
{
  if(YPulseCount>0)
  {
    YPulseCount--;
    digitalWrite(YDirectionPort,YDirection);
    digitalWrite(YPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    digitalWrite(YPort,LOW);
    digitalWrite(13,HIGH);
  }
  else
  {
    TCCR3B = 0;
    TIMSK3 &= ~_BV(TOIE3);
    ExecutionStep++;
  }

}

void ZAxisISR()
{
  if(ZPulseCount>0)
  {
    ZPulseCount--;
    digitalWrite(ZDirection,ZDirection);
    digitalWrite(ZPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    digitalWrite(ZPort,LOW);
  }
  else
  {
    TCCR4B = 0;
    TIMSK4 &= ~_BV(TOIE4);
    digitalWrite(30,HIGH);
    ExecutionStep++;
  }

}


//*****************************************************************
//*****************************************************************
//**                 Interupts for manual control                **
//*****************************************************************
//*****************************************************************

void ManualEStop()  //used for manual control
{
  FlagStart=0;
  FlagEStop=1;
  
   //Stop the timers
  TCCR1B = 0;
  TCCR3B = 0;
  TCCR4B = 0;

  //shuts off the interupts that would move the motors
  TIMSK1 &= ~_BV(TOIE1);
  TIMSK3 &= ~_BV(TOIE3);
  TIMSK4 &= ~_BV(TOIE4);
    
  AcknowledgeMessage(0);
  return;
}

void XAxisManual()
{
  digitalWrite(XDirectionPort,XDirection);
  digitalWrite(XPort,HIGH);
  digitalWrite(XPort,HIGH);
  digitalWrite(XPort,HIGH);
  digitalWrite(XPort,HIGH);  //done to make sure the signal is not to fast for the PICS
  digitalWrite(XPort,LOW);
  return;
}

void YAxisManual()
{
    digitalWrite(YDirectionPort,YDirection);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    digitalWrite(YPort,LOW);
    return;
}

void ZAxisManual()
{
    digitalWrite(ZDirection,ZDirection);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    digitalWrite(ZPort,LOW);
    return;
}
