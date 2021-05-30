using Zenject;
public class InitializeUiUseCase
{
    private readonly IBoard Board;
    private readonly BlocksQueuePresenter BlocksQueuePresenter;
    private readonly IControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public InitializeUiUseCase(
        IBoard board,
        BlocksQueuePresenter blocksQueuePresenter,
        IControlBlocksPresenter controlBlocksPresenter
    )
    {
        Board = board;
        BlocksQueuePresenter = blocksQueuePresenter;
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public void Execute(int playerId)
    {
        Board.AddPlayerCount();
        BlocksQueuePresenter.ChangePositionByPlayerId(playerId);
        ControlBlocksPresenter.SetPlayerId(playerId);
    }
}
