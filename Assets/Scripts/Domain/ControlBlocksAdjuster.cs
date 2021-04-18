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
            controlBlocks.Blocks.BlockList.Where(block => block.Y == i).ToList().ForEach(block =>
            {
                if (CollisionDetection.IsCollisionBlock(controlBlocks, block))
                {
                    controlBlocks.MoveUp();
                }
            });
        }

        for (var i = -1; i >= MinX; i--)
        {
            controlBlocks.Blocks.BlockList.Where(block => block.X == i).ToList().ForEach(block =>
            {
                if (CollisionDetection.IsCollisionBlock(controlBlocks, block))
                {
                    controlBlocks.MoveRight();
                }
            });
        }

        for (var i = 1; i <= MaxX; i++)
        {
            controlBlocks.Blocks.BlockList.Where(block => block.X == i).ToList().ForEach(block =>
            {
                if (CollisionDetection.IsCollisionBlock(controlBlocks, block))
                {
                    controlBlocks.MoveLeft();
                }
            });
        }
        return controlBlocks;
    }
}
