using System;

namespace Zadania_4_5
{
    public sealed class Logger
    {
        private static Logger instance;
        private static readonly object lockObj = new object();
        private string _value;
        public string Value { get => _value; set => _value = value; } 
        private Logger() { }
        public static Logger Instance(string value)
        {
            if(instance == null)
            {
                lock (lockObj)
                {
                    if(instance == null)
                    {
                        instance = new Logger();
                        instance.Value = value;
                    }
                }
            }
               
            return instance;
        }
        public void LoggActivity(string log)
        {
            Console.WriteLine($"* {log}");
        }


        static void Main(string[] args)
        {
            Thread p1 = new Thread(() =>
            {
                Logger logger = Logger.Instance("testowa");
                Console.WriteLine($"{logger.Value}");
            });

            Thread p2 = new Thread(() =>
            {
                Logger logger2 = Logger.Instance("To jest ta druga");
                Console.WriteLine($"{logger2.Value}");
            });

            p1.Start();
            p2.Start();

            p1.Join();
            p2.Join();

        }
    }
}
