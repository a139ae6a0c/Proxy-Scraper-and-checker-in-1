using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Drawing;
using Colorful;
using Console = Colorful.Console;



namespace Proxiessourcecode
{
    class Startup
    {
        public static void start(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("Which Type do you want to check? Http/socks4/socks5");
                Program.type = Console.ReadLine().ToLower();
                bool check = Program.type.Contains("http") || Program.type.Contains("socks4") || Program.type.Contains("socks5");
                if (check)
                {
                    break;

                }
                Console.WriteLine("Unknown type..");
                Thread.Sleep(100);
                Program.Main(args);
            }
            Console.Clear();
            // Starting Discord RPC...
            DiscordRPC.RPC();
            // Checks if Directory exists..
            if (!Directory.Exists("Results"))
            {
                Directory.CreateDirectory("Results");
            }
            // Checks if Directory exists..
            if (!Directory.Exists(Program.folder))
            {
                Directory.CreateDirectory(Program.folder);
            }
            // Checks if proxy.txt exits
            if (!File.Exists("Proxy.txt"))
            {
                var file = File.Create("Proxy.txt");
                file.Close();
            }
            // Check if the proxy.txt contains something..
            if (new FileInfo("Proxy.txt").Length == 0)
            {
                Console.Clear();
                Console.WriteLine("File is empty!! Press enter to close the program...", Color.Red);
                Console.ReadLine();
                Environment.Exit(1);
            }

        }
    }
}
