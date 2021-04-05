using UnityEngine;
public class BlockHandling
{
    private IBoard Board { get; }

    public BlockHandling(IBoard board)
    {
        Board = board;
    }

    public void AdjustControlBlocksPosition(IControlBlocks controlBlocks)
    {
        while (true)
        {
            if (IsCollisionRightWall(controlBlocks))
            {
                controlBlocks.MoveLeft();
                continue;
            }

            if (IsCollisionLeftWall(controlBlocks))
            {
                controlBlocks.MoveRight();
                continue;
            }
            if (IsCollisionGround(controlBlocks))
            {
                controlBlocks.MoveUp();
                continue;
            }

            break;
        }
    }

    private bool IsCollisionRightWall(IControlBlocks controlBlocks)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.Blocks.BlockList)
        {
            var movePlanPositionX = controlBlocks.X + controlBlock.X;
            if (movePlanPositionX > Board.Width - 1)
            {
                result = true;
            }
        }
        return result;
    }

    private bool IsCollisionLeftWall(IControlBlocks controlBlocks)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.Blocks.BlockList)
        {
            var movePlanPositionX = controlBlocks.X + controlBlock.X;
            if (movePlanPositionX < 0)
            {
                result = true;
            }
        }
        return result;
    }

    private bool IsCollisionGround(IControlBlocks controlBlocks)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.Blocks.BlockList)
        {
            var movePlanPositionY = controlBlocks.Y + controlBlock.Y;
            if (movePlanPositionY < 0)
            {
                result = true;
            }
        }
        return result;
    }
}
