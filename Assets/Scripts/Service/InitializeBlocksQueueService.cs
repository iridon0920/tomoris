using Zenject;
using Cysharp.Threading.Tasks;

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

    public void Execute(int queueSize)
    {
        BlocksQueue.InitializeQueue(queueSize);
        BlocksQueuePresenter.DrawAllQueueBlocks(BlocksQueue.Queue.ToArray());
    }
}
