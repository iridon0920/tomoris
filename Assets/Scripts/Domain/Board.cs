using System;
using System.Linq;
using System.Collections.Generic;
public interface IBoard
{
    int Width { get; }
    int Height { get; }
    List<BoardPutBlock> Blocks { get; }

    int GetInsertPositionX();
    int GetInsertPositionY();
    List<BoardPutBlock> PutBlocks(ControlBlocks controlBlocks);
    bool ExistPosition(int x, int y);
    List<BoardPutBlock> EraseIfAlign();
    List<BoardPutBlock> FallToEmptyLine();
    bool IsGameOver();

}
public class Board : IBoard
{
    public int Width { get; }
    public int Height { get; }
    public int InsertPositionX { get; }


    // 二次元配列を使って各座標のブロックの存在を管理
    private int NextBlockId = 1;
    public List<BoardPutBlock> Blocks { get; }

    private int BlocksHeight = 0;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        Blocks = new List<BoardPutBlock>();
    }

    public int GetInsertPositionX()
    {
        return (Width - 1) / 2;
    }

    public int GetInsertPositionY()
    {
        return Height + 1;
    }
    public List<BoardPutBlock> PutBlocks(ControlBlocks controlBlocks)
    {
        var result = new List<BoardPutBlock>();
        foreach (var block in controlBlocks.GetBoardPositionBlockList())
        {
            var newBoardPutBlock = new BoardPutBlock(NextBlockId, block);
            NextBlockId++;
            Blocks.Add(newBoardPutBlock);
            BlocksHeight = Blocks.Select(boardblock => boardblock.GetY()).Max() + 1;

            result.Add(newBoardPutBlock);
        }
        return result;
    }

    public bool ExistPosition(int x, int y)
    {
        return Blocks.Any(block => block.GetX() == x && block.GetY() == y);
    }

    public List<BoardPutBlock> EraseIfAlign()
    {
        var result = new List<BoardPutBlock>();
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

    public List<BoardPutBlock> FallToEmptyLine()
    {
        var result = new List<BoardPutBlock>();
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
