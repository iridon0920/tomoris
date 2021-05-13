using System.Collections.Generic;
using Zenject;
public interface IBlocksQueue
{
    Queue<IBlocks> Queue { get; }
    void InitializeQueue(int size);
    IBlocks Dequeue();
}
public class BlocksQueue : IBlocksQueue
{
    public Queue<IBlocks> Queue { get; private set; }

    private BlocksFactory BlocksFactory;


    [Inject]
    public BlocksQueue(BlocksFactory blocksFactory)
    {
        BlocksFactory = blocksFactory;
    }

    public void InitializeQueue(int size)
    {
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
