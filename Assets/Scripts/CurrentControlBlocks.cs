using System;
using System.Collections.Generic;

public class CurrentControlBlocks
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public IBlocks Blocks { get; private set; }

    public CurrentControlBlocks(int x, int y, IBlocks blocks)
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
