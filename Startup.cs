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
using System.Text.RegularExpressions;
using Leaf.xNet;



namespace Proxiessourcecode
{
    class Startup
    {
        public static string checker = "";
        public static string color = "";
        public static string configdefault;
        public static string Failed_WriteOn_Console = "";

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

            if (!File.Exists("Config.json"))
            {
                var httpRequest = new Leaf.xNet.HttpRequest();
                configdefault = httpRequest.Get("https://pastebin.com/raw/mWeWeiQu", null).ToString();
                var file = File.Create("Config.json");
                file.Close();
                using (StreamWriter writer = new StreamWriter("Config.json"))
                {
                    writer.Write(configdefault);
                }
            }
            // We are using Regex for the Config.json because i am a retard.
            Regex Checker = new Regex("  \"Checker\":\"(.*?)\",");
            Regex Colors = new Regex("  \"Color\":\"(.*?)\",");
            Regex Failed_Console = new Regex("  \"Write Failed_hits on console\":\"(.*?)\",")
            using (StreamReader r = new StreamReader("Config.json"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Match Checker_match = Checker.Match(line);
                    Match Colors_match = Colors.Match(line);
                    Match Failed_Console_Match = Failed_Console.Match(line)
                    if (Checker_match.Success)
                    {
                        checker = Checker_match.Groups[1].Value;
                    }
                    if (Colors_match.Success)
                    {
                        color = Colors_match.Groups[1].Value;
                    }
                    if (Failed_Console_Match.Success)
                    { 
                        Failed_WriteOn_Console = Failed.Console.Match.Groups[1].Value;
                    }
                }
            }
            // Starting Discord RPC...
            DiscordRPC.RPC();
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
