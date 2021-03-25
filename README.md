# StrandedDeep_Waypoints

- Download the newest release [this](https://github.com/deadkex/StrandedDeep_Waypoints/releases/download/1.0/SDWaypointsV1.0.zip) and put it into a folder
- Download https://github.com/warbler/SharpMonoInjector/releases/download/v2.2/SharpMonoInjector.Console.zip
  - Unzip into the same folder
- Start the game and wait until it loaded into the main menu!!
- **either**
  - start SDWaypointStarter.exe OR
  - create a .bat file with the command: *path to folder*\smi.exe inject -p Stranded_Deep -a *path to folder*\SDWaypoints.dll -n SDWaypoints -c Loader -m Load
- Press F1 ingame to enter the menu

#### Some info
- Will show up when loaded into a world. Will NOT show in the main menu!
- Click the currently selected WP to open the WP manager
- Waypoints get saved here: %USERPROFILE%\AppData\LocalLow\Beam Team Games\Stranded Deep\Data\Waypoints.json
- Saved Waypoints get loaded automatically


Seems to work on the epic version.  
You are using this at your own risk  

#### Changelog
- V1  
  - release  
- V2  
  - default hideallwaypoints setting is now false
  - size of waypoints can now be adjusted
  - waypoints automatically get saved after creating a new one

![Image](Screenshots/Screenshot1.PNG)
![Image](Screenshots/Screenshot2.PNG)
