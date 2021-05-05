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
        newControlBlocks = AdjustBlocksForSpinLoop(newControlBlocks);

        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return currentControlBlocks;
        }
        return newControlBlocks;
    }

    private ControlBlocks AdjustBlocksForSpinLoop(ControlBlocks controlBlocks)
    {
        var MinY = controlBlocks.Blocks.BlockList.Select(selectBlock => selectBlock.Y).Min();
        var MinX = controlBlocks.Blocks.BlockList.Select(selectBlock => selectBlock.X).Min();
        var MaxX = controlBlocks.Blocks.BlockList.Select(selectBlock => selectBlock.X).Max();

        for (var i = -1; i >= MinY; i--)
        {
            if (CollisionDetection.IsCollisionControlBlocksLower(controlBlocks, MinY))
            {
                controlBlocks.MoveUp();
            }
        }

        for (var i = -1; i >= MinX; i--)
        {
            if (CollisionDetection.IsCollisionControlBlocksLeftSide(controlBlocks, MinX))
            {
                controlBlocks.MoveRight();
            }
        }

        for (var i = 1; i <= MaxX; i++)
        {
            if (CollisionDetection.IsCollisionControlBlocksRightSide(controlBlocks, MaxX))
            {
                controlBlocks.MoveLeft();
            }
        }

        return controlBlocks;
    }
}
