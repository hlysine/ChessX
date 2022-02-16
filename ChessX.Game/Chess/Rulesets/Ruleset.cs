using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Chess.Drawables;

namespace ChessX.Game.Chess.Rulesets
{
    public abstract class Ruleset
    {
        public abstract string Name { get; }

        public abstract ChessMatch CreateChessMatch();

        public abstract DrawableChessMatch CreateDrawableChessMatch(ChessMatch match);

        public abstract ChessPiece CreateChessPiece(ChessPieceType type, ChessColor color);
    }
}
