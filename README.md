# Music Manager Application #

This solution contains an easy to use and simple music manager.

## Technologies
- Visual Studio 2022 
- .NET 8 SDK
- Node.js (for Tailwind CSS)
- Git
- Entity Framework Core 
- Blazor
- C#
- Serilog logging library

## Local Enviroment Setup
You need to have the .NET 8 SDK installed. Be sure to download the latest version for Visual Studio 2022.

Use the latest version of Visual Studio 2022: https://visualstudio.microsoft.com/downloads/

Install the node packages before building the solution by using ```npm install```

### Note: If developing on Mac:
Visual Studio is retired on Mac as of August, 2024, so if working on mac run project with Visual Studio code or .NET CLI. The features of Visual Studio for Mac were moved to VS Code as extensions, or you can use the .NET CLI to enter commands in terminal.

Setting Up Local Environment on Mac
2. Clone the repository
3. Run `npm install` to install dependencies.
4. Build and run the server project. In the project's root directory run the command
 
`dotnet watch run --project Server` 

`--project` incates running the Server project.

## Features
- **Song Rating**: Added a rating field to the song model and implemented input validation to ensure rating is a number 1 - 10.
- **Search Songs**: Filter songs based on title, artist, album, or genre dynamically as the user types.
- **Song Details**: Detailed view of songs, displaying  genre, release date, rating, and placholder for album cover for individual songs.
- **Logging**: Added Serilog package for logging in the server for console logs and writing logs to a file.
- **Error Handling**: Try/Catch blocks in the server controller.

## Additional Features
- **Sort songs** by: 
    - alphabetically (ascedning or descending)
    - date added (recently or earliest)
    - rating (highest to lowest or lowes to highest)
- **Edit Song**: Edit existing song in the database. Implemented input validation on frontend and backend.
- **Release Date Field**: added release date field to the song model.
- **Improved UI**: Changed the view of the songs list to look more modern, inspired by Spotify's song view.
- **Delete Songs**: Button for deleting individual song from database

## Project Structure:
Each directory contains a .proj project within the application.
- **Client**: Contains the front end written with blazor components. The Tailwind file is also stored here, which is built when the project is built.
- **Server**: Instructions for configuring the database. API Endpoints for the client to call. Also stores the Logging directory and text file that is created as server is running. The Server is the entry point for the application. It runs and serves the client.
- **Shared**: Contains infomration used by the client and server, such as the Song model
- **music-manager-starter.Data**: Stores database context and contains record of Database migrations. The model for the database is defined here. Here is where migrations are run.
- **Styles**: Contains the directives to set up Tailwind CSS.