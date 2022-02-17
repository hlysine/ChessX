using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Rulesets.Classic
{
    public class ClassicMatch : ChessMatch
    {
        public override Vector2I BoardSize => DEFAULT_BOARD_SIZE;

        public ClassicMatch(Ruleset ruleset)
            : base(ruleset)
        {
        }
    }
}
