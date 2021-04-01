using System;
using System.Linq;
using UnityEngine;

public interface IBoard
{
    int Width { get; }
    int Height { get; }
    int InsertPositionX { get; }

    bool[,] StatusByPositions { get; }
}
public class Board : IBoard
{
    public int Width { get; }
    public int Height { get; }
    public int InsertPositionX { get; }

    public Blocks CurrentBlocks { get; private set; }
    public int CurrentBlocksPositionX { get; private set; }
    public int CurrentBlocksPositionY { get; private set; }

    // 二次元配列を使って各座標のブロックの存在を管理
    public bool[,] StatusByPositions { get; private set; }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        InsertPositionX = (width - 1) / 2;

        StatusByPositions = new bool[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                StatusByPositions[x, y] = false;
            }
        }
    }

    public void InsertBlocks(Blocks controlBlocks)
    {
        InitCurrentBlocksPosition();
        CurrentBlocks = controlBlocks;
    }

    private void InitCurrentBlocksPosition()
    {
        CurrentBlocks = null;
        CurrentBlocksPositionX = InsertPositionX;
        CurrentBlocksPositionY = Height - 1;
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
                StatusByPositions[
                    CurrentBlocksPositionX + controlBlock.X,
                    CurrentBlocksPositionY + controlBlock.Y
                ] = true;
            }

            if (EraseIfAlign())
            {
                FallToEmptyRow();
            }

            InitCurrentBlocksPosition();
            return false;
        }
        CurrentBlocksPositionY = movePositionY;
        return true;
    }

    private bool EraseIfAlign()
    {

        int blockCount;
        var isErase = false;
        for (var y = 0; y < Height; y++)
        {
            blockCount = 0;
            for (var x = 0; x < Width; x++)
            {
                if (StatusByPositions[x, y])
                {
                    blockCount++;
                }
            }
            if (blockCount == Width)
            {
                isErase = true;
                for (var x = 0; x < Width; x++)
                {
                    StatusByPositions[x, y] = false;
                }
            }
        }
        return isErase;
    }

    private void FallToEmptyRow()
    {
        int blockCount;
        for (var y = 0; y < Height - 1; y++)
        {
            blockCount = 0;
            for (var x = 0; x < Width; x++)
            {
                if (!StatusByPositions[x, y])
                {
                    blockCount++;
                }
            }
            if (blockCount == Width)
            {
                for (var x = 0; x < Width; x++)
                {
                    StatusByPositions[x, y] = StatusByPositions[x, y + 1];
                    StatusByPositions[x, y + 1] = false;
                }
            }
        }
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
    private bool IsBlockCollisionBottom(Blocks controlBlocks, int movePositionY)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositionY = movePositionY + controlBlock.Y;
            if (movePlanPositionY < 0)
            {
                result = true;
            }
            if (IsMovePlanCollisionPutBlock(CurrentBlocksPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }

    // 横向きの衝突判定
    private bool IsBlocksCollisionSide(Blocks controlBlocks, int movePositionX)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositionX = movePositionX + controlBlock.X;
            if (movePlanPositionX < 0 || movePlanPositionX > Width - 1)
            {
                result = true;
            }
            var movePlanPositionY = CurrentBlocksPositionY + controlBlock.Y;
            if (IsMovePlanCollisionPutBlock(movePlanPositionX, movePlanPositionY))
            {
                result = true;
            }
        }
        return result;
    }

    // ブロックとの衝突判定
    private bool IsMovePlanCollisionPutBlock(int movePlanX, int movePlanY)
    {
        if (movePlanX >= 0 && movePlanX <= Width - 1 && movePlanY >= 0 && movePlanY <= Height - 1)
        {
            if (StatusByPositions[movePlanX, movePlanY])
            {
                return true;
            }
        }
        return false;
    }
}
