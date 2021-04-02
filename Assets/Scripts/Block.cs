using System;

public class Block : IEquatable<Block>
{
    public int X { get; }
    public int Y { get; }
    public Block(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Block block)
    {
        return X == block.X && Y == block.Y;
    }

}
