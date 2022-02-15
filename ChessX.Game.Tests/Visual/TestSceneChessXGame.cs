using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace ChessX.Game.Tests.Visual
{
    public class TestSceneChessXGame : ChessXTestScene
    {
        private ChessXGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new ChessXGame();
            game.SetHost(host);

            AddGame(game);
        }
    }
}
