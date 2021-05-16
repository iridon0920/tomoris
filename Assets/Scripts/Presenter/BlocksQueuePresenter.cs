using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System.Threading.Tasks;

public class BlocksQueuePresenter
{
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

    public void DrawQueueBlocksDequeue(IBlocks nextBlocks)
    {
        BlocksQueueView.DeleteTopBlocks();
        BlocksQueueView.SqueezeEmptyPosition();
        BlocksQueueView.DrawQueueBlocks(nextBlocks);
    }
}
