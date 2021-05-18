using Zenject;
using UnityEngine;
public class InitializeUiUseCase
{
    private readonly ScorePresenter ScorePresenter;
    private readonly BlocksQueuePresenter BlocksQueuePresenter;

    [Inject]
    public InitializeUiUseCase(ScorePresenter scorePresenter, BlocksQueuePresenter blocksQueuePresenter)
    {
        ScorePresenter = scorePresenter;
        BlocksQueuePresenter = blocksQueuePresenter;
    }

    public void Execute(int playerId)
    {
        ScorePresenter.ChangeScorePositionByPlayerId(playerId);
        BlocksQueuePresenter.ChangePositionByPlayerId(playerId);
    }
}
