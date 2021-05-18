using Zenject;
using UnityEngine;
public class InitializeUiUseCase
{
    private readonly IBoard Board;
    private readonly ScorePresenter ScorePresenter;
    private readonly BlocksQueuePresenter BlocksQueuePresenter;

    [Inject]
    public InitializeUiUseCase(IBoard board, ScorePresenter scorePresenter, BlocksQueuePresenter blocksQueuePresenter)
    {
        Board = board;
        ScorePresenter = scorePresenter;
        BlocksQueuePresenter = blocksQueuePresenter;
    }

    public void Execute(int playerId)
    {
        Board.AddPlayerCount();
        ScorePresenter.ChangeScorePositionByPlayerId(playerId);
        BlocksQueuePresenter.ChangePositionByPlayerId(playerId);
    }
}
