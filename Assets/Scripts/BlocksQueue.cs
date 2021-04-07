using System.Collections.Generic;

public interface IBlocksQueue
{
    Queue<IBlocks> Queue { get; }
    IBlocks Dequeue();
}
public class BlocksQueue : IBlocksQueue
{
    public Queue<IBlocks> Queue { get; private set; }

    private int Count;

    public BlocksQueue(int size)
    {
        Queue = new Queue<IBlocks>();
        for (var i = 0; i < size; i++)
        {
            Queue.Enqueue(CreateBlocks(i));
        }

        Count = size;
    }

    public IBlocks Dequeue()
    {
        Queue.Enqueue(CreateBlocks(Count));
        Count++;

        return Queue.Dequeue();
    }

    private IBlocks CreateBlocks(int i)
    {
        if (i % 2 == 0)
        {
            return new Blocks(
                new List<Block>
                {
                        new Block(0, 2),
                        new Block(0, 1),
                        new Block(0, 0),
                        new Block(0, -1)
                }
            );
        }
        else
        {
            return new Blocks(
                 new List<Block>
                 {
                        new Block(0, 1),
                        new Block(0, 0),
                        new Block(0, -1),
                        new Block(1, -1)
                 }
             );
        }
    }
}
