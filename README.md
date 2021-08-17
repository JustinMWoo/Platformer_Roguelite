# Platformer_Roguelite

## Overview
This repo contains a working prototype for a platformer game. The premise of the game is to go from level to level defeating enemies and collecting 
abilities and upgrades that can drop from the enemies or from locations throughout each level. Abilities give the player a new action skill for 
attack or utility and also provide changes to other skills through "mutations".  

## Table of Contents  
* [Controls](#controls)
* [Enemies](#enemies)   
* [Abilities and Mutations](#abilities-mutations)
  * [Ability Mutations](#ability-mutations)
  * [General Mutations](#general-mutations)
  * [Major Mutations](#major-mutations)  


<a name="controls"/>  

### Controls
The arrow keys and space can be used to control the character and 'a', 's', 'd' and the left shift button are used to activate abilities. New abilities 
can be equipped by walking over the ability drop and pressing the key to assign the ability to, if there is already an ability equipped to that slot, 
it will drop to the ground and be replaced with the new ability (Note: only movement abilities can be equipped in the bottom slot activated by the left shift button).
Interactables can be used by pressing z when nearby and the prompt is shown.  

<img src="Readme/controls.gif" width = "600">  

<a name="enemies"/>  

### Enemies  
**General**  
Enemies will patrol to the ends of the platform it is currently on until the player enters it's line of sight. Upon sighting the player the unit will attempt to attack the player with the behavior different between enemy types. The currently implemented enemy types are below.  

**Melee**  
When a player enters the vision of a melee enemy, the enemy will lock onto the player and chase them down (done through the use of the A* Pathfinding Project). Melee enemies will deal damage upon colliding with the player.

<img src="Readme/enemies_melee.gif" width = "600">  

**Ranged**  
When a player enters the vision of a ranged enemy, the enemy will attempt to get within a certain range of the player and fire it's projectile towards the player.

<img src="Readme/enemies_ranged.gif" width = "600">  

**Boss**  
Upon entering the boss arena the player will be locked in and combat will start with the boss. The currently implemented boss attacks by tracking the player and drawing a bouncing path for a few seconds. The path is then locked in and the boss will begin teleporting to the ends of each line in order and dealing damage to the player if they are in said path.

<img src="Readme/enemies_boss.gif" width = "600">  

<a name="abilities-mutations"/>  

## Abilities and Mutations

<a name="ability-mutations"/> 

### Ability Mutations
Defeated enemies have a chance to drop abilities on death which can then be equipped by the player. Abilities come in different tiers with higher tiers having a lower chance of being dropped changing depending on the level of the slain enemy. Abilities have mutations that provide modifiers to the other equipped abilities. The player can pick and choose abilities that best benefit their other equipped abilities (Note: currently only some abilities have implemented mutations).  

<img src="Readme/ability_mutations_1.gif" width = "600">  

<img src="Readme/ability_mutations_2.gif" width = "600">  

<a name="general-mutations"/> 

### General Mutations
General mutations can be picked up from certain locations on the map. When interacting with these spots, the game will pause and provide a choice of 3 choices in mutation that the player can select. These mutations provide permenant enhancements to the player's character.  

<img src="Readme/general_mutations.gif" width = "600">  

<a name="major-mutations"/> 

### Major Mutations
Upon defeating the boss and interacting with the exit of the level, the player will be brought to a menu where they can purchase more significant mutations for their character using experience gained from defeating enemies.  

<img src="Readme/major_mutations.gif" width = "600">  
