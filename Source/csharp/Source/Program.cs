using System;

namespace RozenMaiden
{
    public static class Program
    {
        public static Main Game;
        [STAThread]
        public static void Main()
        {
            using (Game = new Main())
            {
                Game.Run();
            }
        }
    }
}