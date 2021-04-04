using UnityEngine;
public class BlockHandling
{
    public IBlocks CurrentBlocks { get; private set; }
    public int CurrentBlocksPositionX { get; private set; }
    public int CurrentBlocksPositionY { get; private set; }

    private IBoard Board;
    private IBlocksQueue BlocksQueue;

    public BlockHandling(IBoard board, IBlocksQueue blocksQueue)
    {
        Board = board;
        BlocksQueue = blocksQueue;
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
                var newBlocks = BlocksQueue.Dequeue();
                controlBlocks.Initialization(Board.InsertPositionX, Board.Height - 1, newBlocks);
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



    public void InsertBlocks(Blocks controlBlocks)
    {
        InitCurrentBlocksPosition();
        CurrentBlocks = controlBlocks;
    }

    private void InitCurrentBlocksPosition()
    {
        CurrentBlocks = null;
        CurrentBlocksPositionX = Board.InsertPositionX;
        CurrentBlocksPositionY = Board.Height - 1;
    }



    public bool MoveBlocksRight()
    {
        var movePosition = CurrentBlocksPositionX + 1;
        if (IsBlocksCollisionSide(CurrentBlocks, movePosition))
        {
            return false;
        }
        CurrentBlocksPositionX = movePosition;
        return true;
    }

    public bool MoveBlocksLeft()
    {
        var movePosition = CurrentBlocksPositionX - 1;
        if (IsBlocksCollisionSide(CurrentBlocks, movePosition))
        {
            return false;
        }
        CurrentBlocksPositionX = movePosition;
        return true;
    }

    public bool MoveBlocksDown()
    {
        var movePositionY = CurrentBlocksPositionY - 1;
        if (IsBlockCollisionBottom(CurrentBlocks, movePositionY))
        {
            foreach (var controlBlock in CurrentBlocks.BlockList)
            {
                Board.StatusByPositions[
                    CurrentBlocksPositionX + controlBlock.X,
                    CurrentBlocksPositionY + controlBlock.Y
                ] = true;
            }

            if (Board.EraseIfAlign())
            {
                Board.FallToEmptyRow();
            }

            InitCurrentBlocksPosition();
            return false;
        }
        CurrentBlocksPositionY = movePositionY;
        return true;
    }

    public bool SpinBlocks()
    {
        var newBlocks = CurrentBlocks.Spin();
        if (IsBlocksCollisionSide(newBlocks, CurrentBlocksPositionX))
        {
            return false;
        }
        CurrentBlocks = newBlocks;
        return true;
    }

    // 下の衝突判定
    private bool IsBlockCollisionBottom(IBlocks controlBlocks, int movePositionY)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositionY = movePositionY + controlBlock.Y;
            if (movePlanPositionY < 0)
            {
                result = true;
            }
            if (Board.IsMovePlanCollisionPutBlock(CurrentBlocksPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }

    // 横向きの衝突判定
    private bool IsBlocksCollisionSide(IBlocks controlBlocks, int movePositionX)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositionX = movePositionX + controlBlock.X;
            if (movePlanPositionX < 0 || movePlanPositionX > Board.Width - 1)
            {
                result = true;
            }
            var movePlanPositionY = CurrentBlocksPositionY + controlBlock.Y;
            if (Board.IsMovePlanCollisionPutBlock(movePlanPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }
}
