using System;

namespace Fantasy_Wars
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FantasyWars game = new FantasyWars())
            {
                game.Run();
            }
        }
    }
#endif
}

