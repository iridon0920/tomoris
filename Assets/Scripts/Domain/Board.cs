using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
public interface IBoard
{
    int Width { get; }
    int Height { get; }
    int InsertPositionX { get; }

    bool[,] StatusByPositions { get; }

    IReadOnlyReactiveCollection<BoardBlock> RxBlocks { get; }
    IReadOnlyReactiveProperty<BoardBlock> RxFallBlock { get; }

    void PutBlocks(ControlBlocks controlBlocks);
    bool ExistPosition(int x, int y);
}
public class Board : IBoard
{
    public int Width { get; }
    public int Height { get; }
    public int InsertPositionX { get; }


    // 二次元配列を使って各座標のブロックの存在を管理
    public bool[,] StatusByPositions { get; private set; }
    private int NextBlockId = 1;
    private ReactiveCollection<BoardBlock> Blocks { get; }
    public IReadOnlyReactiveCollection<BoardBlock> RxBlocks
    {
        get { return Blocks; }
    }
    private ReactiveProperty<BoardBlock> FallBlock;
    public IReadOnlyReactiveProperty<BoardBlock> RxFallBlock
    {
        get { return FallBlock; }
    }

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
        FallBlock = new ReactiveProperty<BoardBlock>();
        Blocks = new ReactiveCollection<BoardBlock>();
    }

    public void PutBlocks(ControlBlocks controlBlocks)
    {
        foreach (var block in controlBlocks.GetBoardPositionBlockList())
        {
            Blocks.Add(new BoardBlock(NextBlockId, block));
            NextBlockId++;
        }

        EraseIfAlign();
    }

    public bool ExistPosition(int x, int y)
    {
        return Blocks.Any(block => block.GetX() == x && block.GetY() == y);
    }

    private void EraseIfAlign()
    {
        for (var y = 0; y < Height; y++)
        {
            var AligenLineBlocks = Blocks.Where(block => block.GetY() == y).ToList();
            if (AligenLineBlocks.Count == Width)
            {

                foreach (var lineBlock in AligenLineBlocks)
                {
                    Blocks.Remove(lineBlock);
                }
                Blocks.Where(block => block.GetY() > y).Select(block =>
                {
                    block.MoveDown();
                    FallBlock.Value = block;
                    return block;
                }).ToReactiveCollection();
                y = 0;
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
