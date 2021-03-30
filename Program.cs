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
using System.Net;


namespace Proxiessourcecode
{
    class Program
    {
        public static  string folder = Directory.GetCurrentDirectory() + "\\Results\\" + DateTime.Now.ToString("H.mm.ss - dddd, dd");
        public static string type = "";
        public static string location = "";
        public static string region = "";
        public static bool check_if_its_on;
        public static void Main(string[] args)
        {
            Console.Title = "Proxy scraper/checker - Created by Vanix#9999";
            // Going to Startup.cs
            Startup.start(args);
            // Write Logo on the console  
            check_if_its_on = Startup.checker.ToLower().Contains("on");
            Design.Logo();
            Console.WriteLine("!! All of your Proxies sources should be in Proxy.txt !!");
            if (check_if_its_on)
            {
                Console.WriteLine("Checker is on!!", Color.Tomato);
            }
            else
            {
                Console.WriteLine("Checker is off!!", Color.Tomato);
            }
                foreach (string line in File.ReadLines("Proxy.txt"))
            {
                string req = "";
                Console.Title = "Proxy scraper/checker - Url: " + line + " - Created by Vanix#9999";
                var httpRequest = new Leaf.xNet.HttpRequest();
                try
                {
                    req = httpRequest.Get(line, null).ToString();
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to get url: " + line, Color.DarkRed);
                    
                }
                    if (check_if_its_on)
                    {
                        try
                        {
                            foreach (object val in new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})").Matches(req))
                            {
                                Match Proxies = (Match)val;
                                File.AppendAllText(folder + "\\Proxies-ALL.txt", Proxies.Value + "\n");
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
                                    string text2 = httpRequest.Get("http://ip-api.com/json/", null).ToString();
                                    string region = "";
                                    if (text2.Contains("\"status\":\"success\""))
                                    {
                                        location = Regex.Match(text2, "\"countryCode\":\"(.*?)\"").Groups[1].Value.ToString();
                                        region = Regex.Match(text2, "\"regionName\":\"(.*?)\"").Groups[1].Value.ToString();
                                        //-------------------
                                        Console.Write("[");
                                        Console.Write("+", Color.DarkGreen);
                                        Console.Write("] ");
                                        Console.Write(Proxies.Value, Color.White);
                                        Console.Write(" Status: ");
                                        Console.Write("Good ", Color.Green);
                                        Console.Write(" - Location: ");
                                        Console.Write(location, Color.Green);
                                        Console.Write(" - Region: ");
                                        Console.WriteLine(region, Color.Green);
                                        //-------------------
                                        File.AppendAllText(folder + "\\Proxies-CHECKED-WORKING.txt", Proxies.Value + "\n");
                                    }
                                }
                                catch (Exception)
                                {
                                    //-------------------
                                    Console.Write("[");
                                    Console.Write("-", Color.DarkRed);
                                    Console.Write("] ");
                                    Console.Write(Proxies.Value, Color.White);
                                    Console.Write(" Status: ");
                                    Console.WriteLine("Failed", Color.DarkRed);
                                    //-------------------
                                    File.AppendAllText(folder + "\\Proxies-CHECKED-SHIT.txt", Proxies.Value + "\n");
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            foreach (object val in new Regex("(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})(?=[^\\d])\\s*:?\\s*(\\d{2,5})").Matches(req))
                            {
                                Match Proxies = (Match)val;
                                File.AppendAllText(folder + "\\Proxies-ALL.txt", Proxies.Value + "\n");
                                Console.Write("[");
                                Console.Write("+", Color.DarkGreen);
                                Console.Write("] ");
                                Console.WriteLine(Proxies.Value, Color.White);
                            }
                        }
                        catch (Exception)
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
