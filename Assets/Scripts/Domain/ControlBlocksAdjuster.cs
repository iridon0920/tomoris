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
        for (var i = 0; i <= controlBlocks.Blocks.BlockList.Count; i++)
        {
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

            if (CollisionDetection.IsCollisionControlBlocksLower(controlBlocks))
            {
                controlBlocks.MoveUp();
                continue;
            }

            break;
        }

        return controlBlocks;
    }
}
