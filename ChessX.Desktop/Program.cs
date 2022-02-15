using osu.Framework.Platform;
using osu.Framework;
using ChessX.Game;

namespace ChessX.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"ChessX"))
            using (osu.Framework.Game game = new ChessXGame())
                host.Run(game);
        }
    }
}
