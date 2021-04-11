using Zenject;

public class ControlBlocksAdjuster
{
    private CollisionDetection CollisionDetection { get; }

    [Inject]
    public ControlBlocksAdjuster(CollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public ControlBlocks AdjustBlocksForSideMove(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return currentControlBlocks;
        }
        return newControlBlocks;
    }
    public ControlBlocks AdjustBlocksForDownMove(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if (CollisionDetection.IsCollisionPutPosition(newControlBlocks))
        {
            currentControlBlocks.SetTruePutable();
            return currentControlBlocks;
        }
        return newControlBlocks;
    }
}
