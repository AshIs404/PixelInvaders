# Pixel Invaders

**Pixel Invaders** is a retro arcade-style 2D space shooter game, developed in Unity. The player controls a triangular spaceship to fight waves of enemies while trying to avoid obstacles. 

## Game Description

Pixel Invaders is a mobile game where you navigate through outer space, using your white triangle spaceship to fight against waves of enemies represented by red triangles.

## Features
- **Retro 8-bit Style**: The game features a nostalgic retro style with pixel-inspired graphics.
- **Adaptive Aspect Ratio**: The game adjusts to different screen sizes, providing an optimal experience across all devices.
- **Three Increasingly Difficult Levels**: Progress through three consecutive levels with increasing difficulty to defeat the enemies.
- **Player and Enemy Shooting**: The player and enemies can shoot projectiles, with enemy attacks becoming progressively more challenging.
- **Shields**: Player can protect themselves with shields which absorb a limited number of hits.
- **Sound Effects and Music**: Includes retro-inspired background music and sound effects to enhance the game atmosphere.

## Installation Instructions
1. **Clone the Repository**: Clone the project using the command: 
   ```sh
   git clone https://github.com/AshIs404/PixelInvaders.git
   ```
2. **Open in Unity**: Open the project using Unity (version 6000.0.24f1 or above is recommended).
3. **Import Dependencies**: Make sure all dependencies are installed and set up correctly. Unity might prompt you to install additional packages.

## Controls
- **PC Controls**: Use `A` and `D` or the `Arrow` keys to move left or right.
- **Touch Devices**: Tap anywhere on the screen to the right or left of the character to move in that direction.

## Game Mechanics
- **Movement**: The player can move left or right using either keyboard input or touch gestures.
- **Shooting**: The player's spaceship shoots automatically when no shields are in front of it.
- **Enemy Behavior**: Enemies move as a group, and can also shoot projectiles at the player at random intervals.
- **Shields**: Shields have limited durability and can take up to three hits before being destroyed. Shields will also prevent the player from shooting if they are directly in front.

## Save System
- **Score Persistence**: The player's score is saved between levels using Unity's PlayerPrefs. Functions to save, load, and reset the score are implemented in the GameManager script.

## Credits
- **Game Developer**: Asher Ruvinov
- **Art & Design**: Concept and graphics follow retro, pixelated styles inspired by 8-bit classics.
- **Music and Sound Effects**: All soundtracks are unique and custom-made with AI tools. Sound effects are sourced from [freesound.org](https://freesound.org).

## Contact
For any questions or suggestions, feel free to reach out to:
- **Email**: asher.ruvinov@gmail.com
