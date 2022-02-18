using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
    public abstract class DrawableChessMatch : Container, IRotatable
    {
        [Cached(typeof(IHasBoardSize))]
        public ChessMatch ChessMatch { get; }

        private readonly Checkerboard checkerboard;
        private readonly Container chessPieceContainer;
        private readonly BindableList<ChessPiece> chessPieces = new BindableList<ChessPiece>();

        protected DrawableChessMatch(ChessMatch match)
        {
            ChessMatch = match;

            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Scale = new Vector2(1 / MathF.Sqrt(2));

            Add(new GridInputRedirector
            {
                RelativeSizeAxes = Axes.Both,
                Children = new[]
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
                    new ChessGridContainer
                    {
                        AlignToTileCenter = true,
                        Children = new[]
                        {
                            chessPieceContainer = new Container
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        }
                    }
                }
            });

            chessPieces.BindTo(ChessMatch.ChessPieces);
            chessPieces.BindCollectionChanged((sender, e) => Schedule(() => OnChessPiecesChanged(sender, e)), true);
            FinishTransforms(true);
        }

        protected virtual void OnChessPiecesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    addRange(e.NewItems.Cast<ChessPiece>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    removeRange(e.OldItems.Cast<ChessPiece>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    removeRange(e.OldItems.Cast<ChessPiece>());
                    addRange(e.NewItems.Cast<ChessPiece>());
                    break;

                case NotifyCollectionChangedAction.Reset:
                    chessPieceContainer.RemoveAll(p => p is DrawableChessPiece);
                    chessPieceContainer.AddRange(chessPieces.Select(CreateDrawableRepresentation));
                    break;
            }
        }

        private void addRange(IEnumerable<ChessPiece> newItems)
        {
            chessPieceContainer.AddRange(newItems.Select(CreateDrawableRepresentation));
        }

        private void removeRange(IEnumerable<ChessPiece> oldItems)
        {
            chessPieceContainer.RemoveAll(p =>
            {
                if (p is DrawableChessPiece piece)
                {
                    return oldItems.Contains(piece.ChessPiece);
                }

                return false;
            });
        }

        protected abstract DrawableChessPiece CreateDrawableRepresentation(ChessPiece chessPiece);
    }
}
