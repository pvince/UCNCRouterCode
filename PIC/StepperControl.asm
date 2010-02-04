;UCNC Router Stepper Control;;;;;;;;;;;;;;;;;
;INPUTS:									;
;PIN 18 => RA1 => Step Pulse				;
;PIN 17 => RA0 => Forward/Reverse signal	;
;OUTPUTS:									;
;PIN 12 => RB6 => A Coil					;
;PIN 11 => RB5 => A* Coil					;
;PIN 10 => RB4 => B Coil					;
;PIN 9  => RB3 => B* Coil					;
;PIN 8  => RB2 => Enable A Coil				;
;PIN 7 	=> RB1 => Enable B Coil				;
;PIN 13	=> RB7 => Busy						;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

;Constants
STATUS	equ 03h
RP0		equ 05h
PORTA	equ 05h
PORTB	equ 06h
ANSEL	equ 91h
TRISA	equ 85h
TRISB	equ 86h
STATE	equ 0Ch

;Startup	18 cycles = 3.6 u seconds
STARTUP	BCF STATUS,RP0	;Bank 0
		CLRF PORTA		;Init PORTA
		CLRF PORTB		;Init PORTB
		BSF STATUS,RP0	;Bank 1
		CLRF ANSEL		;digital I/O
		MOVLW 03h		;Set RA0-1 as inputs
		MOVWF TRISA		;Setup RA0-1 as inputs and 2-5 as outputs
		MOVLW 00h		;Set RC0-5 as outputs
		MOVWF TRISB		;Setup RC0-5 as outputs
		BCF STATUS,RP0	;Bank 0
		MOVLW 03h		;Set Enable outputs (RC0-1)
		MOVWF PORTB		;Output Enable bits for H-Bridge
		
		GOTO STEP1		;Start the system at STEP1

;Listen for step instruction
LISTEN
		BTFSC PORTA,1	;If PORTA, bit 1 is 0 skip next line
		GOTO STEP		;Call STEP subroutine
		GOTO LISTEN		;Continue listening

STEP
		BSF PORTA,3		;Set PORTA, bit 3 to signal busy to controller
		BTFSC PORTA,0	;If PORTA, bit 0 is 0 skip next line
		CALL FORWARD	;Step Forward
		CALL REVERSE	;Step in Reverse
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN

FORWARD
		BTFSC STATE,1		;If W, bit 0 is 0 skip next line
		GOTO STEP2		;Next step(2)
		BTFSC STATE,2		;If STATE, bit 1 is 0 skip next line
		GOTO STEP3		;Next step(3)
		BTFSC STATE,3		;If STATE, bit 2 is 0 skip next line
		GOTO STEP4		;Next step(4)
		BTFSC STATE,4		;If STATE, bit 3 is 0 skip next line
		GOTO STEP1		;Next step(1)
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN

REVERSE
		BTFSC STATE,4		;If STATE, bit 3 is 0 skip next line
		GOTO STEP3		;Next step(3)
		BTFSC STATE,3		;If STATE, bit 2 is 0 skip next line
		GOTO STEP2		;Next step(2)
		BTFSC STATE,2		;If STATE, bit 1 is 0 skip next line
		GOTO STEP1		;Next step(1)
		BTFSC STATE,1		;If STATE, bit 0 is 0 skip next line
		GOTO STEP4		;Next step(4)
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN
		
;Power to Step 1
STEP1	MOVLW 2Bh		;Setup Step1 output(101011)
		MOVWF PORTB		;Output Step1 to PORTB
		BCF STATE,4		;Reset STATE
		BSF STATE,1		;Set STATE to 0001 to track steps
		BCF STATE,2		;Reset STATE
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN
;Power to Step 2
STEP2	MOVLW 27h		;Setup Step1 output(100111)
		MOVWF PORTB		;Output Step2 to PORTB
		BCF STATE,1		;Reset STATE
		BSF STATE,2		;Set W to 0010 to track steps
		BCF STATE,3		;Reset STATE
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN
;Power to Step 3
STEP3	MOVLW 17h		;Setup Step1 output(010111)
		MOVWF PORTB		;Output Step2 to PORTB
		BCF STATE,2		;Reset STATE
		BSF STATE,3		;Set W to 0100 to track steps
		BCF STATE,4		;Reset STATE
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN
;Power to Step 4
STEP4	MOVLW 1Bh		;Setup Step1 output(011011)
		MOVWF PORTB		;Output Step2 to PORTB
		BCF STATE,3		;Reset STATE
		BSF STATE,4		;Set W to 1000 to track steps
		BCF STATE,1		;Reset STATE
		BCF PORTA,3		;Clear PORTA bit 3 to signal ready for next step
		GOTO LISTEN
END