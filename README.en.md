# PlantsVsZombies.net

[中文版本](./README.md)

[Wiki(Chinese)](https://github.com/Mewnojs/PlantsVsZombies.NET/wiki)

#### Description
An open-source cross-platform Plants Vs. Zombies project, written in C#, originated from codes in Windows Phone edition.

#### Project Structure
The whole project comprises of three parts, which are *Lawn*, *Sexy*, and *Sexy.TodLib*.
**Lawn** contains the very mechanic of the game system, including picking and spawning zombies, setting on the shooter plants, etc.
**Sexy** works as the intermediate layer between the top-level of the game and XNA framework, providing several managers which help to control multimedia resources, and basic UI widgets.
**Sexy.TodLib** controls of the rendering of graphic FXs.
**LawnModExtension** is a new module that provides modding support for the game using IronPython3.

#### Installation

1.  Clone into local;
2.	Open the solution with VS2022; (VSCode works too, but make sure don't compile with absolute project file path)
3.	For PCDX/PCGL, Complie and extract game contents (not included in this repo, see [contacts](####Contacts)) into the path of the compiled executable; For Android, extract game contents into `Lawn_Android\Assets\` first, then compile and deploy;
4.	Enjoy!

#### Notice

1.  This is an early yet unstable version of the project, things may have broken. If so, please report them as issues, which is very important for me. Thanks to all guys who contribute codes and report bugs to this project!
2.	Save files are in `.\docs\userdata` (this had changed since v0.13.1)

#### Modding
There are two ways to mod the game, each has its own pros and cons:
1. Fork this repo and modify the source code or resources directly, then publish your own version;
2. Make IronPython3 scripts to modify the game. This method doesn't require modifying the source code, and is compatible with different versions of PvZ (derived from this project), but the content you can modify is limited, and you need to have a certain understanding of Hooking techniques.
For the second method, you can refer to the examples in the LawnMod API document in the Wiki. The script files are placed in `.\mods\`, and the game will load them automatically when it starts.


#### Contacts

- Ported by: MnJS (aka. 2508)
- QQ Group: 884792079
- Discord Link: [discord.gg/uXz6g6Adnm](https://discord.gg/uXz6g6Adnm) 
