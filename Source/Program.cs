using System;

namespace MucciArena
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Loop())
                game.Run();
        }
    }
}
