using System;
using System.IO;
using System.Net;
using System.Threading;

namespace MiniTool
{
    public class QuickToolsDownloader
    {
        string BranchName { get; set; } = "testing";
        string Link { get; set; } = "https://github.com/Mel4221/QuickTools/raw";
        string SubPath { get; set; } = "bin/Release";
        static void Wait(string label, Action action)
        {
            int x, y;
            x = Console.CursorLeft;
            y = Console.CursorTop;
            char ch = '-';

            Thread work = new Thread(() => { action(); });
            work.Start();
            while (work.IsAlive)
            {
                switch (ch)
                {
                    case '-':
                        ch = '\\';
                        break;
                    case '\\':
                        ch = '|';
                        break;
                    case '|':
                        ch = '/';
                        break;
                    case '/':
                        ch = '-';
                        break;
                }
                Thread.Sleep(100);
                Console.SetCursorPosition(x, y);
                Console.Write($"{label} [{ch}]");
            }
            Console.WriteLine("\nDONE\n");
        }
        /// <summary>
        /// Dowload this instance.
        /// </summary>
        public void Dowload()
        {
            const string q = "QuickTools.dll";
            if (!File.Exists(q))
            {
                try
                {
                    Wait("PLEASE WAIT...", () =>
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.UseDefaultCredentials = true;

                            Uri Uri = new Uri($"{Link}/{BranchName}/{q}");
                            client.DownloadFileAsync(Uri, q);
                            while (client.IsBusy) { }
                        }
                    });
                }catch(Exception ex)
                {
                    Console.WriteLine($"FAILED TO DOWNLOAD THE REQUIRED PACKAGE: [{q}] DUE TO THE FALLOWING REASON: \n{ex}");
                }
            }
        }
    }
}

