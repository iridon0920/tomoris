using System;

public enum BlockColor
{
    Blue,
    Green,
    LightBlue,
    Orange,
    Purple,
    Red,
    Yellow
}

public interface IBlock : IEquatable<IBlock>
{
    int X { get; }
    int Y { get; }

    BlockColor BlockColor { get; }

    IBlock MoveDown();
    IBlock RotateLeft90Degree();
    IBlock RotateRight90Degree();
}
public class Block : IBlock
{
    public int X { get; }
    public int Y { get; }

    public BlockColor BlockColor { get; }
    public Block(int x, int y, BlockColor blockColor = BlockColor.Blue)
    {
        X = x;
        Y = y;
        BlockColor = blockColor;
    }

    public IBlock MoveDown()
    {
        return new Block(X, Y - 1, BlockColor);
    }

    // 原点に対し、90度左方向へ座標を回転させる
    public IBlock RotateLeft90Degree()
    {
        return new Block(-Y, X, BlockColor);
    }

    // 原点に対し、90度右方向へ座標を回転させる
    public IBlock RotateRight90Degree()
    {
        return new Block(Y, -X, BlockColor);
    }

    public bool Equals(IBlock block)
    {
        return X == block.X && Y == block.Y;
    }
}
