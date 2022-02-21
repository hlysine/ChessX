using System.Collections.Generic;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Chess.Moves.Instructions;
using ChessX.Game.Rulesets.Chess.Pieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess.Moves
{
    public abstract class ChessMove : Move<ChessPiece>
    {
        protected ChessMove(ChessPiece piece, Vector2I targetPosition)
            : base(piece, targetPosition)
        {
        }

        public override IEnumerable<Instruction<ChessPiece>> GetInstructions(Match<ChessPiece> match)
        {
            return GetInstructions((ChessMatch)match);
        }

        public abstract IEnumerable<Instruction<ChessPiece>> GetInstructions(ChessMatch match);
    }
}
