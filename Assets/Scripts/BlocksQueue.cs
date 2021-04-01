
using System.Collections.Generic;

public class BlocksQueue
{
    public Queue<Blocks> Queue { get; private set; }

    private int Count;

    public BlocksQueue(int size)
    {
        Queue = new Queue<Blocks>();
        for (var i = 0; i < size; i++)
        {
            Queue.Enqueue(CreateBlocks(i));
        }

        Count = size;
    }

    public Blocks Dequeue()
    {
        Queue.Enqueue(CreateBlocks(Count));
        Count++;

        return Queue.Dequeue();
    }

    private Blocks CreateBlocks(int i)
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
