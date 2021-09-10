using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace Proxiessourcecode
{
    class Design
    {
        public static void Logo()
        {
            Console.WriteLine(@"


██████╗ ██████╗  ██████╗ ██╗  ██╗██╗███████╗███████╗
██╔══██╗██╔══██╗██╔═══██╗╚██╗██╔╝██║██╔════╝██╔════╝
██████╔╝██████╔╝██║   ██║ ╚███╔╝ ██║█████╗  ███████╗
██╔═══╝ ██╔══██╗██║   ██║ ██╔██╗ ██║██╔══╝  ╚════██║
██║     ██║  ██║╚██████╔╝██╔╝ ██╗██║███████╗███████║
╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝╚═╝╚══════╝╚══════╝
                                                    
                                       
", Color.FromName(Startup.color));
            Console.Write("Created by [");
            Console.Write("Vanix#9999", Color.RosyBrown);
            Console.Write("] ");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}