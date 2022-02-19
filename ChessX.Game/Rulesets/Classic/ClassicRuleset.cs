using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Classic
{
    public class ClassicRuleset : Ruleset
    {
        public override string Name => "Classic";

        public override ChessMatch CreateChessMatch() => new ClassicMatch();

        public override DrawableRuleset CreateDrawableRuleset(ChessMatch match) => new DrawableClassicRuleset(match);
    }
}
