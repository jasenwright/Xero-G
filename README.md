#Xero-G 
##Jasen Wright && Conner Leverett
###UVic CSC 205 Final Project

###Intro
	The year is 20xx and the space station Mars Xplorer has gone silent. It is your 
	mission to figure out what's going on. Is the crew the safe? Are they even still alive?
	
###Game Type
	-2D action platformer shooter with unique level generation
	-Roguelike, endless gameplay.
	-As player advances through the game it gets more difficult by adding more enemies and increasing enemy health

###Technical Breadth
	-Audio
		-Background music (ambient)
		-Running sound
		-Firing sound
		-Explosion sound
		-Enemy death sound
		-Enemy talking sound when change FSM states
		-Crackle of flames of things that are on fire
		-Buzzing of lights
		-Sounds when picking up ammo / health
		
	-Light
		-Explosions, gun fire (muzzle flash)
		-Dynamic lighting with how level is lit (e.g the only light is where the player is looking)
		-Flickering lights
		-Swinging lights
		
	-Animation
		-Main player movement (running, crouching, climbing ladders, aiming, idle)
		-Enemy movement (turrets spinning)
		-POTENTIAL Limbs could blow off of enemies when shot
		
	-Physics / Collisions
		-Levels with different amounts of gravity which will effect environment in different ways
		-Explosions effect things depending on where they are located from the explosion
		-Explosions effect environment
		-Hit boxes of NPC have different levels of damage (critical hit areas)
		-Aim with mouse
	
	-AI
		-AI knows where boxes are to jump up on to gain better vantage point
		-Kamikaze enemies, try to run at you and blow up
		-Enemies try and dodge your bullets
		-Enemies take into account player motion and shoot in the direction there are heading
		
###Technical Depth
	-Procedural Generation
		-Levels are procedurally generated
		
		
###Credits
Health Pack - http://piq.codeus.net/static/media/userpics/piq_70042_400x400.png
Intro Theme - https://www.youtube.com/watch?v=tZtzn5GQOng
Tiles, Turrets - https://opengameart.org/content/open-gunner-starter-kit
Turret Laser sound - http://www.freesound.org/people/bubaproducer/sounds/151022/
