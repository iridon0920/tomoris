using Zenject;
public class GetNextControlBlocksService
{
    private readonly IBoard Board;
    private readonly IBlocksQueue BlocksQueue;

    private readonly IControlBlocksPresenter ControlBlocksPresenter;
    private readonly BlocksQueuePresenter BlocksQueuePresenter;

    [Inject]
    public GetNextControlBlocksService(
        IBoard board,
        IBlocksQueue blocksQueue,
        IControlBlocksPresenter controlBlocksPresenter,
        BlocksQueuePresenter blocksQueuePresenter
    )
    {
        Board = board;
        BlocksQueue = blocksQueue;
        ControlBlocksPresenter = controlBlocksPresenter;
        BlocksQueuePresenter = blocksQueuePresenter;
    }

    public ControlBlocks Execute(int playerId)
    {
        var controlBlocks = new ControlBlocks(
                    Board.GetInsertPositionX(playerId),
                    Board.GetInsertPositionY(),
                    BlocksQueue.Dequeue()
                );

        ControlBlocksPresenter.DrawControlBlocks(controlBlocks);

        BlocksQueuePresenter.DrawQueueBlocksDequeue(BlocksQueue.LastBlock);

        return controlBlocks;
    }
}
