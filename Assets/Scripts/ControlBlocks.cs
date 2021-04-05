using System;
using System.Collections.Generic;

public interface IControlBlocks
{
    int X { get; }
    int Y { get; }
    IBlocks Blocks { get; }

    void Initialization(int x, int y, IBlocks blocks);
    void MoveRight();
    void MoveLeft();
    void MoveUp();
    void MoveDown();
    void Spin();
}

/**
* プレイヤー操作対象のブロックとその座標を保持するEntityクラス
*/
public class ControlBlocks : IControlBlocks
{
    public int PlayerId { get; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public IBlocks Blocks { get; private set; }

    public ControlBlocks(int playerId, int x, int y, IBlocks blocks)
    {
        PlayerId = playerId;
        Initialization(x, y, blocks);
    }

    public void Initialization(int x, int y, IBlocks blocks)
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

    public void Spin()
    {
        Blocks = Blocks.Spin();
    }
}
