using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Update_Checker
{
    public class DownloadManager
    {
        public void Download()
        {
            // Starts Chill Updator
            Process process = new Process();

            process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"Chill\UC-Update-Downloader.exe";
            process.Start();

            // Closes the application
            System.Environment.Exit(0);

        }
    }
}
