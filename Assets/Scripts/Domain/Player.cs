using Zenject;
public class Player
{
    public int PlayerId { get; }
    private readonly BlockController BlockController;

    [Inject]
    public Player(int playerId, BlockController blockController)
    {
        PlayerId = playerId;
        BlockController = blockController;

        BlockController.PlayerId = PlayerId;
    }

    public double GetScoreTotalPoints()
    {
        return BlockController.Score.TotalPoints;
    }

    public class Factory : PlaceholderFactory<int, Player>
    { }
}
