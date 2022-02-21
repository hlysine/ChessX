using System;

namespace ChessX.Game.Rulesets.Chess
{
    public enum ChessColor
    {
        White,
        Black
    }

    public static class ChessColorExtensions
    {
        public static ChessColor GetOpposingColor(this ChessColor color) => color switch
        {
            ChessColor.Black => ChessColor.White,
            ChessColor.White => ChessColor.Black,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

        public static bool IsOppositeOf(this ChessColor color1, ChessColor color2) => color1 == color2.GetOpposingColor();
    }
}
