using ChessX.Game.Chess;
using ChessX.Game.Rulesets.Chess.UI;
using ChessX.Game.Rulesets.UI;

namespace ChessX.Game.Rulesets.Chess
{
    public class ChessRuleset : Ruleset
    {
        public override string Name => "Chess";

        public override IMatch CreateMatch() => new ChessMatch();

        public override DrawableRuleset CreateDrawableRuleset(IMatch match) => new DrawableChessRuleset((ChessMatch)match);
    }
}
