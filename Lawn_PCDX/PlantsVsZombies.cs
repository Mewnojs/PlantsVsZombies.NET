﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace LAWN
{
    class PlantsVsZombies
    {
        static void Main()
        {
            var args = ParseCommandLineArguments();
            Main game = new Main();
            game.Window.Title = String.Format
            ("PlantsVsZombies.Net v{0}", Lawn.LawnApp.AppVersionNumber);
            game.mGameConfig = new();
            game.mGameConfig.mStoragePath = (string)(args[(int)CommandLineArgumentType.Storage] ?? null);
            game.mGameConfig.mIronpythonPort = (short)(args[(short)CommandLineArgumentType.IronPyInteractivePort] ?? 8080);
            game.Run();
        }

        static object[] ParseCommandLineArguments() 
        {
            string[] argstrs = Environment.GetCommandLineArgs()[1..];
            var args = new object[(int)CommandLineArgumentType.NumArgs];
            foreach (string argstr in argstrs) 
            {
                string[] arg = argstr.Split('=');
                if (arg.Length != 2) PrintHelpAndExit();
                switch (arg[0]) 
                {
                    case "-storage":
                        args[(int)CommandLineArgumentType.Storage] = arg[1];
                        break;
                    case "-ironpyport":
                        args[(int)CommandLineArgumentType.IronPyInteractivePort] = int.Parse(arg[1]);
                        break;
                    case "-help":
                    default:
                        PrintHelpAndExit();
                        break;
                }
            }
            return args;

            static void PrintHelpAndExit() 
            {
                Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} [-storage=<path>] [-ironpyport=<port>]");
                Console.WriteLine("  -storage: Path to the storage directory. Defaults to the current directory.");
                Console.WriteLine("  -ironpyport: Port to use for the IronPython interactive console. Defaults to 8080.");
                Environment.Exit(0);
            }
        }

        enum CommandLineArgumentType 
        {
            Storage,
            IronPyInteractivePort,
            NumArgs
        }
    }
}
