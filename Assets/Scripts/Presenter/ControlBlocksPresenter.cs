using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public interface IControlBlocksPresenter
{
    void DrawControlBlocks(IControlBlocks controlBlocks);
    void ChangeControlBlocks(IControlBlocks controlBlocks);
    void PlayCollisionSound();
}
public class ControlBlocksPresenter : IControlBlocksPresenter
{
    private readonly ControlBlocksView ControlBlocksView;

    [Inject]
    public ControlBlocksPresenter(ControlBlocksView controlBlocksView)
    {
        ControlBlocksView = controlBlocksView;
    }

    public void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        ControlBlocksView.DeleteControlBlocks();
        ControlBlocksView.DrawControlBlocks(controlBlocks);
    }

    public void ChangeControlBlocks(IControlBlocks controlBlocks)
    {
        ControlBlocksView.ChangeControlBlocksPosition(controlBlocks);
    }

    public void PlayCollisionSound()
    {
        ControlBlocksView.PlayCollisionSound();
    }
}
