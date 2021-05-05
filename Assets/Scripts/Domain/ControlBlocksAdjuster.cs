using System.Linq;
using Zenject;
using UnityEngine;
public class ControlBlocksAdjuster
{
    private CollisionDetection CollisionDetection { get; }

    [Inject]
    public ControlBlocksAdjuster(CollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public ControlBlocks AdjustBlocksForSpin(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if (CollisionDetection.IsCollisionControlBlocksLower(newControlBlocks)
            && CollisionDetection.IsCollisionControlBlocksUpper(newControlBlocks)
        )
        {
            return currentControlBlocks;
        }

        if (CollisionDetection.IsCollisionControlBlocksLeftSide(newControlBlocks)
            && CollisionDetection.IsCollisionControlBlocksRightSide(newControlBlocks)
        )
        {
            return currentControlBlocks;
        }

        return AdjustBlocksForSpinLoop(newControlBlocks);
    }

    private ControlBlocks AdjustBlocksForSpinLoop(ControlBlocks controlBlocks)
    {
        while (true)
        {
            if (CollisionDetection.IsCollisionControlBlocksLower(controlBlocks))
            {
                controlBlocks.MoveUp();
                continue;
            }

            if (CollisionDetection.IsCollisionControlBlocksLeftSide(controlBlocks))
            {
                controlBlocks.MoveRight();
                continue;
            }

            if (CollisionDetection.IsCollisionControlBlocksRightSide(controlBlocks))
            {
                controlBlocks.MoveLeft();
                continue;
            }

            break;
        }

        return controlBlocks;
    }
}
