using System;
using System.Collections.Generic;
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
            string link = $"{Link}/{BranchName}/{SubPath}/{q}";

			if (!File.Exists(q))
            {
                try
                {
                    Wait($"PLEASE WAIT DOWNLOADING: [{link}]", () =>
                    {
                        Thread.Sleep(5000);
                        using (WebClient client = new WebClient())
                        {
                            client.UseDefaultCredentials = true;

                            Uri Uri = new Uri(link);
                            client.DownloadFileAsync(Uri, q);
                            while (client.IsBusy) { }
                        }
                      
                    });
					GC.Collect();
					if(File.ReadAllBytes(q).Length == 0)
                    {
                        File.Delete(q);
                        throw new Exception($"THE PACKAGE WAS NO IN THE CORRECT FORMAT");
                    }

				}
				catch(Exception ex)
                {
                    Console.WriteLine($"FAILED TO DOWNLOAD THE REQUIRED PACKAGE: [{q}] DUE TO THE FALLOWING REASON: \n{ex} LINK: [{link}]");
                    throw ex; 
                }
                return;
            }else{
				if (File.ReadAllBytes(q).Length == 0)
				{
					File.Delete(q);
					throw new Exception($"THE PACKAGE WAS NO IN THE CORRECT FORMAT");
				}
			}
        }
    }
}

