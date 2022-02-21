using ChessX.Game.Chess;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Chess
{
    public class ChessRuleset : Ruleset
    {
        public override string Name => "Chess";

        public override Match CreateMatch() => new ChessMatch();

        public override DrawableRuleset CreateDrawableRuleset(Match match) => new DrawableChessRuleset(match);
    }
}
