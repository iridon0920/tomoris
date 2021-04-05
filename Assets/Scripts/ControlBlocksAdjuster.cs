using UnityEngine;
public class ControlBlocksAdjuster
{
    private IControlBlocksCollisionDetection CollisionDetection { get; }

    public ControlBlocksAdjuster(IControlBlocksCollisionDetection collisionDetection)
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
