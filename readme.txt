Team name: GoGoRabbit
Game name: A Chicken's Adventure

i. Start Scene file: MainMenu
ii. How to play and what parts of the level to observe technology requirements 
a) Basic control: player movements (WASD)
		  mouse (click for UI interaction + hover for tooltip)
		  esc (call out in-game menu)
b) Parts of the level to observe technology requirements:
	1) MainMenu Scene: Start menu GUI including a level selection feature.
	2) Level 1(Scene name: IntroArea): 
		a) 3D character with real-time control: six-degree of freedom control, animation when moving. 
		b) 3D world with Physics and spatial simulatoin: Movable boxes to overcome obstacles, activate button to solve puzzles. Crumbling bridges to make course more chanlleging.  
		c) GUI: Tutorial to help player familiarize with controls and game mechanics. Tooltips are provided throughout the level to give hits to player about the objectives of the subquests. 
		d) Real-Time NPC Steering behavior: Multiply NPCs. Including: 
			i) Monster NPC implemeted using AI state machine. NPC states including: patrol preset waypoints, targeting player when player goes into certain proximity, attacking player when player within attack range and defeated when player manage to go on top of it. Animation for corresponding AI states. Player can go into the bushes to hide from them. They gives out visual cues to indicate their alert status. 
			ii) Boss NPC implemented using AI FSM. It has two phases. In phase 1, it will stay static and spawn two types of minions. Blue minions are hostile to the Player. They will chase and attack the player but they have limited life span. Red minions will try to evade the player. Player have to chase them to gain access to a catapult. Player can use this catapult to do damage to the boss and cause it to enter the second phase. In phase 2, the boss will start to chase and charge at the player. It will not be damaged by the catapult unless stunned by the spikes spawned on the playfield. Player needs to lure the boss into spike traps three times to defeat the boss. 
			iii) Catapult automatically locks onto the boss. It throws a trajectile that followes a physics stimulated trajectile motion and explodes upon impacting the boss. 
			iv) Red and Blue minions behaviors are stated above in the boss NPC section. 
		e) Game Feel: Respawn/checkpoints on the map to save player progress. Visual cues are added to Hostile NPCs to provide an intuitive experience dealing with them. Animations and cutscenes are added throughout the level to add a feel of progression and accomplishment to the experience. 
	3) Level 2 (Scene name: Level2):
		a) 3D world with Physics and spatial simulation: Moving obstacles to make course more chanlleging. 
		b) Multiplen prop mechanisms to let charactor figure out how to show the road.
	4) Level 3 (Scene name: Level2demo):
		 a) Game Feel: Procedure generated maze creating replayability. 

iii. Known problem areas:
	a) Level2demo included in alpha is not procedure generated for stability purposes.
iv. Manifest:
	a) Tong Shen: 
		1) MainMenu scene and related assets
		2) IntroArea/Level1 scene and related assets
		3) All UI componenets including:
			i) Panels: MainMenu, WinPanel, DeathPanel, Tutorial, Tooltip, etc.
			ii) Buttons: Start, Exit, LevelSelection, Pause, etc
		4) NPC and related assets
		5) Player and related assets 
		6) Audio system and related assets 
		7) Overall integration and Common utilities
	b) Jianan Song
		1) Provides customization options such as maze width, height, and cell size. 
		2) Supports prefabs for walls and paths, allowing for easy visual customization
		3) Creates an entrance and exit point in the maze. 
		4) Efficiently removes walls between cells to create paths.
		5) Design survey and playtests objective
		6) Write playlets summary writeups
		7) Project management and set individual goals for the projects
	c) Chao Wang
		1) Generates a 2D maze in Unity suing a depth-first search algorithm.
		2) Implements a method to find unvisited neighboring cells.
		3) Replaces existing Gameobjects when instantiating new cells, avoding overlap. 
		4) Design survey and playtests objective
		5) Conduct in-depth analysis on the survey items
		6) Write playlets summary writeups
		7) Performs PMs for team project
	d) Dongli He
		1) Focus on video production and scripting
		2) Responsible for the entire process of video production, from initial concept to final product, including script creation, design, sound and debugging.
		3) Game mechanics creation, independently designed and implemented a coin collection mechanic, which involved coding, design and sound, as well as debugging to ensure the functionality worked properly.
		4) Set up points for coin collection
		5) Contributed ideas and insights to team brainstorming sessions, and collaborate with colleagues to bring new concepts to life. 
	e) Grace Kuo
		1) Design the game field in the scene Level 2, decorate with trees, and add audio resource in the game.
		2) Create 3 prop mechanisms to show bridge and road when the player complete the tasks.
		3) Add red monster (AI) to jump randimly on the block to let the player collect it. 
		4) Add whale to swim around the game field accroding to the waypoint.
		5) Alter water prefab, navigation, main camera on the scene Level 2. 

	f) C# Script:
		1) MazeGenerator.cs - Jianan Song, Chao Wang
		2) PlayerScript.cs - Grace Kuo, Tong Shen, Dongli He
		3) NPCSwim.cs - Grace Kuo
		4) RedNPC.cs - Grace Kuo
		5) ShowBridge.cs - Grace Kuo
		6) DestroyNPC.cs - Grace Kuo
		7) CoinCollector.cs - Dongli He
		8) DollController.cs - Dongli He
		9) CameraViewChanger.cs - Tong Shen
		10) DemoCamera.cs - Tong Shen
		11) ProgressionManager - Tong Shen
		12) BoostPad.cs - Tong Shen
		13) BossFightStarter.cs - Tong Shen
		14) CatapultSystemController.cs - Tong Shen
		15) CrumblingBridge.cs - Tong Shen
		16) ElevatorTrigger.cs - Tong Shen
		17) FinalDoorController.cs - Tong Shen
		18) GrassGroups.cs - Tong Shen
		19) Hazard.cs - Tong Shen
		20) LeftBoxProgressionSaver.cs - Tong Shen
		21) LevelManager.cs - Tong Shen
		22) MovingSpikes.cs - Tong Shen
		23) Projectiles.cs - Tong Shen
		24) RightBoxProgressionSaver.cs - Tong Shen
		25) SpawnPoint.cs - Tong Shen
		26) StunTrap.cs - Tong Shen
		27) TurretController.cs - Tong Shen
		28) WinScript.cs - Tong Shen
		29) BridgeActivator.cs - Tong Shen
		30) LeftSideQuestFinalButton.cs - Tong Shen
		31) MainFinalButtonController.cs - Tong Shen
		32) MazeActivator.cs - Tong Shen
		33) RedButton.cs - Tong Shen
		34) RightSideQuestFinalButton.cs - Tong Shen
		35) SpikeDeactivator.cs - Tong Shen
		36) TurretTrigger.cs - Tong Shen
		37) Boss_AI.cs - Tong Shen
		38) Enemy_AI.cs - Tong Shen
		39) EvasiveMinion.cs - Tong Shen
		40) HostileMinion_AI.cs - Tong Shen
		41) TargetDetection.cs - Tong Shen
		42) Whale_AI.cs - Tong Shen
		43) ObsticlePush.cs - Tong Shen
		44) CloseOtherPanels.cs - Tong Shen
		45) ButtonFunctions.cs - Tong Shen
		46) PauseMenuToggle.cs - Tong Shen
		47) Tooltip.cs - Tong Shen
		48) TooltipManager.cs - Tong Shen
		49) Tutorial.cs - Tong Shen
		50) TutorialController.cs - Tong Shen
		













