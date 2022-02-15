using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }

        public ChessPiece ChessPiece { get; set; }

        public Vector2I TargetPosition { get; set; }

        public abstract IEnumerable<Instruction> GetInstructions();
    }
}
