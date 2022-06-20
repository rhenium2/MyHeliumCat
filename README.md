# HeliumCat - Helium hotspot utility

I decided to make a .NET Cli application to help me understand helium hotspots more.
It requires [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to be installed.

Current functions:
* front: beacons in the semi-circle(270° to 90°) from the position of the hotspot
* radius: beacons in the 360° radius around the hotspot
* box: beacons in the witnessed box around the hotspot
* direction: calculates the direction between two hotspots
* distance: distance statistics for the witnessed hotspots in the last 5 days 

## Usage

### front
beacon stats of hotspots in front semi-circle, in the past _x_ minutes and _y_ kilometers radius 
```
dotnet HeliumCat.dll front <hotspot animal name> --past 10 --radius 5
```
<details>
    <summary>example output</summary>

```
Hello, World!
Staring Front Semi-Circle Beacon Stats for the past 10 minutes ...
There are 867 hotspots in front of me, in 30km semi-circle radius
There has been 1765 beacons in the world
- dazzling-clear-tardigrade {SenseCAP, 2.8dBi, 0m} (15341.7m/NW/299°) ... beacons: 1, witnessed: 0, missed: 1
- alert-mossy-woodpecker {Milesight, 5.8dBi, 4m} (18771.2m/SW/243°) ... beacons: 1, witnessed: 1, missed: 0

--- beacon statistics ---
total: 2 , witnessed: 1 , missed: 1
Done
```
</details>

### radius
beacon stats of hotspots in a radius
```
dotnet HeliumCat.dll radius <hotspot animal name> --past 10 --radius 5
```

### box
beacon stats of hotspots in box area based on last 5 days witnessed locations
```
dotnet HeliumCat.dll box <hotspot animal name> --past 10
```

### direction
calculates the direction between two hotspots
```
dotnet HeliumCat.dll direction <hotspot1 animal name> <hotspot2 animal name>
```

### distance
distance stats of hotspot witnessed in the last 5 days
```
dotnet HeliumCat.dll distance <hotspot1 animal name>
```