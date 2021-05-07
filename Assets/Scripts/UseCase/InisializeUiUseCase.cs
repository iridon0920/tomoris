using Zenject;

public class InitializeUiUseCase
{
    private readonly ScorePresenter ScorePresenter;

    [Inject]
    public InitializeUiUseCase(ScorePresenter scorePresenter)
    {
        ScorePresenter = scorePresenter;
    }

    public void Execute(int playerId)
    {
        ScorePresenter.ChangeScorePositionByPlayerId(playerId);
    }
}
