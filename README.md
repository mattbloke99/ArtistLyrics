# Aire Logic Tech Test

## PreRequisites
.Net Core 3.1 SDK

## Building
To build type the following on the commandline

dotnet build 


## Running


> Usage: ArtistLyrics [options]
>
> Options:
>   -?                              Show help information
>   -a|--artist-name <ARTIST_NAME>  Artist Name`

eg 

> ArtistLyrics -a "Queen"

## Assumptions

If multple artists are found with the same name the first will be used.
The lyrics are only taken from the first 10 songs found.