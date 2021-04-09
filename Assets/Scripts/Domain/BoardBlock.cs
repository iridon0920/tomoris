public class BoardBlock
{
    public int Id { get; }
    private IBlock Block;

    public BoardBlock(int id, IBlock block)
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

    public void MoveDown()
    {
        Block = Block.MoveDown();
    }
}
