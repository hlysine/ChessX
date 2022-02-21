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
    public abstract class DrawableMatch : Container, IRotatable
    {
        [Cached(typeof(Match))]
        [Cached(typeof(IHasBoardSize))]
        public Match Match { get; }

        public Container Underlays { get; } = new Container { RelativeSizeAxes = Axes.Both };

        [Cached]
        public ChessPieceContainer ChessPieceContainer { get; } = new ChessPieceContainer();

        public Container Overlays { get; } = new Container { RelativeSizeAxes = Axes.Both };

        private readonly BindableList<Piece> chessPieces = new BindableList<Piece>();

        protected DrawableMatch(Match match)
        {
            Match = match;

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

            chessPieces.BindTo(Match.Pieces);
            chessPieces.BindCollectionChanged((sender, e) => Schedule(() => OnChessPiecesChanged(sender, e)), true);
            FinishTransforms(true);

            foreach (var player in Match.Players)
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
                    addRange(e.NewItems.Cast<Piece>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    removeRange(e.OldItems.Cast<Piece>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    removeRange(e.OldItems.Cast<Piece>());
                    addRange(e.NewItems.Cast<Piece>());
                    break;

                case NotifyCollectionChangedAction.Reset:
                    ChessPieceContainer.RemoveAll(p => p is DrawablePiece);
                    ChessPieceContainer.AddRange(chessPieces.Select(CreateDrawableRepresentation));
                    break;
            }
        }

        private void addRange(IEnumerable<Piece> newItems)
        {
            var newDrawables = newItems.Select(CreateDrawableRepresentation).ToList();
            newDrawables.ForEach(d => d.MoveTo(d.Piece.Position).FadeInFromZero(200, Easing.InOutQuint));
            ChessPieceContainer.AddRange(newDrawables);
        }

        private void removeRange(IEnumerable<Piece> oldItems)
        {
            foreach (var child in ChessPieceContainer)
            {
                if (child is DrawablePiece piece)
                {
                    if (oldItems.Contains(piece.Piece))
                        piece.FadeOut(200, Easing.InOutQuint).Expire();
                }
            }
        }

        protected abstract DrawablePiece CreateDrawableRepresentation(Piece piece);
    }
}
