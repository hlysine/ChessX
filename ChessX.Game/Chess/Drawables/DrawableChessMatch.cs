using System;
using ChessX.Game.Chess.ChessPieces;
using ChessX.Game.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace ChessX.Game.Chess.Drawables
{
    [Cached(typeof(IRotatable))]
    public class DrawableChessMatch : Container, IRotatable
    {
        public DrawableChessMatch()
        {
            Rotation = 45;
            RelativeSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Scale = new Vector2(1 / MathF.Sqrt(2));
            AddRange(new Drawable[]
            {
                new ChessGridContainer
                {
                    AlignToTileCenter = false,
                    Child = new Checkerboard
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    }
                },
                new ChessGridContainer
                {
                    AlignToTileCenter = true,
                    Children = new Drawable[]
                    {
                        new DrawableChessPiece(new KingPiece(ChessColor.White)),
                        new DrawableChessPiece(new KingPiece(ChessColor.Black))
                        {
                            X = 4,
                            Y = 7
                        }
                    }
                }
            });
        }
    }
}
