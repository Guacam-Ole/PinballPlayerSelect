# About PinballPlayerSelect (PPS)
Pinballplayer-Select (PPS) is a tool to allow the selection of number of players in your Pinball Cabinet remaining the "look and feel" and using the DMD - Display for the player selection. 
This documentation assumes you are using it with PinballX and Zen Studios Pinball FX3/FX/M. So for any other combination modify your settings accordingly.

# Installation
Just extract the files into any directory on your windows machine. 

# Configuration
## Backup
**Before you change anything to PinballX make sure you have a backup of the PinballX.ini** 

## Autoconfig
The recommend way to configure PPS is to use the AutoConfiguration - Feature. Just start PPS.exe and a window will appear. Select the PinballX-Location by using the "Browser" button. Then click onto "Read Settings". PPS now simply copies the configuration from PinballX and displays this data. As this configuration is directly taken from PinballX there should be no adjustments necessary on the left "screen" - side. On the right side you can select which design you want to use. After selecting one of the options (e.g. "indyHands" a picture displaying the players selection will appear)

If you are happy with your choice click onto the "Write Config" - Button

After that the PinballX.INI - File will have been modified and also the config.json in the PPS-Folder. When Selecting a Zen Pinball from PinballX you should now see a Player selection on the DMD. Select the number of Players with the right or left Pinball Button and start the game through the start button. 

## Manual Config
If after Autoconfig everything works fine but you want to tweak a bit you can manually edit more settings:



### Selection-Screen by game
Apart from the default layout you can always add Table-Specific Selection Images. You can do this in the `Overlays`- Section of the Config:
```
 "overlays": [
   // default:
   {
     "prefix": "indyhands",
     "width": 100,
     "height": 100
   },
   // Only Indy Jones (fx3+fx)
   {
     "filter": "WMS_Indiana_Jones,133",
     "prefix": "choosewisely",
     "width": 100,
     "height": 100
   },
   // Pinball FX Classic Pinballs (LED displays)
   {
     "filter": "129,134,135,148,149",
     "prefix": "led",
     "width": 100,
     "height": 100
   }
 ],
```
prefix must be any of the images found in the "pix"-directory of PPS. Feel free to add (and share!) your own images. Filter can have one (or more) table names exact the same way as they are used by PinballFX. In this example the "choosewisely" image is shown when starting an Indy Pinball, and the simple LED-Screen if one of the early 90s Pinballs from PinballFX. Otherwise the "indyhands" Overlay is used


### Other Emulators
Other Emulators like Virtual Pinball usually do not need a player selection beforehand because the Playerselection is done within the game. Therefore this is not supported by the Autoconfig. But if you have some exotic requirements you can add them manually into the Config. Just add a new entry in the "emulators" - Section and also modify the PinballX.ini if necessary:

```
  "emulators": [
    {
      "name": "weird",
      "workingPath": "C:\\pinball\\steam\\steamapps\\common\\Weird Pinball",
      "executable": "WeirdPinball.exe",

      "TwoPlayers": "-hotseat_2",
      "ThreePlayers": "-hotseat_3",
      "FourPlayers": "-hotseat_4",
      "media": "c:\\pinball\\PinballX\\Media\\WeirdPinball"
      }
    }
]

```


 
## Test
PPS allows you to run int in Testmode. You can do this by simply starting it with "test" before the other paramters. E.g. "PPS.exe test fx3 120 -...."
In this case instead of launching a game after the "Start"-Button has been pressed it only displays the commandline that would have been called instead.
In addition all configured screens are now resizable and movable. While you do that the position and size informations are shown.

## Finishing touches
You might need to change the properties to be run as Administrator. In addition when working with Steam it can be a bit annoying when a new Program wants to start a game. 
This is usually only displayed ONCE per table, but you have to allow the game that single time. Because any external launcher like PinballX is ABOVE that steam window you should run PPS once from the commandline to check if everything works:
`PPS fx3 [tablename]`
so for example:
`PPS fx3 FamilyGuy`

This should start Pinball FX3 with Family Guy and the number of Players you selected

## Advanced Configuration parameters:
PPS has some advanced configuration Options you _usually_ should not need to touch. But you are free to do anyways if your machine is somehow special.

Adding `"stayopen":"true"` to the config will allow PPS to stay open until FX3 is closed. 
Adding `"batchmode":"true"` to the config will not start FX3 at all but create a batchfile instead that would open it

Usually the default selection starts with one player. You can change this by adding `PlayerCountAtStart=x` to the `Input` - Area in the config (where x is the number of players)



