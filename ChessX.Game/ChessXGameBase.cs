using System.Linq;
using ChessX.Game.Graphics;
using ChessX.Game.Rulesets;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using ChessX.Resources;
using osu.Framework.Bindables;

namespace ChessX.Game
{
    public partial class ChessXGameBase : osu.Framework.Game
    {
        // Anything in this class is shared between the test browser and the game implementation.
        // It allows for caching global dependencies that should be accessible to tests, or changing
        // the screen scaling for all components including the test browser and framework overlays.

        protected override Container<Drawable> Content { get; }

        [Cached]
        protected readonly RulesetStore RulesetStore;

        [Cached]
        [Cached(typeof(IBindable<Ruleset>))]
        protected readonly Bindable<Ruleset> Ruleset;

        [Cached]
        public readonly ChessXColor ChessXColor = new();

        protected ChessXGameBase()
        {
            // Ensure game and tests scale with window size and screen DPI.
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                // You may want to change TargetDrawSize to your "default" resolution, which will decide how things scale and position when using absolute coordinates.
                TargetDrawSize = new Vector2(1920, 1080)
            });

            RulesetStore = new RulesetStore();
            Ruleset = new Bindable<Ruleset>(RulesetStore.Rulesets.First().CreateInstance());
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(ChessXResources).Assembly));
        }
    }
}
