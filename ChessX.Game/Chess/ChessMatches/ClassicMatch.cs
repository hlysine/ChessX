using System.Linq;
using ChessX.Game.Chess.ChessPieces;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.ChessMatches
{
    public class ClassicMatch : ChessMatch
    {
        public override Vector2I BoardSize => DEFAULT_BOARD_SIZE;

        public override void Initialize()
        {
            ChessPieces.Clear();
            ChessPieces.AddRange(new ChessPiece[]
            {
                new RookPiece(ChessColor.Black) { X = 0, Y = 0 },
                new KnightPiece(ChessColor.Black) { X = 1, Y = 0 },
                new BishopPiece(ChessColor.Black) { X = 2, Y = 0 },
                new QueenPiece(ChessColor.Black) { X = 3, Y = 0 },
                new KingPiece(ChessColor.Black) { X = 4, Y = 0 },
                new BishopPiece(ChessColor.Black) { X = 5, Y = 0 },
                new KnightPiece(ChessColor.Black) { X = 6, Y = 0 },
                new RookPiece(ChessColor.Black) { X = 7, Y = 0 },
            });
            ChessPieces.AddRange(Enumerable.Range(0, 8).Select(i => new PawnPiece(ChessColor.Black) { X = i, Y = 1 }));

            ChessPieces.AddRange(new ChessPiece[]
            {
                new RookPiece(ChessColor.White) { X = 0, Y = 7 },
                new KnightPiece(ChessColor.White) { X = 1, Y = 7 },
                new BishopPiece(ChessColor.White) { X = 2, Y = 7 },
                new QueenPiece(ChessColor.White) { X = 3, Y = 7 },
                new KingPiece(ChessColor.White) { X = 4, Y = 7 },
                new BishopPiece(ChessColor.White) { X = 5, Y = 7 },
                new KnightPiece(ChessColor.White) { X = 6, Y = 7 },
                new RookPiece(ChessColor.White) { X = 7, Y = 7 },
            });
            ChessPieces.AddRange(Enumerable.Range(0, 8).Select(i => new PawnPiece(ChessColor.White) { X = i, Y = 6 }));
        }
    }
}
