namespace MassiveDangerZone
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MassiveDangerZone game = new MassiveDangerZone())
            {
                game.Run();
            }
        }
    }
#endif
}

