using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using System.IO;

namespace Proxiessourcecode
{
    class DiscordRPC
    {
        public static void RPC()
        {
            int count = File.ReadAllLines("Sources.txt").Length;
            try
            {
                discordRpcClient = new DiscordRpcClient("864602310466207794");
                discordRpcClient.Initialize();
                discordRpcClient.SetPresence(new RichPresence
                {
                    Details = "Proxies - Created by Vanix#9999",
                    State = Startup.checker.ToLower().Replace("on", "Scraping and checking..").Replace("off", "Scraping...").ToString(),
                    Timestamps = Timestamps.Now,
                    Assets = new Assets
                    {
                        LargeImageKey = "logo",
                        LargeImageText = Program.type.Replace("http", "Checking: HTTPS").Replace("socks4", "Checking: SOCKS4").Replace("socks5", "Checking: SOCKS5"),
                        SmallImageKey = "logo",
                        SmallImageText = "Url: " + count,
                    }
                });
            }
            catch (Exception)
            {
                Console.WriteLine("[DISCORD-RPC] FAILED TO START");
            }
        
        }

        public static DiscordRpcClient discordRpcClient;
    }
}