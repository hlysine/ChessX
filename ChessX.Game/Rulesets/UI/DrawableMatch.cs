using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ChessX.Game.Chess;
using ChessX.Game.Graphics;
using JetBrains.Annotations;
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
        [Cached(typeof(IMatch))]
        [Cached(typeof(IHasBoardSize))]
        public IMatch Match { get; }

        [Cached]
        public PieceContainer PieceContainer { get; } = new();

        public Container Underlays { get; } = new() { RelativeSizeAxes = Axes.Both };
        public Container Overlays { get; } = new() { RelativeSizeAxes = Axes.Both };

        protected DrawableMatch(IMatch match)
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
                    new BoardScalingContainer
                    {
                        ChildrenEnumerable = new[]
                        {
                            CreateGameBoard()
                        }.Where(c => c != null)
                    },
                    new BoardScalingContainer
                    {
                        ChildrenEnumerable = new[]
                        {
                            Underlays,
                            PieceContainer,
                            Overlays
                        }.Where(c => c != null)
                    }
                }
            });
        }

        /// <summary>
        /// The bottom-most drawable displayed in the drawable match, usually a game board.
        /// <remarks>This game board can be interactive by reporting inputs to a <see cref="GridInputRedirector"/> retrieved via DI.</remarks>
        /// </summary>
        /// <returns>The game board, or null if this isn't required.</returns>
        [CanBeNull]
        protected abstract Drawable CreateGameBoard();
    }

    public abstract class DrawableMatch<TPiece> : DrawableMatch where TPiece : Piece
    {
        public new Match<TPiece> Match { get; }

        private readonly BindableList<TPiece> pieces = new();

        protected DrawableMatch(Match<TPiece> match)
            : base(match)
        {
            Match = match;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            pieces.BindTo(Match.Pieces);
            pieces.BindCollectionChanged((sender, e) => Schedule(() => OnChessPiecesChanged(sender, e)), true);
            FinishTransforms(true);

            foreach (var player in Match.Players)
            {
                player.TurnStarted += _ =>
                {
                    if (player.RotateBoardInTurn)
                        Schedule(() => this.RotateTo(player.TargetBoardRotation, 200, Easing.InOutQuint));
                };
            }
        }

        protected virtual void OnChessPiecesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    addRange(e.NewItems.Cast<TPiece>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    removeRange(e.OldItems.Cast<TPiece>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    removeRange(e.OldItems.Cast<TPiece>());
                    addRange(e.NewItems.Cast<TPiece>());
                    break;

                case NotifyCollectionChangedAction.Reset:
                    PieceContainer.RemoveAll(p => p is DrawablePiece, true);
                    PieceContainer.AddRange(pieces.Select(CreateDrawableRepresentation));
                    break;
            }
        }

        private void addRange(IEnumerable<TPiece> newItems)
        {
            var newDrawables = newItems.Select(CreateDrawableRepresentation).ToList();
            newDrawables.ForEach(d => d.MoveTo(d.Piece.Position).FadeInFromZero(200, Easing.InOutQuint));
            PieceContainer.AddRange(newDrawables);
        }

        private void removeRange(IEnumerable<TPiece> oldItems)
        {
            foreach (var child in PieceContainer)
            {
                if (child is DrawablePiece<TPiece> piece)
                {
                    if (oldItems.Contains(piece.Piece))
                        piece.FadeOut(200, Easing.InOutQuint).Expire();
                }
            }
        }

        protected abstract DrawablePiece<TPiece> CreateDrawableRepresentation(TPiece piece);
    }
}
