﻿using System;

namespace DNSAgent
{
    internal class Logger
    {
        public static readonly System.Diagnostics.TraceSource _trace = new System.Diagnostics.TraceSource("DNSAgent");
        private static readonly object OutputLock = new object();
        private static string _title;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public static string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if (Environment.UserInteractive)
                    Console.Title = value;
            }
        }

        public static void Error(string format, params object[] arg)
        {
            WriteLine(ConsoleColor.Red, format, arg);
        }

        public static void Warning(string format, params object[] arg)
        {
            WriteLine(ConsoleColor.Yellow, format, arg);
        }

        public static void Info(string format, params object[] arg)
        {
            WriteLine(ConsoleColor.Green, format, arg);
        }

        public static void Debug(string format, params object[] arg)
        {
            WriteLine(ConsoleColor.Magenta, format, arg);
        }

        public static void Trace(string format, params object[] arg)
        {
            WriteLine(ConsoleColor.White, format, arg);
        }

        private static void WriteLine(ConsoleColor textColor, string format, params object[] arg)
        {
            if (!Environment.UserInteractive)
            {
                lock (OutputLock)
                {
                    if (arg.Length > 0)
                    {
                        if (arg[0] is Exception)
                            _trace.TraceEvent(System.Diagnostics.TraceEventType.Error, -1, format, arg);
                    }
                }
            }
            else
                lock (OutputLock)
                {
                    Console.ForegroundColor = textColor;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(format, arg);
                    Console.ResetColor();
                }
        }
    }
}