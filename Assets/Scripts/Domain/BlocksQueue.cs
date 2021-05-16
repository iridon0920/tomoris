using System.Collections.Generic;
using Zenject;

public interface IBlocksQueue
{
    Queue<IBlocks> Queue { get; }
    void InitializeQueue(int size);
    IBlocks Dequeue();
    IBlocks LastBlock { get; }
}
public class BlocksQueue : IBlocksQueue
{
    public Queue<IBlocks> Queue { get; private set; }

    private BlocksFactory BlocksFactory;

    public IBlocks LastBlock { get; private set; }


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
        LastBlock = BlocksFactory.Create();
        Queue.Enqueue(LastBlock);

        return Queue.Dequeue();
    }
}
