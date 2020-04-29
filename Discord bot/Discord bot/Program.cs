using System;

namespace Discord_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot1();
            bot.RunAsync().GetAwaiter().GetResult();

        }
    }
}
