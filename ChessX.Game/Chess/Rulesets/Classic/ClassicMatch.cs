using System;
using System.Linq;
using System.Threading.Tasks;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Moves;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class ClassicMatch : ChessMatch
    {
        public override ChessPiece CreateChessPiece(ChessPieceType pieceType, ChessColor color) => pieceType switch
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

        private void executeMove(Move move)
        {
            var instructions = move.GetInstructions(this).ToList();

            foreach (var instruction in instructions)
            {
                instruction.Execute(this);
            }

            MoveHistory.Add(move);
        }

        public override bool MatchEnded => false;
    }
}
