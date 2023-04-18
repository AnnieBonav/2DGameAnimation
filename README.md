# 2DGameAnimation
This Unity project is my implementation of a level of Mictlán: Beyond the colours. Whis is my Final project of the subject 2D Game Animation, but also the beginning of a long-term project. You can find more information about it in my Website.

## The level
The level I am currently implementing is "The obsidian Mountain." In here, Xolotl (my main character, who you can get to know more about [here](https://anniebonav.com/characters-creation/#XolotlSection) ) needs to find the way out, while being trapped on a mountain where arrows are being thrown at him.

The most important components are:
- A round world that rotates
- Xolotl as the player, who can run and jump
- Arrows Launcher

## Cool coding/artsy things
### Object Pooling
I implemented a fully generic Object Pooling Class wich's instance can be used to create a Pool of any Type. An Object Pool is useful in, for example, a projectile launcher, where there will be multiple instances of a Projectile. Instead of creating and destroying them, we create N instances of the object (in this case, a projectile) at the start of the scene. And then activate them and deactivate them as needed. This improves performance!

### Projectile Launcher
To launch my arrows, I have a ProjectileLauncher Class which has an instance of ObjectPool with type Projectile. The Projectile handles all of the logic on how to move itself, and it is separate from the actual rendered object which, in my case, is an Arrow prefab. But it could be anything else as a projectile, as the Prefab that is taken can be changed in the editor.

### Animations
Xolotl animations were made using Spine. This means that changing between them in Unity is pretty cool, using Spine-Unity integration and API

### Using Unity's new input system
All of the input is gotten using Unity's new input system. Currently, there is a specific input system for XBox controller, and Moyse and Keyboard. I am planning on adding key-rebinding as a possibility for users, and take advantage of the Input System for it.

## Current status
As of the 18th of april:
- Generic Object pool and Projectile Launcher (with arrows) are implemented.
- There is a placeholder for Xolotl going around a dummy world. But the interaction between the world and teh player collider is buggy, and the implementation will probably need to change.
- There is a test scene where Xolotl is already rendered and the animations are changing based on the current user input.
- The XBox Input binding exists.

The current state is still prototyping. ETA for a minimum playable game: May 4th!


