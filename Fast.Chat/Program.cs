﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Fast.Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            FastServer.Start();
            Console.ReadLine();
            FastServer.Stop();
        }
    }
}
