# Aire Logic Tech Test

## PreRequisites
.Net Core 3.1 SDK

## Building
From the commandline

> dotnet restore

> dotnet build 


## Running
From the commandline

> Usage: ArtistLyrics [options]
>
> Options:
>   -?                              Show help information

>   -a|--artist-name <ARTIST_NAME>  Artist Name`

eg 

> ArtistLyrics -a "Pink Floyd"

## Assumptions

If multple artists are found with the same name the first will be used.
The lyrics are only taken from the first 10 songs found. Only one artist can be searched for at one time.

## Notes

I have created three projects within the solution.  
ArtistLyrics.Core which contains the core functionality.  
ArtistLyrics.Core.Tests for integration and unit tests for the core project.  
ArtistLyrics which is the console application and consumes the core project.  

The following packages have been utilised in the solution  
RestSharp - for connecting to REST endpoints and conversion of JSON into c# objects  
McMaster - for commandline argument parsing  
Serilog - For logging  
XUnit - For testing  

## Future releases
Remove hard coding of UserAgent and put in settings  
Create Web front end  
Add tests for LyricsService and MusicBrainzService  
Setup CI pipeline to run tests etc  
