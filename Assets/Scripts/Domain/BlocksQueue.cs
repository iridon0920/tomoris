using System.Collections.Generic;
using Zenject;
public interface IBlocksQueue
{
    Queue<IBlocks> Queue { get; }
    IBlocks Dequeue();
}
public class BlocksQueue : IBlocksQueue
{
    public Queue<IBlocks> Queue { get; private set; }

    private BlocksFactory BlocksFactory;


    [Inject]
    public BlocksQueue(int size, BlocksFactory blocksFactory)
    {
        BlocksFactory = blocksFactory;

        Queue = new Queue<IBlocks>();
        for (var i = 0; i < size; i++)
        {
            Queue.Enqueue(BlocksFactory.Create());
        }
    }

    public IBlocks Dequeue()
    {
        Queue.Enqueue(BlocksFactory.Create());

        return Queue.Dequeue();
    }
}
