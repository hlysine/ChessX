using System;

namespace ChessX.Game.Chess.Utils
{
    public static class MathUtils
    {
        public static bool InBetween(int value, int bound1, int bound2) => value > Math.Min(bound1, bound2) && value < Math.Max(bound1, bound2);
    }
}
