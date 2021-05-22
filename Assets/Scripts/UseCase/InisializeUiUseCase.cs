using Zenject;
public class InitializeUiUseCase
{
    private readonly IBoard Board;
    private readonly ScorePresenter ScorePresenter;
    private readonly BlocksQueuePresenter BlocksQueuePresenter;
    private readonly IControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public InitializeUiUseCase(
        IBoard board,
        ScorePresenter scorePresenter,
        BlocksQueuePresenter blocksQueuePresenter,
        IControlBlocksPresenter controlBlocksPresenter
    )
    {
        Board = board;
        ScorePresenter = scorePresenter;
        BlocksQueuePresenter = blocksQueuePresenter;
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public void Execute(int playerId)
    {
        Board.AddPlayerCount();
        ScorePresenter.ChangeScorePositionByPlayerId(playerId);
        BlocksQueuePresenter.ChangePositionByPlayerId(playerId);
        ControlBlocksPresenter.SetPlayerId(playerId);
    }
}
