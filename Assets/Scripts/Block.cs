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

    public Block MoveX(int addX)
    {
        return new Block(X + addX, Y);
    }

    public Block MoveY(int addY)
    {
        return new Block(X, Y + addY);
    }

    public bool Equals(Block block)
    {
        return X == block.X && Y == block.Y;
    }

}
