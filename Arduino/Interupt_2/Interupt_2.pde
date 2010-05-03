#include <avr/io.h>
#include <avr/interrupt.h>

//#define RESOLUTION 65536    // Timer1 is 16 bit
unsigned int test;
//unsigned char clockSelectBits;

ISR(TIMER1_OVF_vect)
  {
    Light();
  }
  
void setup()
{
  TCCR1A=0;
  TCCR1B=1;
  TIMSK1=33;
  test=0;
  SetTimers();
  //digitalWrite(47,LOW);
  //  digitalWrite(52,HIGH);
}

void loop()
{
 // delay(2000);
  //SetTimers();
 // delay(2000);
digitalWrite(49,HIGH);
  if (TCCR1B == 1)
  {
      //digitalWrite(13,HIGH);
  }
  if ( test == 1)
  {
    //digitalWrite(13,LOW);
  }
  
}



void SetTimers()
{
  //Tells the main function that the router is currently moving, so don't execute another queue command.
  //ExecutionStep = 0;
  
  //Stop interupts while setting them up
  //cli();
  TCCR1B &= ~(_BV(CS10) | _BV(CS11) | _BV(CS12));
  //These registers should be 0 before starting the counters
  //Timers 1, 3 and 4 are used because they are 16 bit and timers 0 and 2 are only 8 bit.
  TCCR1A=0;

  
  //Clear the counter high and low bytes
  TCNT1H=0;
  TCNT1L=0;

  
  //Clear the overflow flags
  TIFR1 &= ~_BV(TOV1);
  
  //enable overflow interupts
  TIMSK1 |= _BV(TOIE1);
  
  //start the timers
  TCCR1B = 1;
    
  //Turn on interupts
  sei();
}



void Light()
{
  cli();
 // TCCR1B &= ~(_BV(CS10) | _BV(CS11) | _BV(CS12));
 TCCR1B = 0;
 //Clear the overflow flags
 TIFR1 &= ~_BV(TOV1);
  if(test==1)
  {
    digitalWrite(13,HIGH);
    test=0;
    //SetTimers();
  }
  else if(test==0)
  {
    digitalWrite(13,LOW);
    test=1;
    //SetTimers();
  }
 TCNT1H=255;
  TCNT1L=0;
//  //start the timers
  TCCR1B = 5;    //0.004096 seconds/interupt no clock scaling  Unity = 2667 = 6000pps
  sei();
  //digitalWrite(13,LOW);
  reti();
}
