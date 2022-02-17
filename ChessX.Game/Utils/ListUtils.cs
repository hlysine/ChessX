using System.Collections.Generic;
using System.Linq;
using osu.Framework.Utils;

namespace ChessX.Game.Utils
{
    public static class ListUtils
    {
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            var list = source.ToList();
            return list.ElementAt(RNG.Next(list.Count));
        }
    }
}
