using Zenject;

public class ControlBlocksAdjuster
{
    private CollisionDetection CollisionDetection { get; }

    [Inject]
    public ControlBlocksAdjuster(CollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public ControlBlocks AdjustBlocksForMove(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return currentControlBlocks;
        }
        return newControlBlocks;
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
