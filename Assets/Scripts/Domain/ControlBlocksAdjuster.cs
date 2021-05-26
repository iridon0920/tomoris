using System.Linq;
using Zenject;
using UnityEngine;
public class ControlBlocksAdjuster
{
    private CollisionDetection CollisionDetection { get; }
    private IControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public ControlBlocksAdjuster(CollisionDetection collisionDetection, IControlBlocksPresenter controlBlocksPresenter)
    {
        CollisionDetection = collisionDetection;
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public ControlBlocks AdjustBlocksForSpin(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if ((CollisionDetection.IsCollisionControlBlocksLower(newControlBlocks)
                && CollisionDetection.IsCollisionControlBlocksUpper(newControlBlocks))
            || (CollisionDetection.IsCollisionControlBlocksLeftSide(newControlBlocks)
                && CollisionDetection.IsCollisionControlBlocksRightSide(newControlBlocks)))
        {
            ControlBlocksPresenter.PlayCollisionSound();
            return currentControlBlocks;
        }

        ControlBlocksPresenter.PlaySpinSound();
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

    public ControlBlocks AdjustBlocksByPutBlocks(ControlBlocks controlBlocks)
    {
        for (var i = 0; i <= controlBlocks.Blocks.BlockList.Count; i++)
        {
            if (CollisionDetection.IsCollisionPutPosition(controlBlocks))
            {
                controlBlocks.MoveUp();
                continue;
            }

            break;
        }
        return controlBlocks;
    }
}
