using System;

public class Block : IEquatable<Block>
{
    public int x { get; }
    public int y { get; }
    public Block(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Block MoveX(int addX)
    {
        return new Block(this.x + addX, this.y);
    }

    public Block MoveY(int addY)
    {
        return new Block(this.x, this.y + addY);
    }

    public bool Equals(Block block)
    {
        return this.x == block.x && this.y == block.y;
    }

}
