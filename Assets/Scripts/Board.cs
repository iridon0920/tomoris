using System;
using System.Linq;
using UnityEngine;

public interface IBoard
{
    int Width { get; }
    int Height { get; }
    int InsertPositionX { get; }

    bool[,] StatusByPositions { get; }

    void PutBlocks(IControlBlocks controlBlocks);
}
public class Board : IBoard
{
    public int Width { get; }
    public int Height { get; }
    public int InsertPositionX { get; }


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

    public void PutBlocks(IControlBlocks controlBlocks)
    {
        foreach (var block in controlBlocks.Blocks.BlockList)
        {
            StatusByPositions[
                controlBlocks.X + block.X,
                controlBlocks.Y + block.Y
            ] = true;
        }

        EraseIfAlign();
        FallToEmptyRow();
    }

    private void EraseIfAlign()
    {
        int blockCount;
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
                for (var x = 0; x < Width; x++)
                {
                    StatusByPositions[x, y] = false;
                }
            }
        }
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
}
