using System;
using System.Linq;
using System.Collections.Generic;

public interface IControlBlocks
{
    int X { get; }
    int Y { get; }
    IBlocks Blocks { get; }

    void MoveRight();
    void MoveLeft();
    void MoveUp();
    void MoveDown();
    void LeftSpin();
    void RightSpin();
    ControlBlocks Clone();
    List<IBlock> GetBoardPositionBlockList();
    List<IBlock> GetBoardPositionBlockListByLower(int minY);
    List<IBlock> GetBoardPositionBlockListByLeftSide(int minX);
    List<IBlock> GetBoardPositionBlockListByRightSide(int maxX);
}

/**
* プレイヤー操作対象のブロックとその座標を保持するEntityクラス
*/
public class ControlBlocks : IControlBlocks
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public IBlocks Blocks { get; private set; }

    public ControlBlocks(int x, int y, IBlocks blocks)
    {
        X = x;
        Y = y;
        Blocks = blocks;
    }

    public void MoveRight()
    {
        X++;
    }

    public void MoveLeft()
    {
        X--;
    }

    public void MoveUp()
    {
        Y++;
    }

    public void MoveDown()
    {
        Y--;
    }

    public void LeftSpin()
    {
        Blocks = Blocks.LeftSpin();
    }

    public void RightSpin()
    {
        Blocks = Blocks.RightSpin();
    }

    public ControlBlocks Clone()
    {
        return new ControlBlocks(X, Y, Blocks);
    }

    public List<IBlock> GetBoardPositionBlockList()
    {
        return CreateBoardPositionBlockList(Blocks.BlockList);
    }

    public List<IBlock> GetBoardPositionBlockListByLower(int minY)
    {
        return CreateBoardPositionBlockList(
            Blocks
                .BlockList
                .Where(block => block.Y < 0 && block.Y >= minY)
                .ToList()
        );
    }

    public List<IBlock> GetBoardPositionBlockListByLeftSide(int minX)
    {
        return CreateBoardPositionBlockList(
            Blocks
                .BlockList
                .Where(block => block.X < 0 && block.X >= minX)
                .ToList()
        );
    }

    public List<IBlock> GetBoardPositionBlockListByRightSide(int maxX)
    {
        return CreateBoardPositionBlockList(
            Blocks
                .BlockList
                .Where(block => block.X > 0 && block.X <= maxX)
                .ToList()
        );
    }


    private List<IBlock> CreateBoardPositionBlockList(List<IBlock> blockList)
    {
        var boardPositionBlockList = new List<IBlock>();
        foreach (var block in blockList)
        {
            var boardPositionX = X + block.X;
            var boardPositionY = Y + block.Y;
            var boardPositionBlock = new Block(boardPositionX, boardPositionY);
            boardPositionBlockList.Add(boardPositionBlock);
        }
        return boardPositionBlockList;
    }
}
