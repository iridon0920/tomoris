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

    IReadOnlyReactiveCollection<BoardBlock> RxBlocks { get; }
    IReadOnlyReactiveProperty<BoardBlock> RxFallBlock { get; }

    List<BoardBlock> PutBlocks(ControlBlocks controlBlocks);
    bool ExistPosition(int x, int y);
    List<BoardBlock> EraseIfAlign();
    List<BoardBlock> FallToEmptyLine();
}
public class Board : IBoard
{
    public int Width { get; }
    public int Height { get; }
    public int InsertPositionX { get; }


    // 二次元配列を使って各座標のブロックの存在を管理
    private int NextBlockId = 1;
    private ReactiveCollection<BoardBlock> Blocks;
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

        FallBlock = new ReactiveProperty<BoardBlock>();
        Blocks = new ReactiveCollection<BoardBlock>();
    }

    public List<BoardBlock> PutBlocks(ControlBlocks controlBlocks)
    {
        var result = new List<BoardBlock>();
        foreach (var block in controlBlocks.GetBoardPositionBlockList())
        {
            var newBoardBlock = new BoardBlock(NextBlockId, block);
            Blocks.Add(newBoardBlock);
            result.Add(newBoardBlock);
            NextBlockId++;
        }
        return result;
    }

    public bool ExistPosition(int x, int y)
    {
        return Blocks.Any(block => block.GetX() == x && block.GetY() == y);
    }

    public List<BoardBlock> EraseIfAlign()
    {
        var result = new List<BoardBlock>();
        for (var y = 0; y < Height; y++)
        {
            var aligenLineBlocks = Blocks.Where(block => block.GetY() == y).ToList();
            if (aligenLineBlocks.Count == Width)
            {
                foreach (var lineBlock in aligenLineBlocks)
                {
                    Blocks.Remove(lineBlock);
                    result.Add(lineBlock);
                }
            }
        }
        return result;
    }

    public List<BoardBlock> FallToEmptyLine()
    {
        var result = new List<BoardBlock>();
        var blocksHeight = Blocks.Select(block => block.GetY()).Max() + 1;
        int emptyMinLine = -1;
        int emptyLineCount = 0;

        for (var y = 0; y < blocksHeight; y++)
        {
            var emptyLineBlocks = Blocks.Where(block => block.GetY() == y).ToList();
            if (emptyLineBlocks.Count == 0)
            {
                if (emptyMinLine == -1)
                {
                    emptyMinLine = y;
                }
                emptyLineCount++;
            }
        }

        Blocks.Where(block => block.GetY() >= emptyMinLine).Select(block =>
        {
            for (var i = 0; i < emptyLineCount; i++)
            {
                block.MoveDown();
                FallBlock.Value = block;
            }
            result.Add(block);
            return block;
        }).ToReactiveCollection();
        return result;
    }
}
