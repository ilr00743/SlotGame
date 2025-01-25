# Slots Game
The prototype of slot machine

## Features
- Configurable reel spin mechanic
- Different implementations of RNG
- Win calculation and displaying results on UI
- Convenient configuration of pay lines and symbols

## Planning to implement
- DI container (Zenject or VContainer)
- Sound and visual effects 

## Run and Build instruction
````
1. Clone the project.
2. Open with Unity 2021.3.43f or higher version.
3. Press "Play" button to play in Editor.
4. Or go to Build Settings and select platform that you want.
5. Click on "Build" button.
````
## Simple rule explaining
- One bet = chance to get one specific pay line combination
- With each next bet you increse amount of probable pay line combinations (bigger bet = bigger chance to win more money = also bigger chance to lose more money :) )
- Each symbol has multipliers for 3,4 and 5 same symbols in a row or diagonal or zig-zag (my implementation provides easy configuration of pay lines)
- The payout is made for all paylines that can occur in one spin. That is, if two combinations come up, the payout is for two combinations. 
  
## Demo
(The identic symbols just for testing)

https://github.com/user-attachments/assets/d417bd05-18a7-465c-817a-0785f59cd5fd


https://github.com/user-attachments/assets/06c4f7b3-4068-4970-93ab-061d062ec686

(Test configuration of pay lines)

https://github.com/user-attachments/assets/b6771f11-3ea4-42cf-a00f-7b9eba066f8b

