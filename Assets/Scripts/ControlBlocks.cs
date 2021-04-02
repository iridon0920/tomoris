using System;
using System.Collections.Generic;

public interface IControlBlocks
{
    int X { get; }
    int Y { get; }
    IBlocks Blocks { get; }

    void MoveRight();
    void MoveLeft();
    void MoveDown();
    void Spin();
}
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

    public void MoveDown()
    {
        Y--;
    }

    public void Spin()
    {
        Blocks = Blocks.Spin();
    }
}
