
using System.Collections.Generic;

public class ControlBlocksQueue
{
    public Queue<ControlBlocks> BlocksQueue { get; private set; }

    private int Count;

    public ControlBlocksQueue(int size)
    {
        BlocksQueue = new Queue<ControlBlocks>();
        for (var i = 0; i < size; i++)
        {
            BlocksQueue.Enqueue(CreateControlBlocks(i));
        }

        Count = size;
    }

    public ControlBlocks Dequeue()
    {
        BlocksQueue.Enqueue(CreateControlBlocks(Count));
        Count++;

        return BlocksQueue.Dequeue();
    }

    private ControlBlocks CreateControlBlocks(int i)
    {
        if (i % 2 == 0)
        {
            return new ControlBlocks(
                new List<ControlBlock>
                {
                        new ControlBlock(0, 2),
                        new ControlBlock(0, 1),
                        new ControlBlock(0, 0),
                        new ControlBlock(0, -1)
                }
            );
        }
        else
        {
            return new ControlBlocks(
                 new List<ControlBlock>
                 {
                        new ControlBlock(0, 1),
                        new ControlBlock(0, 0),
                        new ControlBlock(0, -1),
                        new ControlBlock(1, -1)
                 }
             );
        }
    }
}
