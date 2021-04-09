using Zenject;

public interface IControlBlocksAdjuster
{
    void AdjustBlocksForMoveRight(IControlBlocks controlBlocks);
    void AdjustBlocksForMoveLeft(IControlBlocks controlBlocks);
    void AdjustBlocksForMoveDown(IControlBlocks controlBlocks);

}
public class ControlBlocksAdjuster : IControlBlocksAdjuster
{
    private ICollisionDetection CollisionDetection { get; }

    [Inject]
    public ControlBlocksAdjuster(ICollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public void AdjustBlocksForMoveRight(IControlBlocks controlBlocks)
    {
        while (true)
        {
            if (CollisionDetection.IsCollision(controlBlocks))
            {
                controlBlocks.MoveLeft();
                continue;
            }

            break;
        }
    }

    public void AdjustBlocksForMoveLeft(IControlBlocks controlBlocks)
    {
        while (true)
        {
            if (CollisionDetection.IsCollision(controlBlocks))
            {
                controlBlocks.MoveRight();
                continue;
            }

            break;
        }
    }

    public void AdjustBlocksForMoveDown(IControlBlocks controlBlocks)
    {
        while (true)
        {
            if (CollisionDetection.IsCollision(controlBlocks))
            {
                controlBlocks.MoveUp();
                continue;
            }

            break;
        }
    }
}
