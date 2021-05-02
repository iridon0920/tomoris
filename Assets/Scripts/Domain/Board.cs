using System;
using System.Linq;
using System.Collections.Generic;
public interface IBoard
{
    int Width { get; }
    int Height { get; }
    BoardPutBlocks BoardPutBlocks { get; }

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
    public BoardPutBlocks BoardPutBlocks { get; }

    private int NextBlockId = 1;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        BoardPutBlocks = new BoardPutBlocks(new List<BoardPutBlock>());
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
        var addPutBlocks = new List<BoardPutBlock>();
        foreach (var block in controlBlocks.GetBoardPositionBlockList())
        {
            var newBoardPutBlock = new BoardPutBlock(NextBlockId, block);
            NextBlockId++;

            addPutBlocks.Add(newBoardPutBlock);
        }
        BoardPutBlocks.AddBoardPutBlocks(addPutBlocks);

        return addPutBlocks;
    }

    public bool ExistPosition(int x, int y)
    {
        return BoardPutBlocks.ExistBlockByPosition(x, y);
    }

    public List<BoardPutBlock> EraseIfAlign()
    {
        return BoardPutBlocks.RemoveYPositionMaxCountBlocks(Width);
    }

    public List<BoardPutBlock> FallToEmptyLine()
    {
        return BoardPutBlocks.SqueezeEmptyYPosition();
    }

    public bool IsGameOver()
    {
        return BoardPutBlocks.GetMaxYPosition() + 1 > Height;
    }
}
