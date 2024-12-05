Mini-Project

Project Name: Labyrinth Zombie Shooter

Name: William Alexander Abildtrup

Student Number: 20233916

Link to Video: <https://www.youtube.com/watch?v=Q2hf3Wk-QqY>

Link to Project: <https://github.com/willineed/Pi3DZombies>
# Overview of the Game:
The idea of the project is to move through the level while surviving and shooting zombies. This game has taken inspiration mainly from Left 4 Dead 2. The goal of the game is to survive and reach the exit by making your way through the labyrinth but beware of the zombies. Use keyboard controls WASD to move, r to reload, left mouse button to shoot, mouse to look around, spacebar to jump.

## The main parts of the game:
- Player – Remy, moved with WASD, mouse to look around, left mouse button to shoot, r to reload, spacebar to jump (only for getting around zombies on the ground) . Has 100 Health Points (HP). Gun deals 10 Damage (dmg).
- Camera, pivot around with the use of the mouse
- Enemies – Zombies: They Scream while getting hit or in detection range of the player, they will then run towards the player and try to kill them. Deals 10 dmg and has 30 HP.
- Start zone: Mine aesthetic, spawn point safe from enemies
- Labyrinth: Play area, navmesh set up so that zombies can move around and chase player
## Game Features:
- You have a limited amount of ammo so you need to be accurate in your shots.
- Zombies scream and are completely silent after, so watch your back
## Different parts implemented from the list provided
- Interactive camera that the user can move using WASD and the mouse.
- A character that can be controlled through input by the user from CharacterBehaviour.cs.
- Character that the user can interact indirectly with through shooting zombies, collide with walls and zombies.
- Materials with different properties. All the assets are imported with different base colors, roughness, normal maps, and Ambient Occlusion. All the lights in the game are point lights.
- Physics interactions like collisions with walls, floor, enemies and raycasting through the ShootBehaviour
- A GUI that display: Red text on death, normal text on win, displays maxammo and current ammo in mag + current gun image, portrait frame that displays: healthbar made with a slider, specific health number, character playing currently. Crosshair in the middle of the screen made with an image
- Enemy zombies are navigating randomly around their spawnpoint through navmesh, when in detection range or shot by player, runs towards the player through navmesh SetDestination.
- Shell of the labyrinth and door to labyrinth created with ProBuilder. 
- Animations on player and enemies  + ragdoll on enemy upon death
- Using particle system to play a muzzle flash effect when shooting.
# Project Parts:
- Scripts:
  - PlayerBehaviour – used for movement, taking damage, Death logic such as reloading the scene, updating relevant UI elements by accessing UIManager.
  - ShootBehaviour – used for shooting a gun, reload, raycasting, updating relevant UI elements by accessing the script UIManager.
  - Enemy – Holds every logic related to the zombies. Allows for detection of player when entering a certain range, chasing the player, screaming before chasing the player, wandering around on navmesh when not engaged, attacking the player, take damage. Enabling and disabling ragdoll, boxcollider, animator, navmesh agent.
  - UIManager – singleton that can be called in every script to make changes to every UI element that requires it.
  - CameraController – controls the rotation of the camera.
  - Win – Script placed on trigger cube to display win text when player enters.
- Models & Animations & Materials:
  - Quixel Imports (not everything in the folder is used, but items listed are)
    - Modular Mine Tunnel Kit: <https://www.fab.com/listings/454001c6-075c-411c-8682-28c098596e2e>
    - Metal Pendant Light: <https://www.fab.com/listings/7cc43b4f-603c-407d-a4db-75fa0f251b4d>
    - Broken Concrete Slab: <https://www.fab.com/listings/b2b9abf7-137c-49b5-bc66-03b42564d43d>
    - Mine Cart: <https://www.fab.com/listings/67026011-cc6a-4b2e-a669-2745e6d8c9ca>
    - Modular Railway track kit: <https://www.fab.com/listings/bfb70324-05a7-4fb8-bf6f-0e4feb5057e9> 
    - Mine Rock Wall: <https://www.fab.com/listings/ed7707c2-b68b-49d2-9157-6ec7e6da1c6a>
    - Tundra Rocky ground: <https://www.fab.com/listings/85873b19-be89-49a6-b983-32882542e817>
    - Quarry Cliff: <https://www.fab.com/listings/512b5b32-cd62-45bf-b38f-b862cfe0e24d>
  - Fab Imports
    - Browning Pistol: <https://www.fab.com/listings/f8d94e88-df3e-4b7c-bae7-e3bc355e7d99>
  - Mixamo Imports: <https://www.mixamo.com/>
    - Remy (player character)
    - Pistol/Handgun Locomotion Pack
    - Shooting
    - Zombiegirl W Kurniawan (enemy zombie)
    - Scary Zombie Pack
- Audio Effects: <https://pixabay.com/da/sound-effects/>
  - 9mm pistol shot by freesound\_community: <https://pixabay.com/sound-effects/search/pistol%20sound/>
  - Zombie Screaming by Alex\_Jauk: <https://pixabay.com/sound-effects/search/zombie/>
- Images:
  - Crosshair: <https://www.pngwing.com/en/free-png-njyuj>
- Scenes:
  - One scene: SampleScene
- Testing:
  - Tested on Windows
# Time Management

|**Task**|**Time it Took (in hours)**|
| :- | :- |
|Setting up Unity and making a project on Github|0\.5|
|Research and conceptualization of game idea|1|
|Searching for 3D models Environment – lamp,walls, mine assets, rocks, pebble ground|1|
|Setting up Environment, importing environmental assets, setting up materials|2|
|Searching for 3D models characters + animation|0\.5|
|Setting up characters + animation + unpacking|3|
|Making camera movement controls and initial testing|0\.5|
|Player movement|0\.5|
|Combining player movement with camera orientation, bugfixing|1|
|Setting up shooting logic, bugfixing|2|
|Enemy movement and attack logic + navmesh|2|
|Searching for 3d model gun + Setting up gun for player use|1|
|Making muzzle flash for gun|0\.5|
|Making UI elements and bugfixing + trial and error.|2|
|Setting up enemy Ragdoll|1|
|Setting up prefabs and moving stuff between scenes|0\.5|
|Code documentation|1|
|Making readme|0\.5|
|**All**|**20.5**|

# Used Resources
- How to make a Muzzle Flash using Unity’s Particle System: <https://www.youtube.com/watch?v=rf7gHVixmmc>
- Adding a Player Health Bar: <https://medium.com/nerd-for-tech/adding-a-player-health-bar-d59d629c1311>
- Github Copilot for creating scripts and bugfixing.








