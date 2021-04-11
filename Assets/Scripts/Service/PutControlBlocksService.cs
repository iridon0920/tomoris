using Zenject;

public class PutControlBlocksService
{
    private readonly IBoard Board;

    [Inject]
    public PutControlBlocksService(IBoard board)
    {
        Board = board;
    }

    public bool Execute(ControlBlocks controlBlocks)
    {
        if (controlBlocks.IsPutable)
        {
            Board.PutBlocks(controlBlocks);
            return true;
        }
        return false;
    }
}
