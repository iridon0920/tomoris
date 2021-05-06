using Zenject;
public class Player
{
    private readonly int PlayerId;
    private readonly BlockController BlockController;

    [Inject]
    public Player(int playerId, BlockController blockController)
    {
        PlayerId = playerId;
        BlockController = blockController;

        BlockController.PlayerId = PlayerId;
    }

    public class Factory : PlaceholderFactory<int, Player>
    { }
}
