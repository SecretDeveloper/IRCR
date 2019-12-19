using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ircr
{
    internal class IRCMessage
    {
        public string Source { get; set; }
        public string Prefix { get; set; }
        public string Command { get; set; }
        public string[] Parameters { get; set; }

        public static IRCMessage ParseMessage(string input)
        {
            var arr = input.Split(" ");

            var m = new IRCMessage();
            m.Source = input;

            m.Prefix = "";
            m.Command = "";

            var idx = 0;
            if(arr[idx].StartsWith(":"))
            {
                m.Prefix = arr[idx].Replace(":", "");
                idx++;
            }

            // TODO: clean up parsing here
            if(arr[idx].StartsWith("/"))
            {
                m.Command = arr[idx].Replace("/","");
            }

            m.Parameters = arr.Skip(idx).Take(arr.Length - idx).ToArray();

            return m;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("IRCr");
                Console.WriteLine("A simple IRC client");

                var input = "";
                while (true)
                {
                    Console.Write(">");
                    input = Console.ReadLine();

                    if(input.Equals("quit")) break;

                    ProcessInput(input);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void ProcessInput(string input)
        {
            var message = ParseMessage(input);

            switch ((message.Command).ToLower())
            {
                case "":
                    Send(message.Source);
                    break;
                case "connect":
                    Connect(message);
                    break;
                case "help":
                case "kick":
                case "mode":
                case "invite":
                case "topic":
                    PrintHelp();
                    break;
                default:
                    PrintHelp(input);
                    break;
            }
        }

        private static void Send(string source)
        {
            throw new NotImplementedException();
        }

        private static void Connect(IRCMessage message)
        {
            throw new NotImplementedException();
        }

        private static void PrintHelp(string input ="")
        {
            Console.WriteLine(@"IRCr
            A simple IRC client

            Commands:
            help        Displays help screen.
            version     Displays version information
            join [channel]        Joins an IRC channel");
        }
    }
}
