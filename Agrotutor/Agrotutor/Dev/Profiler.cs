﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Agrotutor.Dev
{
    public static class Profiler
    {
        static readonly ConcurrentDictionary<string, Stopwatch> watches = new ConcurrentDictionary<string, Stopwatch>();

        public static void Start(object view)
        {
            Start(view.GetType().Name);
        }

        public static void Start(string tag)
        {
#if DEBUG
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------", tag);
            Console.WriteLine("Starting Stopwatch {0}", tag);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------", tag);

            var watch =
                watches[tag] = new Stopwatch();
            watch.Start();
#endif
        }

        public static void Stop(string tag)
        {
#if DEBUG
            Stopwatch watch;
            if (watches.TryGetValue(tag, out watch))
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------", tag);
                Console.WriteLine("Stop Stopwatch {0} took {1}", tag, watch.Elapsed);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------", tag);
            }
#endif
        }
    }
}