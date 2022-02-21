using System;

namespace ChessX.Game.Rulesets
{
    public class RulesetLoadException : Exception
    {
        public RulesetLoadException(string message)
            : base(@$"Ruleset could not be loaded ({message})")
        {
        }
    }
}
