using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Colorful;
using Console = Colorful.Console;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;

namespace Proxiessourcecode
{
    class Program
    {
        public static string type = "";
        static void Main(string[] args)
        {
            Console.Title = "Proxy scraper/checker - Created by Vanix#9999";
            for (;;)
            {
                Console.WriteLine("Which Type do you want to check? Http/socks4/socks5");
                type = Console.ReadLine().ToLower();
                bool check = type.Contains("http") || type.Contains("socks4") || type.Contains("socks5");
                if (check)
                {
                    break;

                }
                Console.WriteLine("Unknown type..");
                Thread.Sleep(100);
                Main(args);
            }
            Console.Clear();
            DiscordRPC.RPC();
            string folder = Directory.GetCurrentDirectory() + "\\Results\\" + DateTime.Now.ToString("H.mm.ss - dddd, dd");
            if(!Directory.Exists("Results"))
            {
                Directory.CreateDirectory("Results");
            }
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists("Proxy.txt"))
            {
                var file = File.Create("Proxy.txt");
                file.Close();
            }
            Design.Logo();
            Console.WriteLine("!! All of your Proxies sources should be in Proxy.txt !!");


            if (new FileInfo("Proxy.txt").Length == 0)
            {
                Console.WriteLine("File is empty!!");
                Console.ReadLine();
            }

            foreach (string line in File.ReadLines("Proxy.txt"))
            {
               var httpRequest = new HttpRequest();
               string req = httpRequest.Get(line).ToString();
                foreach (object val in new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})").Matches(req))
                {
                    try {
                        Match Proxies = (Match)val;
                        File.WriteAllText(folder + "\\Proxies-ALL.txt", Proxies.Value +  Environment.NewLine);
                        try
                        {
                            if (type.Contains("http"))
                            {
                                httpRequest.Proxy = HttpProxyClient.Parse(Proxies.Value);
                            }

                            if (type.Contains("socks4"))
                            {
                                httpRequest.Proxy = Socks4ProxyClient.Parse(Proxies.Value);
                            }

                            if (type.Contains("socks5"))
                            {
                                httpRequest.Proxy = Socks5ProxyClient.Parse(Proxies.Value);
                            }
                            httpRequest.Proxy = httpRequest.Proxy;
                            httpRequest.IgnoreProtocolErrors = true;
                            httpRequest.UserAgent = Http.ChromeUserAgent();
                            string text2 = httpRequest.Get("https://iplocation.com/", null).ToString();
                            if (text2.Contains("Your IP address"))
                            {
 
                                string location = Regex.Match(text2, "<span class=\"country_name\">(.*?)</span>").Groups[1].Value.ToString();
                                string region = Regex.Match(text2, "<span class=\"region_name\">(.*?)</span>").Groups[1].Value.ToString();
                                Console.Write("[+] " + Proxies.Value + " - " + location + " - " + region +"\n", Color.DarkGreen);
                                File.WriteAllText(folder + "\\Proxies-CHECKED-WORKING.txt", Proxies.Value + Environment.NewLine);
                            }
                        }
                        catch(Exception)
                        {
                            Console.Write("[-] " + Proxies.Value + "\n", Color.Red);
                            File.WriteAllText(folder + "\\Proxies-CHECKED-SHIT.txt", Proxies.Value + Environment.NewLine);
                        }
       
                    }
                    catch
                    {
                    }
                }



            }
            Console.ReadLine();

        }
    }
}
