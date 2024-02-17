# 2DGameAnimation
This Unity project is my implementation of a level of Mictlán: Beyond the colours. Whis is my Final project of the subject 2D Game Animation, but also the beginning of a long-term project. You can find more information about it in my Website.

## How to play it
### Keyboard & Mouse
To move around, you can use WASD and the arrows on the keyboard, as it is 2D, only WA and left and right will work. To 
### Controller
Using the controller, you can use the left stick to move, and the south button to jump.

## Winning / loosing
Winning is possible by completing the challe: going into the dark and finding the light out. AKA, going left and then colliding with the pretty particle system.

Loosing is done by loosing your spirit before completing the challenge, which happens if too many arrows hit you.

## The level
The level I am currently implementing is "The obsidian Mountain." In here, Xolotl (my main character, who you can get to know more about [here](https://anniebonav.com/characters-creation/#XolotlSection) ) needs to find the way out, while being trapped on a mountain where arrows are being thrown at him.

The most important components are:
- A round world that rotates
- Xolotl as the player, who can run and jump
- Arrows Launcher

## Cool coding/artsy things
### * *THE SHADERS* *
The In-Game background might look simple, but it is actually a coded shader which's color changes considering the x value of the UV and the current rotation the user has. This helps recreate a day/night environment.

### Object Pooling
I implemented a fully generic Object Pooling Class wich's instance can be used to create a Pool of any Type. An Object Pool is useful in, for example, a projectile launcher, where there will be multiple instances of a Projectile. Instead of creating and destroying them, we create N instances of the object (in this case, a projectile) at the start of the scene. And then activate them and deactivate them as needed. This improves performance!

https://user-images.githubusercontent.com/46715001/232895701-e4670d52-16dd-46e8-b3a1-6c09c0778c66.mp4

### Projectile Launcher
To launch my arrows, I have a ProjectileLauncher Class which has an instance of ObjectPool with type Projectile. The Projectile handles all of the logic on how to move itself, and it is separate from the actual rendered object which, in my case, is an Arrow prefab. But it could be anything else as a projectile, as the Prefab that is taken can be changed in the editor.

### Animations
Xolotl animations were made using Spine. This means that changing between them in Unity is pretty cool, using Spine-Unity integration and API.

### Using Unity's new input system
All of the input is gotten using Unity's new input system. Currently, there is a specific input system for XBox controller, and Moyse and Keyboard. I am planning on adding key-rebinding as a possibility for users, and take advantage of the Input System for it.

## Current status
As of the 12th of May:
- Generic Object pool and Projectile Launcher (with arrows) are implemented.
- There are animations of Xolotl (the character) and the calendar. The calendar is not really useful right now, but it is cool, and if you hover over it, it speeds-up. You can laso pause it by clicking.
- There is an input system that supports controller and keyboard and mouse.

The current state is good enough for hand-in!


## Moving forward
### Cleanig up prefabs
Some of the prefabs need to be cleaned up and used around scenes, like the clouds. Which could be returned to origin instead of having so many, so the same clouds are reutilized.

### Working on gameplay
The gameplay heavily relies on visuals, but this visuals are kind of hard to create, as they are mostly coded shaders. I need to make sure there is a good portray of the state of the game to the player only by using the colors and sound.

### Sound
Adding sounds, like background music and steps/collisions would make the experience better! One that I would love to have is a gear-turning-like sound when the calendar is rotating, and some drums when it clicks into place.




