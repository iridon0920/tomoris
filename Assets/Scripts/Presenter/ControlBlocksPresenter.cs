using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class ControlBlocksPresenter
{
    private readonly ControlBlocksView ControlBlocksView;

    [Inject]
    public ControlBlocksPresenter(ControlBlocksView controlBlocksView)
    {
        ControlBlocksView = controlBlocksView;
    }

    public void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        ControlBlocksView.DrawControlBlocks(controlBlocks);
    }
}
