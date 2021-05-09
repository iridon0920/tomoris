using Zenject;

public class GetNextControlBlocksService
{
    private readonly IBoard Board;
    private readonly IBlocksQueue Queue;

    private readonly ControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public GetNextControlBlocksService(
        IBoard board,
        IBlocksQueue queue,
        ControlBlocksPresenter controlBlocksPresenter
    )
    {
        Board = board;
        Queue = queue;
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public ControlBlocks Execute()
    {
        var controlBlocks = new ControlBlocks(
                    Board.GetInsertPositionX(),
                    Board.GetInsertPositionY(),
                    Queue.Dequeue()
                );
        ControlBlocksPresenter.DrawControlBlocks(controlBlocks);

        return controlBlocks;
    }
}
