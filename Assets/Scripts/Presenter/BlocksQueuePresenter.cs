using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class BlocksQueuePresenter
{
    private readonly BlocksQueueView BlocksQueueView;

    [Inject]
    public BlocksQueuePresenter(BlocksQueueView blocksQueueView)
    {
        BlocksQueueView = blocksQueueView;
    }

    public void DrawAllQueueBlocks(IBlocks[] allQueueBlocks)
    {
        foreach (var queueBlocks in allQueueBlocks)
        {
            BlocksQueueView.DrawQueueBlocks(queueBlocks);
        }
    }

    public void DrawQueueBlocksDequeue(IBlocks nextBlocks)
    {
        BlocksQueueView.DeleteTopBlocks();
        BlocksQueueView.SqueezeEmptyPosition();
        BlocksQueueView.DrawQueueBlocks(nextBlocks);
    }
}
