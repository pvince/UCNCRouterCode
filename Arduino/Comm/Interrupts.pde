
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
ISR(INT0_vect)
{
  ManualEStop();
}
ISR(INT1_vect)
{
  XAxisManual();
}
ISR(INT2_vect)
{
  YAxisManual();
}
ISR(INT3_vect)
{
  ZAxisManual();
}

void SetTimers(MoveDetails& MD)
{
  //Tells the main function that the router is currently moving, so don't execute another queue command.
  ExecutionStep = 0;
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
//  Serial.write(MD.XScalar);
//  Serial.write(YTime>>8 & 0b11111111);
//  Serial.write(YTime & 0b11111111);
  //Turn on interupts
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
}

void setYTimer() {
  //Set the values in the timers to get the correct slopes
  TCNT3H=YTime>>8 & 0b11111111;
  TCNT3L=YTime & 0b11111111;
  //Clear the overflow flags
  TIFR3 &= ~_BV(TOV3);
  //enable overflow interupts
  TIMSK3 |= _BV(TOIE3);
}

void setZTimer() {
  //Set the values in the timers to get the correct slopes
  TCNT4H=ZTime>>8 & 0b11111111;
  TCNT4L=ZTime & 0b11111111;
  //Clear the overflow flags
  TIFR4 &= ~_BV(TOV4);
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
  digitalWrite(53,HIGH);
  if(XPulseCount>0)
  {
    XPulseCount--;
    digitalWrite(XDirectionPort,XDirection);
    digitalWrite(XPulsePort,HIGH);  //done to make sure the signal is not to fast for the PICS
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    digitalWrite(XPulsePort,LOW);
  }
  else
  {
    TCCR1B = 0;
    TIMSK1 &= ~_BV(TOIE1);
    ExecutionStep++;
    digitalWrite(52,LOW);
  }
  return;
}

void YAxisISR()
{
  if(YPulseCount>0)
  {
    YPulseCount--;
    digitalWrite(YDirectionPort,YDirection);
    digitalWrite(YPulsePort,HIGH);  //done to make sure the signal is not to fast for the PICS
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    digitalWrite(YPulsePort,LOW);
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
    digitalWrite(ZPulsePort,HIGH);  //done to make sure the signal is not to fast for the PICS
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    asm("nop");
    digitalWrite(ZPulsePort,LOW);
  }
  else
  {
    TCCR4B = 0;
    TIMSK4 &= ~_BV(TOIE4);
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
  digitalWrite(13,HIGH);
  digitalWrite(52,HIGH);
  EIFR |= 0b00000001;
  return;
}

void XAxisManual()
{
  digitalWrite(XDirectionPort,XDirection);
  digitalWrite(XPulsePort,HIGH);
  digitalWrite(XPulsePort,LOW);
  EIFR |= 0b00000010;
  return;
}

void YAxisManual()
{
    digitalWrite(YDirectionPort,YDirection);
    digitalWrite(YPulsePort,HIGH);
    digitalWrite(YPulsePort,LOW);
    EIFR |= 0b00000100;
    return;
}

void ZAxisManual()
{
    digitalWrite(ZDirection,ZDirection);
    digitalWrite(ZPulsePort,HIGH);
    digitalWrite(ZPulsePort,LOW);
    EIFR |= 0b00001000;
    return;
}
