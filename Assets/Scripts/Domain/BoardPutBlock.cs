public class BoardPutBlock
{
    public int Id { get; }
    private IBlock Block;

    public BoardPutBlock(int id, IBlock block)
    {
        Id = id;
        Block = block;
    }

    public int GetX()
    {
        return Block.X;
    }

    public int GetY()
    {
        return Block.Y;
    }

    public BlockColor GetBlockColor()
    {
        return Block.BlockColor;
    }

    public void MoveDown()
    {
        Block = Block.MoveDown();
    }
}
