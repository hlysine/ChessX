using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;

namespace ChessX.Game.Chess.Drawables
{
    [Cached]
    public class GridInputRedirector : Container
    {
        public bool SendClick(Vector2I position)
        {
            foreach (var receiver in findInputReceivers(this))
            {
                if (receiver.GridPosition == position)
                {
                    if (receiver.TriggerClick())
                        return true;
                }
            }

            return false;
        }

        private IEnumerable<IReceiveGridInput> findInputReceivers(Container root)
        {
            for (var i = root.Count - 1; i >= 0; i--)
            {
                var child = root[i];

                if (child is IReceiveGridInput receiver)
                    yield return receiver;
                else if (child is Container container)
                {
                    foreach (var n in findInputReceivers(container))
                        yield return n;
                }
            }
        }
    }

    public interface IReceiveGridInput
    {
        public Vector2I GridPosition { get; }

        public bool TriggerClick();
    }
}
