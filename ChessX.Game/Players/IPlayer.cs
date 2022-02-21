using System;
using System.Threading.Tasks;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using JetBrains.Annotations;

namespace ChessX.Game.Players
{
    public interface IPlayer
    {
        bool IsInTurn { get; set; }

        bool RotateBoardInTurn { get; }

        float TargetBoardRotation { get; }

        void StartTurn();

        void EndTurn();

        Task WaitForTurnEnd();
    }

    public interface IPlayer<TPiece> : IPlayer where TPiece : Piece
    {
        Match<TPiece> Match { get; set; }

        [CanBeNull]
        Move<TPiece> SelectedMove { get; }

        delegate void TurnStartHandler(Action<Move<TPiece>> selectMove);

        event TurnStartHandler TurnStarted;

        event Action TurnEnded;
    }
}
