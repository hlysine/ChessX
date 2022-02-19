using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ChessX.Game.Chess;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace ChessX.Game.Rulesets.UI
{
    [Cached(typeof(IRotatable))]
    public abstract class DrawableChessMatch : Container, IRotatable
    {
        [Cached(typeof(ChessMatch))]
        [Cached(typeof(IHasBoardSize))]
        public ChessMatch ChessMatch { get; }

        public Container Underlays { get; } = new Container { RelativeSizeAxes = Axes.Both };

        [Cached]
        public ChessPieceContainer ChessPieceContainer { get; } = new ChessPieceContainer();

        public Container Overlays { get; } = new Container { RelativeSizeAxes = Axes.Both };

        private readonly BindableList<ChessPiece> chessPieces = new BindableList<ChessPiece>();

        protected DrawableChessMatch(ChessMatch match)
        {
            ChessMatch = match;

            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Scale = new Vector2(1 / MathF.Sqrt(2));
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new GridInputRedirector
            {
                RelativeSizeAxes = Axes.Both,
                Children = new[]
                {
                    new ChessGridContainer
                    {
                        Child = new Checkerboard
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both
                        }
                    },
                    new ChessGridContainer
                    {
                        Children = new[]
                        {
                            Underlays,
                            ChessPieceContainer,
                            Overlays
                        }
                    }
                }
            });

            chessPieces.BindTo(ChessMatch.ChessPieces);
            chessPieces.BindCollectionChanged((sender, e) => Schedule(() => OnChessPiecesChanged(sender, e)), true);
            FinishTransforms(true);

            foreach (var player in ChessMatch.Players)
            {
                player.TurnStarted += _ =>
                {
                    if (player.RotateChessBoard)
                        Schedule(() => this.RotateTo(player.Color == ChessColor.White ? 0 : 180, 200, Easing.InOutQuint));
                };
            }
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
                    ChessPieceContainer.RemoveAll(p => p is DrawableChessPiece);
                    ChessPieceContainer.AddRange(chessPieces.Select(CreateDrawableRepresentation));
                    break;
            }
        }

        private void addRange(IEnumerable<ChessPiece> newItems)
        {
            var newDrawables = newItems.Select(CreateDrawableRepresentation).ToList();
            newDrawables.ForEach(d => d.MoveTo(d.ChessPiece.Position).FadeInFromZero(200, Easing.InOutQuint));
            ChessPieceContainer.AddRange(newDrawables);
        }

        private void removeRange(IEnumerable<ChessPiece> oldItems)
        {
            foreach (var child in ChessPieceContainer)
            {
                if (child is DrawableChessPiece piece)
                {
                    if (oldItems.Contains(piece.ChessPiece))
                        piece.FadeOut(200, Easing.InOutQuint).Expire();
                }
            }
        }

        protected abstract DrawableChessPiece CreateDrawableRepresentation(ChessPiece chessPiece);
    }
}
