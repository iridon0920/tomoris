using Zenject;
using System.Threading.Tasks;

public class BlocksQueuePresenter
{
    const float PLAYER_1_POSITION_X = -6.5f;
    const float PLAYER_2_POSITION_X = 7.5f;
    const float COMMON_POSITION_Y = 8f;

    private readonly BlocksQueueView BlocksQueueView;

    [Inject]
    public BlocksQueuePresenter(BlocksQueueView blocksQueueView)
    {
        BlocksQueueView = blocksQueueView;
    }

    public async Task DrawAllQueueBlocks(IBlocks[] allQueueBlocks)
    {
        foreach (var queueBlocks in allQueueBlocks)
        {
            await BlocksQueueView.DrawQueueBlocks(queueBlocks);
        }
    }

    public async void DrawQueueBlocksDequeue(IBlocks nextBlocks)
    {
        BlocksQueueView.DeleteTopBlocks();
        BlocksQueueView.SqueezeEmptyPosition();
        await BlocksQueueView.DrawQueueBlocks(nextBlocks);
    }

    public void ChangePositionByPlayerId(int playerId)
    {
        if (playerId == 1)
        {
            BlocksQueueView.ChangePosition(PLAYER_1_POSITION_X, COMMON_POSITION_Y);
        }
        else if (playerId == 2)
        {
            BlocksQueueView.ChangePosition(PLAYER_2_POSITION_X, COMMON_POSITION_Y);
        }
    }
}
