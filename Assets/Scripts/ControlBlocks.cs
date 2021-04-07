using System;
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
    IControlBlocks Clone();
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

    public IControlBlocks Clone()
    {
        return new ControlBlocks(X, Y, Blocks);
    }

}
