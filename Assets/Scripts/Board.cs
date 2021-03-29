using System;
using System.Linq;
using UnityEngine;

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

    public void InsertBlocks(ControlBlocks controlBlocks)
    {
        InitCurrentControlBlocksPosition();
        CurrentControlBlocks = controlBlocks;
    }

    private void InitCurrentControlBlocksPosition()
    {
        CurrentControlBlocks = null;
        CurrentControlBlocksPositionX = InsertPositionX;
        CurrentControlBlocksPositionY = Height - 1;
    }

    public bool MoveBlocksRight()
    {
        var movePosition = CurrentControlBlocksPositionX + 1;
        if (IsBlocksCollisionSide(CurrentControlBlocks, movePosition))
        {
            return false;
        }
        CurrentControlBlocksPositionX = movePosition;
        return true;
    }

    public bool MoveBlocksLeft()
    {
        var movePosition = CurrentControlBlocksPositionX - 1;
        if (IsBlocksCollisionSide(CurrentControlBlocks, movePosition))
        {
            return false;
        }
        CurrentControlBlocksPositionX = movePosition;
        return true;
    }

    public bool MoveBlocksDown()
    {
        var movePositionY = CurrentControlBlocksPositionY - 1;
        if (IsBlockCollisionBottom(CurrentControlBlocks, movePositionY))
        {
            foreach (var controlBlock in CurrentControlBlocks.BlockList)
            {
                StatusByPositions[
                    CurrentControlBlocksPositionX + controlBlock.X,
                    CurrentControlBlocksPositionY + controlBlock.Y
                ] = true;
            }

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

            if (isErase)
            {
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

            InitCurrentControlBlocksPosition();
            return false;
        }
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
    private bool IsBlockCollisionBottom(ControlBlocks controlBlocks, int movePositionY)
    {
        var result = false;
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var movePlanPositionY = movePositionY + controlBlock.Y;
            if (movePlanPositionY < 0)
            {
                result = true;
            }
            if (IsMovePlanCollisionPutBlock(CurrentControlBlocksPositionX, movePlanPositionY))
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
            var movePlanPositionX = movePositionX + controlBlock.X;
            if (movePlanPositionX < 0 || movePlanPositionX > Width - 1)
            {
                result = true;
            }
            var movePlanPositionY = CurrentControlBlocksPositionY + controlBlock.Y;
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
