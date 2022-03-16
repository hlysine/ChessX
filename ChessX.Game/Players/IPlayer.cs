#nullable enable

using System;
using System.Threading.Tasks;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Players
{
    public interface IPlayer
    {
        IMatch? Match { get; }

        Move? SelectedMove { get; }

        bool IsInTurn { get; set; }

        bool RotateBoardInTurn { get; }

        float TargetBoardRotation { get; }

        void StartTurn();

        void EndTurn();

        Task WaitForTurnEnd();
    }

    public interface IPlayer<TPiece> : IPlayer where TPiece : Piece
    {
        IMatch? IPlayer.Match => Match;
        new Match<TPiece>? Match { get; set; }

        Move? IPlayer.SelectedMove => SelectedMove;
        new Move<TPiece>? SelectedMove { get; }

        delegate void TurnStartHandler(Action<Move<TPiece>> selectMove);

        event TurnStartHandler? TurnStarted;

        event Action? TurnEnded;
    }
}
