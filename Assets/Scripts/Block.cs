using System;

public interface IBlock : IEquatable<IBlock>
{
    int X { get; }
    int Y { get; }

    IBlock MoveDown();
    IBlock RotateLeft90Degree();
    IBlock RotateRight90Degree();
}
public class Block : IBlock
{
    public int X { get; }
    public int Y { get; }
    public Block(int x, int y)
    {
        X = x;
        Y = y;
    }

    public IBlock MoveDown()
    {
        return new Block(X, Y - 1);
    }

    // 原点に対し、90度左方向へ座標を回転させる
    public IBlock RotateLeft90Degree()
    {
        return new Block(-Y, X);
    }

    // 原点に対し、90度右方向へ座標を回転させる
    public IBlock RotateRight90Degree()
    {
        return new Block(Y, -X);
    }

    public bool Equals(IBlock block)
    {
        return X == block.X && Y == block.Y;
    }


}
