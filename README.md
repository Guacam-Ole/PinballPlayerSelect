# About PinballPlayerSelect (PBS)
Pinballplayer-Select (PBS) is a tool to allow the selection of number of players in your Pinball Cabinet remaining the "look and feel". 
This documentation assumes you are using it with PinballX and Pinfball FX3. So for any other combination modify your settings accordingly.

## Installation
Just extract the files into any directory on your windows machine. This program needs .NET Framework 5.0. If you do not have installed it yet simply start the application and a download-option should appear. Select the "desktop"-version for the framework

## Configuration
### PinballX
**Before you change anything to PinballX make sure you have a backup of the PinballX.ini** 
In Addition you will need some of the properties there and copy them over to PBS.

_You can use the settings-tool instead, but for the ease of this document we will describe working directly in the Ini-File_
1. Open `PinballX`.ini in the `config` Directory of PinballX
2. Search for `[PinballFX3]`
3. Make a copy from the Lines "WorkingPath", "Executable","Parameters". You will need them later
4. Change those to the following values:
```
[PinballFX3]
WorkingPath=C:\pinball\PlayerSelect
Executable=PBS.exe
Parameters=[TABLEFILE]
```
(Change "WorkingPath to the folder where you have PBS installed)


### PBS
Now you can modify the configfile from PBS. Start by copying "config.example.json" to "config.json". THe config.json should now look like this:
```
{
  "media": {
    "root": "c:\\pinball\\PinballX\\Media\\Pinball FX3"
  },
  "screens": {
    "playfield": {
      "id": 0,
      "rotate": 270,
      "overlayrotate": 180,
      "enabled": true
    },
    "dmd": {
      "id": 1,
      "rotate": 0,
      "overlayrotate": 0,
      "enabled": true
    },
    "backglass": {
      "id": 2,
      "rotate": 0,
      "overlayrotate": 0,
      "enabled": true
    }
  },
  "overlays": [
    {
      // default
      "DMD": {
        "prefix": "indyhands",
        "width": 100,
        "height": 100
      }
    }
  ],
  "launch": {
    "workingPath": "C:\\pinball\\steam\\steamapps\\common\\Pinball FX3",
    "executable": "Pinball FX3.exe",
    "parameters": "-applaunch 442120 \"-table_[TABLEFILE]\" [PLAYER] -offline -class",

    "TwoPlayers": "-hotseat_2",
    "ThreePlayers": "-hotseat_3",
    "FourPlayers": "-hotseat_4"
  },
  "Input": {
    "exit": 81, 
    "startgame": 13, 
    "moreplayers": 161,
    "lessplayers": 160
  }
}
```
Now modify the "media/root"-Value to where the root of your FX3-Media is. After that copy the values from your screens (playfield, dmd, backglass) to the according values in the json.
Finally you enter the "workingPath", "Executable" and "Paramaters" you copied from the INI-File.

_Note:_ put "[PLAYER]" into the "parameters"-value. This will be replaced by "-hotseat_x" lateron.

If your keycodes differ from the default (shift for right/left, enter for selection) you can also change the "input"-region.

Now you should be good to go.

## Test
PBS allows you to run int in Testmode. You can do this by simply starting it without any parameter. (you should have mouse/keyboard attached when doing this). 
In this case instead of launching a game after the "Start"-Button has been pressed it only displays the commandline that would have been called instead.
In addition all configured screns are now resizable and movable. While you do that the position and size informations are shown:

## Finishing touches
You might need to change the properties to be run as Administrator. In addition when working with Steam it can be a bit annoying when a new Program wants to start a game. 
This is usually only displayed ONCE but you have to allow the game that single time. Because any external launcher like PinballX is ABOVE that steam window you should run PBS once from the commandline to check if everything works:
`PBS [tablename]`
so for example:
`PBS FamilyGuy`

This should start Pinball FX3 with Family Guy and the number of Players you selected

## Overlays
The Overlays are the graphics that are shown for 1-4 players: 
```
"overlays": [
    {
      // default
      "DMD": {
        "prefix": "indyhands",
        "width": 100,
        "height": 100
      }
    }
  ]
  ```
Where `prefix` is the name of the graphics file and width and heihgt are set in Percent of the screen they are displayed on.

### Specific Overlays
You can add specific overlays for specific tables. For example display a specific overlay for classic pinball machines that have no DMD. You do this by providing a `filter` property.
The current example has the default overlay (no filter), a special overlay for the Indiana Jones pinball and an additional overlay for the Funhouse table
```
"overlays": [
    {
      // default
      "DMD": {
        "prefix": "indyhands",
        "width": 100,
        "height": 100
      }
    },
    {
       "filter":"WMS_Indiana_Jones",
       "width":100,
       "height":100,
       "prefix":"wisely"
    },
    {
       "filter":"WMS_FUNHOUSE",
       "width":100,
       "height":100,
       "prefix":"led"
    }
  ]
```

## Advanced Configuration:
PBS has some advanced configuration Options you _usually_ should not need to touch. But you are free to do anyways if your machine is somehow special.

### Generic
Adding `"stayopen":"true"` to the config will allow PBS to stay open until FX3 is closed. 
Adding `"batchmode":"true"` to the config will not start FX3 at all but create a batchfile instead that would open it

### Media
Normally the paths for playfield, dmd and backglass will automatically generated the following way:
"[root]\Table Images", "[root]\Backglass Images", "[root]\DMD Images"

If you store your backtround images on a different position you can configure those locations:
```
{
   "media": 
   {
      "playfield":"C:\\pinball\\pinballx\\media\\fx3\\tables",   
      "backglass":"C:\\pinball\\pinballx\\media\\fx3\\backglass",
      "dmd":"C:\\pinball\\pinballx\\media\\fx3\\dmds",
      
   }
}
```

### Screen
By default you only need to provide the ScreenId (0 or above). Then the complete area is filled by the playfield, dnd or backglass. If you have special requirements (like Backglass and DMD on the same screen) you can explictely define the position and size for that screen:
```
{
   "dmd":
   { 
      "enabled":"true",
      "id":0,
      "x":0,
      "y:20,
      "width":720,
      "height":320
  }
}
```

#### OnTop
Usually PinballX shows its "loading"-animation on the playfield-screen. If you also want to use that screen you can force PBS to be on top of it by adding the "`ontop`" setting:
```
{
   "playfield":
   {
      "ontop":true
   }
}
```

### Input
Usually the default selection starts with one player. You can change this by adding `PlayerCountAtStart=3` to the `Input` - Area



