





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


void SetTimers()
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
  
  //Clear the counter high and low bytes
  TCNT1H=0;
  TCNT1L=0;
  TCNT3H=0;
  TCNT3L=0;
  TCNT4H=0;
  TCNT4L=0;
  
  //Clear the overflow flags
  TIFR1 &= ~_BV(TOV1);
  TIFR1 &= ~_BV(TOV3);
  TIFR1 &= ~_BV(TOV4);
  
  //enable overflow interupts
  TIMSK1 |= _BV(TOIE1);
  TIMSK3 |= _BV(TOIE3);
  TIMSK4 |= _BV(TOIE4);
  
  //start the timers
  TCCR1B = 1;
  TCCR3B = 1;
  TCCR4B = 1;
  
  //Turn on interupts
  sei();
  
}

void XAxisISR()
{
  if(XPulseCount>0)
  {
    XPulseCount--;
    digitalWrite(XDirectionPort,XDirection);
    digitalWrite(XPort,HIGH);
    digitalWrite(XPort,HIGH);
    digitalWrite(XPort,HIGH);
    digitalWrite(XPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    //digitalWrite(XPort,LOW);
  }
  else
  {
    ExecutionStep++;
    detachInterrupt(0);
  }

}

void YAxisISR()
{
  if(YPulseCount>0)
  {
    YPulseCount--;
    digitalWrite(YDirectionPort,YDirection);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);
    digitalWrite(YPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    //digitalWrite(YPort,LOW);
  }
  else
  {
    ExecutionStep++;
    detachInterrupt(1);
  }

}

void ZAxisISR()
{
  if(ZPulseCount>0)
  {
    ZPulseCount--;
    digitalWrite(ZDirection,ZDirection);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);
    digitalWrite(ZPort,HIGH);  //done to make sure the signal is not to fast for the PICS
    //digitalWrite(ZPort,LOW);
  }
  else
  {
    ExecutionStep++;
    detachInterrupt(2);
  }

}

