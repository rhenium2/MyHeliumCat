# HeliumCat - Helium hotspot utility

I developed this .NET Cli application to help me understand helium hotspots more.
It requires [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to be installed.

Current functions:
* [front](#front): beacons in the semi-circle(270° to 90°) from the position of the hotspot
* [radius](#radius): beacons in the 360° radius around the hotspot
* [box](#box): beacons in the witnessed box around the hotspot (the most bottom left and the most top right hotspots) in the last 5 days
* [direction](#direction): calculates the direction between two hotspots
* [witnessed](#witnessed): shows witnessed stats of hotspot with their distance and height analysis
* [rewards](#rewards): lists the rewards transaction for a financial year with their respective USD value (based on Helium Price Oracle at the time of the transaction), with the option to get an idea of it in other exchange currencies. This feature is **Only** for information purposes. 

## Usage Notes:
1. for hotspot, you can use any of:
   1. hotspot animal name (like _"Angry Purple Tiger"_)
   2. hotspot snake-case lowercase animal name (like _angry-purple-tiger_)
   3. hotspot id (Base58 address) as well (like _11cxkqa2PjpJ9YgY9qK3Njn4uSFu6dyK9xV8XE4ahFSqN1YN2db_)
2. for time, you use any of 
   1. past minutes (--past-m)
   2. past hours (--past-h)
   3. past days (--past-d)

## Usage Examples:
### front
beacon stats of hotspots in front semi-circle, in the past _x_ minutes and _y_ kilometers radius 
```
dotnet HeliumCat.dll front <hotspot name or address> --past-m 10 --radius 5
```

### radius
beacon stats of hotspots in a radius
```
dotnet HeliumCat.dll radius <hotspot name or address> --past-h 12 --radius 5
```

### box
beacon stats of hotspots in box area based on last 5 days witnessed locations
```
dotnet HeliumCat.dll box <hotspot name or address> --past-d 1
```

### direction
calculates the direction between two hotspots
```
dotnet HeliumCat.dll direction <hotspot1 name or address> <hotspot2 name or address>
```

### witnessed
shows witnessed stats of hotspot
```
dotnet HeliumCat.dll witnessed <hotspot name or address> --past-m 30
```
```
dotnet HeliumCat.dll witnessed <hotspot name or address> --past-h 2
```
```
dotnet HeliumCat.dll witnessed <hotspot name or address> --past-d 1
```

### rewards
Lists the rewards transaction for a financial year with their respective USD value (based on Helium Price Oracle at the time of the transaction), with the option to get an idea of it in other exchange currencies. This feature is **Only** for information purposes.
```
dotnet HeliumCat.dll rewards <hotspot name or address> --fy 2022
```
```
dotnet HeliumCat.dll rewards <hotspot name or address> --fy 2022 --currency aud
```