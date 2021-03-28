using System;
using System.Linq;

public class Board
{
    public int Width { get; }
    public int Height { get; }
    private int InsertPositionX { get; }

    public ControlBlocks CurrentControlBlocks { get; private set; }
    public int CurrentControlBlocksPositionX { get; private set; }
    public int CurrentControlBlocksPositionY { get; private set; }

    // 二次元配列を使って各座標のブロックの存在を管理
    public bool[,] StatusByPositions { get; private set; }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        InsertPositionX = width / 2 - 1;

        StatusByPositions = new bool[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                StatusByPositions[x, y] = false;
            }
        }
    }

    public void InsertBlocks(ControlBlocks controlBlocks)
    {
        CurrentControlBlocks = controlBlocks;
        CurrentControlBlocksPositionX = InsertPositionX;
        CurrentControlBlocksPositionY = Height - 1;
    }

    public bool MoveBlocks(int movePositionX, int movePositionY)
    {
        if (IsBlocksCollisionSide(CurrentControlBlocks, movePositionX))
        {
            return false;
        }
        if (IsBlockCollisionBottom(CurrentControlBlocks, movePositionX, movePositionY))
        {
            foreach (var controlBlock in CurrentControlBlocks.BlockList)
            {
                StatusByPositions[movePositionX + controlBlock.X, movePositionY + controlBlock.Y] = true;
                CurrentControlBlocks = null;
            }
        }
        CurrentControlBlocksPositionX = movePositionX;
        CurrentControlBlocksPositionY = movePositionY;

        return true;
    }

    public bool SpinBlocks()
    {
        var newControlBlocks = CurrentControlBlocks.Spin();
        if (IsBlocksCollisionSide(newControlBlocks, CurrentControlBlocksPositionX))
        {
            return false;
        }
        CurrentControlBlocks = newControlBlocks;
        return true;
    }

    // 下の衝突判定
    private bool IsBlockCollisionBottom(ControlBlocks controlBlocks, int movePositionX, int movePositionY)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositionY = movePositionY + controlBlock.Y;
            if (movePlanPositionY <= 0)
            {
                result = true;
            }
        }
        return result;
    }

    // 横向きの衝突判定
    private bool IsBlocksCollisionSide(ControlBlocks controlBlocks, int movePositionX)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositoinX = movePositionX + controlBlock.X;
            if (movePlanPositoinX < 0 || movePlanPositoinX > Width - 1)
            {
                result = true;
            }
        }
        return result;
    }
}
