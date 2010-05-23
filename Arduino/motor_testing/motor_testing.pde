
//ISR's
ISR(TIMER1_OVF_vect)
{
  XAxisISR();
}

   int count = 0;
   int Direction = 1;
void setup()
{
   pinMode(22,OUTPUT);
   pinMode(23,OUTPUT);
   
   digitalWrite(23,LOW);
//   total = total - time;
//   TCNT1L = total & 0b11111111;
//   TCNT1H = total>8 & 0b11111111;
  // TCNT1H = 245;  //62869 unity value causes 6000 pps and 0 clock scaling
   //TCNT1L = 149;
  
  
  //set timers
  //These registers should be 0 before starting the counters
  //Timers 1, 3 and 4 are used because they are 16 bit and timers 0 and 2 are only 8 bit.
  TCCR1A=0;

  
  //Clear the counter high and low bytes
//  TCNT1H=0;
//  TCNT1L=0;
  
  //Clear the overflow flags
  TIFR1 &= ~_BV(TOV1);

  //enable overflow interupts
  TIMSK1 |= _BV(TOIE1);
  
  //start the timers
  TCCR1B = 1;

  //Turn on interupts
}

void loop()
{
  
  digitalWrite(13,LOW);
  if (count >= 201 && Direction==0)
  {
    digitalWrite(23,LOW);
    Direction=1;
    count=0;
  }
  else if (count >= 201 && Direction==1)
  {
    digitalWrite(23,HIGH);
    Direction=0;
    count=0;
  }
}
// Scale=1 200-20   :1
// Scale=2 240-0    :8
// Scale=3 250-0    :64
// Scale=4 253-0    :256
// Scale=5 255-0    :1024

void XAxisISR()
{
    count++;
    digitalWrite(13,HIGH);
    digitalWrite(22,HIGH);
    digitalWrite(22,LOW);
    TCNT1L = 0;
    TCNT1H = 253;
    //clear overflow flag
    TIFR1 &= ~_BV(TOV1);
    //Turn on overflow interupt
    //TIMSK1 |= _BV(TOIE1);
    TCCR1B = 4;
    
    return;
}
