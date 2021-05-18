using System;
using System.Linq;
using System.Collections.Generic;
using Zenject;
public interface IBoard
{
    int Width { get; }
    int Height { get; }
    BoardPutBlocks BoardPutBlocks { get; }
    void AddPlayerCount();
    int GetInsertPositionX(int playerId);
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
    private int PlayerCount = 0;

    // 二次元配列を使って各座標のブロックの存在を管理
    public BoardPutBlocks BoardPutBlocks { get; } = new BoardPutBlocks(new List<BoardPutBlock>());

    private int NextBlockId = 1;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void AddPlayerCount()
    {
        PlayerCount++;
    }

    public int GetInsertPositionX(int playerId)
    {
        double interval = (Width - 1) / (PlayerCount + 1);
        double result = interval * playerId;
        return (int)Math.Round(result);

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
