# HBeacons - Helium hotspot beacon stats

I decided to make a .NET Cli application to show the beacons in the past n minutes around my hotspot. 
This was really helpful to see whether I couldn't receive a signal (aka antenna issues) or there simply isn't any beacon to witness.

There are 3 ways to search:
* front: beacons in the semi-circle(270° to 90°) from the position of the hotspot
* radius: beacons in the 360° radius around the hotspot
* box: beacons in the witnessed box around the hotspot


An example run is:
```
dotnet HeliumInsighter.dll front <hotspot animal name> --past 10 --radius 5
```
An example output is:
```
Hello, World!
Staring Front Semi-Circle Beacon Stats for the past 10 minutes ...
There are 867 hotspots in front of me, in 30km semi-circle radius
There has been 1765 beacons in the world
- dazzling-clear-tardigrade {SenseCAP, 2.8dBi, 0m} (15341.7m/NW/299°) ... beacons: 1, witnessed: 0, missed: 1
- alert-mossy-woodpecker {Milesight, 5.8dBi, 4m} (18771.2m/SW/243°) ... beacons: 1, witnessed: 0, missed: 1

--- beacon statistics ---
total: 3 , witnessed: 0 , missed: 3
Done
```
