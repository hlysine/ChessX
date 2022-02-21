using System.Collections.Generic;
using ChessX.Game.Chess.Moves.Instructions;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Moves
{
    public abstract class Move
    {
        public Vector2I OriginalPosition { get; }

        public Vector2I TargetPosition { get; }

        public virtual bool CanCapture => false;

        protected Move(Vector2I originalPosition, Vector2I targetPosition)
        {
            OriginalPosition = originalPosition;
            TargetPosition = targetPosition;
        }
    }

    public abstract class Move<TPiece> : Move where TPiece : Piece
    {
        public TPiece Piece { get; }

        protected Move(TPiece piece, Vector2I targetPosition)
            : base(piece.Position, targetPosition)
        {
            Piece = piece;
        }

        /// <summary>
        /// Expand this <see cref="Move{TPiece}"/> into <see cref="Instruction{TPiece}"/>s.
        /// <remarks>The returned enumerable must be completely enumerated before any of the instructions are executed.</remarks>
        /// </summary>
        /// <param name="match">The state of the match before this move is made.</param>
        /// <returns>A enumeration of <see cref="Instruction{TPiece}"/>s.</returns>
        public abstract IEnumerable<Instruction<TPiece>> GetInstructions(Match<TPiece> match);
    }
}
