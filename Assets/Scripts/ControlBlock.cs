using System;

public class ControlBlock : IEquatable<ControlBlock>
{
    public int X { get; }
    public int Y { get; }
    public ControlBlock(int x, int y)
    {
        X = x;
        Y = y;
    }

    public ControlBlock MoveX(int addX)
    {
        return new ControlBlock(X + addX, Y);
    }

    public ControlBlock MoveY(int addY)
    {
        return new ControlBlock(X, Y + addY);
    }

    public bool Equals(ControlBlock block)
    {
        return X == block.X && Y == block.Y;
    }

}
