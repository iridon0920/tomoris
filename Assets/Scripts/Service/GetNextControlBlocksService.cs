using Zenject;

public class GetNextControlBlocksService
{
    private readonly IBoard Board;
    private readonly IBlocksQueue Queue;

    [Inject]
    public GetNextControlBlocksService(IBoard board, IBlocksQueue queue)
    {
        Board = board;
        Queue = queue;
    }

    public ControlBlocks Execute()
    {
        return new ControlBlocks(
                    Board.InsertPositionX,
                    Board.Height - 1,
                    Queue.Dequeue()
                );
    }
}
