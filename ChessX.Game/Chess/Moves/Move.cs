using System.Collections.Generic;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public abstract class Move
    {
        public Piece Piece { get; }

        public Vector2I OriginalPosition { get; }

        public Vector2I TargetPosition { get; }

        public virtual bool CanCapture => false;

        protected Move(Piece piece, Vector2I targetPosition)
        {
            Piece = piece;
            OriginalPosition = piece.Position;
            TargetPosition = targetPosition;
        }

        /// <summary>
        /// Expand this <see cref="Move"/> into <see cref="Instruction"/>s.
        /// <remarks>The returned enumerable must be completely enumerated before any of the instructions are executed.</remarks>
        /// </summary>
        /// <param name="match">The state of the match before this move is made.</param>
        /// <returns>A enumeration of <see cref="Instruction"/>s.</returns>
        public abstract IEnumerable<Instruction> GetInstructions(Match match);
    }
}
