#include <stdint.h>
#include <Keyboard.h>
#include <EEPROM.h>

#define TXLED 17
#define ID 1008

#define MACRO_COUNT 6
#define KEY_COUNT 256

// these all must be correct and define correct sizes (!!!!)
#define FIRST_COL_PIN 2
#define LAST_COL_PIN 5
#define FIRST_ROW_PIN 6
#define LAST_ROW_PIN 9
#define ROWS_COUNT 4
#define COLS_COUNT 4
#define TOTAL_BUTTONS_COUNT 16

// each bit is a 1 if the corresponding key is pressed
uint16_t keyboardState = 0;

// Each element contains a macro for each key. Up to {MACRO_COUNT} keys in each macro
char macros[TOTAL_BUTTONS_COUNT][MACRO_COUNT];

// assuming sizeof(char) == 1. If for some reason this is not true, 
// the program will either not compile or break.
// May also not break if you're lucky with your encoding :D
// This is a flag for each possible character if any key holds it.
// basically busyKeys[i] != 0  ==> we can't send press or release.
// A lot of valuable RAM will be used for this
// but it's a sacrifice I'm willing to make (c)
uint16_t busyKeys[KEY_COUNT];

byte calcMacrosChecksum() {
	byte checksum = 0;
	for(int i = 0; i < TOTAL_BUTTONS_COUNT; ++i) {
		for(int j = 0; j < MACRO_COUNT; ++j) {
			checksum += macros[i][j];
		}
	}
	return checksum;
}

void saveMacros() {
	for(int i = 0; i < TOTAL_BUTTONS_COUNT; ++i) {
		for(int j = 0; j < MACRO_COUNT; ++j) {
			EEPROM.update(1 + i * TOTAL_BUTTONS_COUNT + j, macros[i][j]);
		}
	}
	EEPROM.update(0, calcMacrosChecksum());
}

void resetMacros() {
	keyboardState = 0;
	memset(busyKeys, 0, sizeof(busyKeys));
	for(int i = 0; i < TOTAL_BUTTONS_COUNT; ++i) {
		memset(macros[i], 0, MACRO_COUNT);
	}
	saveMacros();
}

void setup() {
	Serial.begin(19200);
	byte checksum = EEPROM.read(0);
	for(int i = 0; i < TOTAL_BUTTONS_COUNT; ++i) {
		for(int j = 0; j < MACRO_COUNT; ++j) {
			macros[i][j] = EEPROM.read(1 + i * TOTAL_BUTTONS_COUNT + j);
		}
	}
	byte calcChecksum = calcMacrosChecksum();
	if(calcChecksum != checksum) {
		delay(2000);
		Serial.println("Checksum mismatch");
		Serial.print("Stored: ");
		Serial.print(checksum, BIN);
		Serial.println();
		Serial.print("Calculated: ");
		Serial.print(calcChecksum, BIN);
		Serial.println();
		resetMacros();
	}
	pinMode(TXLED, OUTPUT);
	digitalWrite(TXLED, LOW);
	// set pins 2 to 9 for keypad
	int i = FIRST_COL_PIN;
	for(; i <= LAST_COL_PIN; ++i) {
		pinMode (i, INPUT_PULLUP);
	}
	for(i = FIRST_ROW_PIN;i <= LAST_ROW_PIN; ++i) {
		pinMode(i, OUTPUT);
		digitalWrite(i, HIGH);
	}
	Keyboard.begin();
}

void updateKeyboard() {
	for(int i = FIRST_ROW_PIN, bitshift = 0; i <= LAST_ROW_PIN; ++i, bitshift += ROWS_COUNT) {
		digitalWrite(i, LOW);
		for(int j = FIRST_COL_PIN, colIndex = 0; j <= LAST_COL_PIN; ++j, ++colIndex) {
			int actualBitshift = bitshift + colIndex;
			uint16_t mask = 1 << actualBitshift;
			uint16_t value = keyboardState & mask;
			int newValue = digitalRead(j);
			if(!newValue && !value) {
				keyboardState |= mask;
				for(int k = 0; k < MACRO_COUNT; ++k) {
					char macro = macros[actualBitshift][k];
					if(!busyKeys[(unsigned char) macro]) {
						Keyboard.press(macro);
					}
					busyKeys[(unsigned char) macro] |= mask;
				}
			}
			else if(newValue && value) {
				keyboardState &= (~mask);
				for(int k = 0; k < MACRO_COUNT; ++k) {
					char macro = macros[actualBitshift][k];
					busyKeys[(unsigned char) macro] &= (~mask);
					if(!busyKeys[(unsigned char) macro]) {
						Keyboard.release(macro);
					}
				}
			}
		}
		digitalWrite(i, HIGH);
	}
}

void programmingMode() {
	digitalWrite(TXLED, HIGH);
	char newMacro[11];
	size_t macroSize = Serial.readBytes(newMacro, MACRO_COUNT + 1);
	if(macroSize == MACRO_COUNT + 1 && newMacro[0] >= 0 && newMacro[0] < 16) {
		Keyboard.releaseAll();
		memset(busyKeys, 0, sizeof(busyKeys));
		keyboardState = 0;
		memcpy(macros[newMacro[0]], newMacro + 1, MACRO_COUNT);
		saveMacros();
	}
	digitalWrite(TXLED, LOW);
}

void loop() {
	updateKeyboard();
	if(Serial.available()) {
		int code = Serial.read();
		switch (code) {
			case 0xAA:
				programmingMode();
				break;
			case 0xAB:
				for(int i = 0; i < KEY_COUNT; ++i) {
					Serial.print(busyKeys[i]);
					Serial.print(' ');
				}
				Serial.println();
				break;
			case 0xAC:
				Serial.println(ID);
				break;
			case 0xAD:
				Serial.print(ROWS_COUNT);
				Serial.print(' ');
				Serial.print(COLS_COUNT);
				Serial.print(' ');
				Serial.print(MACRO_COUNT);
				Serial.println();
				break;
			case 0xAE:
				for(int i = 0; i < TOTAL_BUTTONS_COUNT; ++i) {
					for(int j = 0; j < MACRO_COUNT; ++j) {
						Serial.print(macros[i][j]);
					}
					Serial.println();
				}
				break;
			case 0xF0:
				resetMacros();
				break;
		}
	}
	delay(1);
}

