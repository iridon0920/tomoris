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
            else
            {
                if (movePlanPositionY < Height - 1)
                {
                    if (StatusByPositions[
                        CurrentControlBlocksPositionX,
                        movePlanPositionY
                    ])
                    {
                        result = true;
                    }
                }

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
            else
            {
                var movePlanPositionY = CurrentControlBlocksPositionY + controlBlock.Y;
                if (movePlanPositionY >= 0 && movePlanPositionY < Height - 1)
                {
                    if (StatusByPositions[
                        movePlanPositoinX,
                        movePlanPositionY
                    ])
                    {
                        result = true;
                    }
                }
            }
        }
        return result;
    }
}
