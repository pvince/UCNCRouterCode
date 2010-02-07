;UCNC Router Stepper Control;;;;;;;;;;;;;;;;;
;INPUTS:									;
;PIN 6  => RB0 => Step Pulse				;
;PIN 7  => RB1 => Forward/Reverse signal	;
;OUTPUTS:									;
;PIN 11 => RB5 => A Coil					;
;PIN 10 => RB4 => A* Coil					;
;PIN 9  => RB3 => B Coil					;
;PIN 8  => RB2 => B* Coil					;
;PIN 13 => RB7 => Enable A Coil				;
;PIN 12 => RB6 => Enable B Coil				;
;PIN 2 	=> RA3 => Busy						;
;											;
;Program waits for the rising edge of a 	;
; signal on RB0 to trigger an interrupt		;
; and initiate a step.						;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

;Constants
STATUS	equ 03h
RP0		equ 05h
PORTA	equ 05h
PORTB	equ 06h
INTCON	equ 0Bh
STATE	equ 0Ch
OPT		equ 81h
TRISA	equ 85h
TRISB	equ 86h

ORG 0000h
GOTO STARTUP	;Skip interrupt
ORG 0004h		;Interrupt memory location
CALL STEP		;Call STEP subroutine
BCF INTCON,1	;Clear Interrupt
RETFIE			;Return from interrupt

;Startup	4.8 u seconds
STARTUP	BCF STATUS,RP0	;Bank 0
		CLRF PORTA		;Init PORTA
		CLRF PORTB		;Init PORTB
		BSF STATUS,RP0	;Bank 1
		BSF OPT,6		;Enable Interrupt on rising edge
		MOVLW 03h		;Set RB0-1 as inputs, RB2-7 as outputs
		MOVWF TRISB		;Setup RC0-5 as outputs
		BCF STATUS,RP0	;Bank 0
		BSF	INTCON,7	;Enable Global Interrupts
		BSF	INTCON,4	;Enable RB0 Interrupt
		MOVLW 0xC0		;Set Enable outputs (RB6-7)
		MOVWF PORTB		;Output Enable bits for H-Bridge
		
		CALL STEP1		;Start the system at STEP1

;Worst case step takes 5 u seconds
;Listen for step instruction
LISTEN
		GOTO LISTEN		;Continue listening until interrupt

STEP
		BTFSC PORTB,1	;If PORTB, bit 1 is 0 skip next line
		CALL FORWARD	;Step Forward
		BTFSS PORTB,1	;If PORTB, bit 1 is 1 skip next line
		CALL REVERSE	;Step in Reverse
		RETURN			;GOTO LISTEN

FORWARD
		BTFSC STATE,1	;If W, bit 0 is 0 skip next line
		GOTO STEP2		;Next step(2)
		BTFSC STATE,2	;If STATE, bit 1 is 0 skip next line
		GOTO STEP3		;Next step(3)
		BTFSC STATE,3	;If STATE, bit 2 is 0 skip next line
		GOTO STEP4		;Next step(4)
		BTFSC STATE,4	;If STATE, bit 3 is 0 skip next line
		GOTO STEP1		;Next step(1)
		RETURN			;GOTO LISTEN

REVERSE
		BTFSC STATE,4		;If STATE, bit 3 is 0 skip next line
		GOTO STEP3		;Next step(3)
		BTFSC STATE,3		;If STATE, bit 2 is 0 skip next line
		GOTO STEP2		;Next step(2)
		BTFSC STATE,2		;If STATE, bit 1 is 0 skip next line
		GOTO STEP1		;Next step(1)
		BTFSC STATE,1		;If STATE, bit 0 is 0 skip next line
		GOTO STEP4		;Next step(4)
		RETURN			;GOTO LISTEN
		
;Power to Step 1
STEP1	MOVLW 0xE8		;Setup Step1 output(1110 1000)
		MOVWF PORTB		;Output Step1 to PORTB
		BCF STATE,4		;Reset STATE
		BSF STATE,1		;Set STATE to 0001 to track steps
		BCF STATE,2		;Reset STATE
		RETURN			;GOTO LISTEN
;Power to Step 2
STEP2	MOVLW 0xE4		;Setup Step1 output(1110 0100)
		MOVWF PORTB		;Output Step2 to PORTB
		BCF STATE,1		;Reset STATE
		BSF STATE,2		;Set W to 0010 to track steps
		BCF STATE,3		;Reset STATE
		RETURN			;GOTO LISTEN
;Power to Step 3
STEP3	MOVLW 0xD4		;Setup Step1 output(1101 0100)
		MOVWF PORTB		;Output Step2 to PORTB
		BCF STATE,2		;Reset STATE
		BSF STATE,3		;Set W to 0100 to track steps
		BCF STATE,4		;Reset STATE
		RETURN			;GOTO LISTEN
;Power to Step 4
STEP4	MOVLW 0xD8		;Setup Step1 output(1101 1000)
		MOVWF PORTB		;Output Step2 to PORTB
		BCF STATE,3		;Reset STATE
		BSF STATE,4		;Set W to 1000 to track steps
		BCF STATE,1		;Reset STATE
		RETURN			;GOTO LISTEN
END