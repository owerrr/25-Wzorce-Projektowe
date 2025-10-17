using System;

namespace Zadania_4_5
{
    public sealed class Logger
    {
        private static Logger instance = null;
        private Logger() { }
        public static Logger Instance()
        {
            if(instance == null)
            {
                instance = new Logger();
            }
               
            return instance;
        }
        public void LoggActivity(string log)
        {
            Console.WriteLine($"* {log}");
        }


        static void Main(string[] args)
        {
            Logger logger = Logger.Instance();
            Logger logger2 = Logger.Instance();
            
            logger.LoggActivity("hej");
            logger2.LoggActivity("hej");

            if(logger == logger2)
            {
                logger.LoggActivity("działa");
            }
        }
    }
}
