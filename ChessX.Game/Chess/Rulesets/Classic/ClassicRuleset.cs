using System;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Drawables;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class ClassicRuleset : Ruleset
    {
        public override string Name => "Classic";

        public override ChessMatch CreateChessMatch() => new ClassicMatch(this);

        public override DrawableChessMatch CreateDrawableChessMatch(ChessMatch match) => new DrawableClassicMatch(match);

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
    }
}
