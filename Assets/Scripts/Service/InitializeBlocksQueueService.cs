using Zenject;
using System.Threading.Tasks;

public class InitializeBlocksQueueService
{
    private readonly IBlocksQueue BlocksQueue;
    private readonly BlocksQueuePresenter BlocksQueuePresenter;

    [Inject]
    public InitializeBlocksQueueService(
         IBlocksQueue blocksQueue,
        BlocksQueuePresenter blocksQueuePresenter
    )
    {
        BlocksQueue = blocksQueue;
        BlocksQueuePresenter = blocksQueuePresenter;
    }

    public async Task Execute(int queueSize)
    {
        BlocksQueue.InitializeQueue(queueSize);
        await BlocksQueuePresenter.DrawAllQueueBlocks(BlocksQueue.Queue.ToArray());
    }
}
