namespace ChessX.Game.UserInterface
{
    /// <summary>
    /// Used for exposing rotation to children via DI so that children can apply counter-rotation.
    /// </summary>
    public interface IRotatable
    {
        public float Rotation { get; set; }
    }
}
