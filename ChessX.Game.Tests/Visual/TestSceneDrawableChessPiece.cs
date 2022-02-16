using System;
using System.Linq;
using ChessX.Game.Chess;
using ChessX.Game.Chess.Drawables;
using ChessX.Game.Chess.Rulesets;
using ChessX.Game.Chess.Rulesets.Classic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneDrawableChessPiece : ChessXTestScene
    {
        [BackgroundDependencyLoader]
        private void load(Bindable<Ruleset> ruleset)
        {
            Add(new Box
            {
                Colour = Colour4.Gray,
                RelativeSizeAxes = Axes.Both
            });
            Add(new ChessGridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                AlignToTileCenter = true,
                Child = new FillFlowContainer
                {
                    Direction = FillDirection.Full,
                    RelativeSizeAxes = Axes.Both,
                    Children = Enum.GetValues(typeof(ChessPieceType)).Cast<ChessPieceType>().SelectMany(p => new[]
                    {
                        new DrawableClassicPiece(ruleset.Value.CreateChessPiece(p, ChessColor.Black)),
                        new DrawableClassicPiece(ruleset.Value.CreateChessPiece(p, ChessColor.White))
                    }).ToList()
                }
            });
        }
    }
}
