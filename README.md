# Unity Aseprite Exporter
Unity plugin to convert aseprite files to png.

I am using Aseprite Animation Workflow (https://assetstore.unity.com/packages/tools/animation/aseprite-animation-workflow-167316), and sometimes stumble upon the need of checking what art is already integrated in the game and what is not. Problem is that file managers can show png, jpg and some other types of graphical files, but not aseprite files. So I have to run Aseprite and open every file manually. Also VCS support in Jetbrains Rider can show changes in graphical files, but again it doesn't support Aseprite.
A solution for this inconvenience is to have an exported png for every aseprite file. That's exactly what this editor plugin does: listen for changes in aseprite files, and saves them as png files.


Usage:
1. Copy the repository to your Unity project.
2. Update 'asepritePath' variable in 'AsepriteExporterCommon.cs' to reflect Aseprite's executable location.

If all goes well, then every time you change .ase or .aseprite files, Unity will convert aseprite files to png format.


!WARNING! This was tested in Linux Mint, so be ready to dive into code a bit during development on Windows or MacOS.

