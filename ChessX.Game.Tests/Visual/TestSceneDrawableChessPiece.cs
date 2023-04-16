using System;
using System.Linq;
using ChessX.Game.Rulesets;
using ChessX.Game.Rulesets.Chess;
using ChessX.Game.Rulesets.Chess.UI;
using ChessX.Game.Rulesets.UI;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace ChessX.Game.Tests.Visual
{
    public partial class TestSceneDrawableChessPiece : ChessXTestScene
    {
        [BackgroundDependencyLoader]
        private void load(Bindable<Ruleset> ruleset)
        {
            Add(new Box
            {
                Colour = Colour4.Gray,
                RelativeSizeAxes = Axes.Both
            });

            var match = (ChessMatch)ruleset.Value.CreateMatch();
            Add(new BoardScalingContainer
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Child = new FillFlowContainer
                {
                    Direction = FillDirection.Full,
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(0.5f),
                    Children = Enum.GetValues(typeof(ChessPieceType)).Cast<ChessPieceType>().SelectMany(p => new[]
                    {
                        new DrawableChessPiece(match.CreatePiece(p, ChessColor.Black)),
                        new DrawableChessPiece(match.CreatePiece(p, ChessColor.White))
                    }).ToList()
                }
            });
        }
    }
}
