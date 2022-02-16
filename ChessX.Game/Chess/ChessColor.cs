using System;

namespace ChessX.Game.Chess
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
    }
}
