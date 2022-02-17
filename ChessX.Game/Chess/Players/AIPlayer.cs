using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChessX.Game.Chess.Moves;
using ChessX.Game.Utils;

namespace ChessX.Game.Chess.Players
{
    public class AIPlayer : TimeLimitedPlayer
    {
        protected override TimeSpan GetTimeAllowed() => TimeSpan.FromSeconds(5);

        protected override Task<Move> PerformTimeLimitedMoveAsync(CancellationToken token = default)
        {
            return Task.Run(() => ChessMatch.ChessPieces.Where(p => p.Color == Color).SelectMany(p => p.GetAllowedMoves(ChessMatch)).GetRandom(), token);
        }
    }
}
