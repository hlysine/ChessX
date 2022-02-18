using ChessX.Game.Chess.Drawables;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class ClassicRuleset : Ruleset
    {
        public override string Name => "Classic";

        public override ChessMatch CreateChessMatch() => new ClassicMatch();

        public override DrawableRuleset CreateDrawableRuleset(ChessMatch match) => new DrawableClassicRuleset(match);
    }
}
