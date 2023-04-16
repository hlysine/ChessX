using osu.Framework.Testing;

namespace ChessX.Game.Tests.Visual
{
    public partial class ChessXTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new ChessXTestSceneTestRunner();

        private partial class ChessXTestSceneTestRunner : ChessXGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
