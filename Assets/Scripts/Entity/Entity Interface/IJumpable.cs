public interface IJumpable
{
    public float MaxJumpHeight { get; }
    public float MaxJumpTime { get; }
    public float InitialJumpVelocity { get; set; }
}