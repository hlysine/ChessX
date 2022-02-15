using System;
using System.Collections.Specialized;
using System.Linq;
using ChessX.Game.Chess.ChessMatches;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    [Cached(typeof(IRotatable))]
    public class DrawableChessMatch : Container, IRotatable
    {
        [Cached(typeof(IHasBoardSize))]
        public ChessMatch ChessMatch { get; }

        private readonly Checkerboard checkerboard;
        private readonly Container chessPieceContainer;
        private readonly BindableList<ChessPiece> chessPieces = new BindableList<ChessPiece>();

        public DrawableChessMatch(ChessMatch match)
        {
            ChessMatch = match;

            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Scale = new Vector2(1 / MathF.Sqrt(2));

            AddRange(new Drawable[]
            {
                new ChessGridContainer
                {
                    AlignToTileCenter = false,
                    Child = checkerboard = new Checkerboard
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    }
                },
                chessPieceContainer = new ChessGridContainer
                {
                    AlignToTileCenter = true
                }
            });

            chessPieces.BindTo(ChessMatch.ChessPieces);
            chessPieces.BindCollectionChanged((sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        chessPieceContainer.AddRange(e.NewItems.Cast<ChessPiece>().Select(p => new DrawableChessPiece(p)));
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        chessPieceContainer.RemoveAll(p =>
                        {
                            if (p is DrawableChessPiece piece)
                            {
                                return e.OldItems.Contains(piece.ChessPiece);
                            }

                            return false;
                        });
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        chessPieceContainer.RemoveAll(p =>
                        {
                            if (p is DrawableChessPiece piece)
                            {
                                return e.OldItems.Contains(piece.ChessPiece);
                            }

                            return false;
                        });
                        chessPieceContainer.AddRange(e.NewItems.Cast<ChessPiece>().Select(p => new DrawableChessPiece(p)));
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        chessPieceContainer.RemoveAll(p => p is DrawableChessPiece);
                        chessPieceContainer.AddRange(chessPieces.Select(p => new DrawableChessPiece(p)));
                        break;
                }
            }, true);
        }
    }
}
