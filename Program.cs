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
        public static  string folder = Directory.GetCurrentDirectory() + "\\Results\\" + DateTime.Now.ToString("H.mm.ss - dddd, dd");
        public static string type = "";
        public static string location = "";
        public static string region = "";
        public static void Main(string[] args)
        {
            Console.Title = "Proxy scraper/checker - Created by Vanix#9999";
            // Going to Startup.cs
            Startup.start(args);
            // Write Logo on the console  
            Design.Logo();
            Console.WriteLine("!! All of your Proxies sources should be in Proxy.txt !!");

            foreach (string line in File.ReadLines("Proxy.txt"))
            {
               Console.Title = "Proxy scraper/checker - Url: " + line +  " - Created by Vanix#9999";
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
                                location = Regex.Match(text2, "<span class=\"country_name\">(.*?)</span>").Groups[1].Value.ToString();
                                //-------------------
                                Console.Write("[+] " + Proxies.Value, Color.White);
                                Console.Write(" Status: ");
                                Console.Write("Good ", Color.Green);
                                Console.Write(" - Location: ");
                                Console.WriteLine(location, Color.Green);
                                //-------------------
                                File.WriteAllText(folder + "\\Proxies-CHECKED-WORKING.txt", Proxies.Value + Environment.NewLine);
                            }
                        }
                        catch(Exception)
                        {
                            //-------------------
                            Console.Write("[-] " + Proxies.Value, Color.White);
                            Console.Write(" Status: ");
                            Console.WriteLine("Failed", Color.DarkRed);
                            //-------------------
                            File.WriteAllText(folder + "\\Proxies-CHECKED-SHIT.txt", Proxies.Value + Environment.NewLine);
                        }
       
                    }
                    catch
                    {
                    }
                }



            }
            Console.WriteLine("Done", Color.Green);
            Console.ReadLine();
            Environment.Exit(1);

        }
    }
}
