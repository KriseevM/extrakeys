# Programmable keyboard with 16 keys

This project is a simple implementation of a 16 keys programmable keyboard with Arduino (preferably Pro Micro or others AtMega32U4 based ones but may be possible to make with others through various hacks like custom bootloader. 
PCB has a slot for Arduino Nano and a marking how to place Micro in there)

Arduino IO pins 2 to 9 are used, 2 to 5 connected to columns and are in INPUT_PULLUP mode utilizing internal pullup resistors (therefore a pin have LOW value when key is pressed!), 6 to 9 are rows and are in OUTPUT mode, set LOW on a row pin before reading it's buttons' values.

Each button can have up to 6 keys pressed simultaneously but as far as I know the keyboard implementation (either in OS or in libraries for controller) are limited to 6 simultaneous keys in total so pressing many keys at once may not work well.

Controller also has a Serial port open at 19200 baudrate that can be used for programming or getting various debug data. You have to send a single byte with specific value to serial port for the controller to perform an action. 

Following actions are available:

* `0xAA` - switch to programming mode. After that a byte with numerical value of a button you need to program is expected (e.g. `0x00` for first button, `0x01` for second, etc.), then `MACRO_COUNT` (currently, 6, but can be edited in sketch if needed)
of bytes with needed keys. All the unused bytes (if you need to program 3 out of 6 keys on button) must be zero
* `0xAB` - report state of keypresses. It consists of 256 pairs of bytes separated with whitespace. Each pair corresponds to specific character code from `0x00` to `0xFF` and shows if any button that has this key in it's macro is currently pressed. This is mostly intended for debug purposes
* `0xAC` - report board `ID` constant. Can be used to differentiate different versions or implementations of this project
* `0xAD` - report board's parameters: button rows count, button columns count and maximum keys per each button (`MACRO_COUNT`), whitespace separated
* `0xAE` - report current programmed macros for all buttons. Each line is for certain button (first is for first button, etc.), `MACRO_COUNT` (currently, 6) of bytes on each line. Line separator seems to be `\r\n` so you have to skip 2 bytes to go to next line
* `0xF0` - reset all programmed macros

PC software for programming uses the Avalonia framework for dotnet. 

For setup you can clone this source code and run command `dotnet run` if you have dotnet runtime 8 installed.

After launching you will be prompted to choose your board. Software uses `0xAD` command to determine if the serial port can be used. IF YOU HAVE SENSITIVE SERIAL PORTS THAT DO NOT WANT TO RECEIVE ANY BYTES LIKE THIS PLEASE DISCONNECT THEM BEFORE USING SOFTWARE.

After choosing and pressing "Start" program will show you the window with all the previously programmed hotkeys of selected board. 

If you want to add a new key to combination you have to press the `+` button on the same line as the number of button that you want to program, if you want to remove a key you have to just click on whatever key you want to remove.

You can remove all keys from certain button by pressing the `x` button on the corresponding line. And if you want to clear all keys you have to press `Clear` button. 

After setting up all keys you need you have to press `Save macros` to save your changes on the board. If you want to cancel all your changes instead of saving them you can press `Restore` button. 
This will load whatever was last saved to the board. If you haven't saved your changes this will delete them and restore the board to it's last saved state.
