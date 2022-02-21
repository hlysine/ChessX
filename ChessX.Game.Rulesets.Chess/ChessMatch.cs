using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Players;
using ChessX.Game.Rulesets.Chess.Pieces;
using ChessX.Game.Rulesets.Chess.Players;
using JetBrains.Annotations;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Rulesets.Chess
{
    public class ChessMatch : Match<ChessPiece>
    {
        public new IEnumerable<ChessPlayer> Players => base.Players.Cast<ChessPlayer>();

        public override void Initialize()
        {
            Pieces.Clear();

            Pieces.AddRange(new[]
            {
                CreatePiece(ChessPieceType.Rook, ChessColor.Black).WithPosition(0, 0),
                CreatePiece(ChessPieceType.Knight, ChessColor.Black).WithPosition(1, 0),
                CreatePiece(ChessPieceType.Bishop, ChessColor.Black).WithPosition(2, 0),
                CreatePiece(ChessPieceType.Queen, ChessColor.Black).WithPosition(3, 0),
                CreatePiece(ChessPieceType.King, ChessColor.Black).WithPosition(4, 0),
                CreatePiece(ChessPieceType.Bishop, ChessColor.Black).WithPosition(5, 0),
                CreatePiece(ChessPieceType.Knight, ChessColor.Black).WithPosition(6, 0),
                CreatePiece(ChessPieceType.Rook, ChessColor.Black).WithPosition(7, 0)
            });
            Pieces.AddRange(Enumerable.Range(0, 8).Select(i => CreatePiece(ChessPieceType.Pawn, ChessColor.Black).WithPosition(i, 1)));

            Pieces.AddRange(new[]
            {
                CreatePiece(ChessPieceType.Rook, ChessColor.White).WithPosition(0, 7),
                CreatePiece(ChessPieceType.Knight, ChessColor.White).WithPosition(1, 7),
                CreatePiece(ChessPieceType.Bishop, ChessColor.White).WithPosition(2, 7),
                CreatePiece(ChessPieceType.Queen, ChessColor.White).WithPosition(3, 7),
                CreatePiece(ChessPieceType.King, ChessColor.White).WithPosition(4, 7),
                CreatePiece(ChessPieceType.Bishop, ChessColor.White).WithPosition(5, 7),
                CreatePiece(ChessPieceType.Knight, ChessColor.White).WithPosition(6, 7),
                CreatePiece(ChessPieceType.Rook, ChessColor.White).WithPosition(7, 7)
            });
            Pieces.AddRange(Enumerable.Range(0, 8).Select(i => CreatePiece(ChessPieceType.Pawn, ChessColor.White).WithPosition(i, 6)));
        }

        public ChessPiece CreatePiece(ChessPieceType pieceType, ChessColor color) => pieceType switch
        {
            ChessPieceType.King => new KingPiece(color),
            ChessPieceType.Queen => new QueenPiece(color),
            ChessPieceType.Bishop => new BishopPiece(color),
            ChessPieceType.Knight => new KnightPiece(color),
            ChessPieceType.Rook => new RookPiece(color),
            ChessPieceType.Pawn => new PawnPiece(color),
            _ => throw new ArgumentOutOfRangeException(nameof(pieceType), pieceType, null)
        };

        public override async Task ProcessRound()
        {
            WhitePlayer.StartTurn();
            await WhitePlayer.WaitForTurnEnd();
            executeMove(WhitePlayer.SelectedMove);
            await Task.Delay(500);

            BlackPlayer.StartTurn();
            await BlackPlayer.WaitForTurnEnd();
            executeMove(BlackPlayer.SelectedMove);
            await Task.Delay(500);
        }

        private void executeMove(Move<ChessPiece> move)
        {
            var instructions = move.GetInstructions(this).ToList();

            foreach (var instruction in instructions)
            {
                instruction.Execute(this);
            }

            MoveHistory.Add(move);
        }

        public override bool MatchEnded => false;

        [NotNull]
        public ChessPlayer GetPlayerWithColor(ChessColor color)
        {
            return Players.First(p => p.Color == color);
        }

        public Player<ChessPiece> WhitePlayer => GetPlayerWithColor(ChessColor.White);

        public Player<ChessPiece> BlackPlayer => GetPlayerWithColor(ChessColor.Black);

        [NotNull]
        public ChessPiece GetKingPiece(ChessColor color)
        {
            return Pieces.First(p => p.Color == color && p.PieceType == ChessPieceType.King);
        }

        public bool PositionCapturable(Vector2I position, ChessColor capturerColor)
        {
            return Pieces.Where(p => p.Color == capturerColor)
                         .Any(p => p.GetAllowedMoves(this, true).Any(m => m.CanCapture && m.TargetPosition == position));
        }

        public bool IsInCheck(ChessColor color)
        {
            return PositionCapturable(GetKingPiece(color).Position, color.GetOpposingColor());
        }

        public bool HasMovedSinceStart(ChessPiece piece)
        {
            return MoveHistory.Any(h => h.Piece == piece && h.TargetPosition != h.OriginalPosition);
        }

        public bool JustMovedTwoSteps(ChessPiece piece)
        {
            var lastMove = MoveHistory.LastOrDefault(h => h.Piece.Color == piece.Color);

            if (lastMove == null) return false;

            return lastMove.Piece == piece && Math.Abs(lastMove.TargetPosition.Y - lastMove.OriginalPosition.Y) == 2;
        }
    }
}
