using System;
using System.Linq;
using System.Collections.Generic;
public interface IBoard
{
    int Width { get; }
    int Height { get; }
    List<BoardBlock> Blocks { get; }

    int GetInsertPositionX();
    int GetInsertPositionY();
    List<BoardBlock> PutBlocks(ControlBlocks controlBlocks);
    bool ExistPosition(int x, int y);
    List<BoardBlock> EraseIfAlign();
    List<BoardBlock> FallToEmptyLine();
    bool IsGameOver();

}
public class Board : IBoard
{
    public int Width { get; }
    public int Height { get; }
    public int InsertPositionX { get; }


    // 二次元配列を使って各座標のブロックの存在を管理
    private int NextBlockId = 1;
    public List<BoardBlock> Blocks { get; }

    private int BlocksHeight = 0;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        Blocks = new List<BoardBlock>();
    }

    public int GetInsertPositionX()
    {
        return (Width - 1) / 2;
    }

    public int GetInsertPositionY()
    {
        return Height + 1;
    }
    public List<BoardBlock> PutBlocks(ControlBlocks controlBlocks)
    {
        var result = new List<BoardBlock>();
        foreach (var block in controlBlocks.GetBoardPositionBlockList())
        {
            var newBoardBlock = new BoardBlock(NextBlockId, block);
            NextBlockId++;
            Blocks.Add(newBoardBlock);
            BlocksHeight = Blocks.Select(boardblock => boardblock.GetY()).Max() + 1;

            result.Add(newBoardBlock);
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
        int emptyMinLine = -1;
        int emptyLineCount = 0;

        for (var y = 0; y < BlocksHeight; y++)
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
            }
            result.Add(block);
            return block;
        }).ToList();
        return result;
    }

    public bool IsGameOver()
    {
        return BlocksHeight > Height;
    }
}
