using Zenject;

public class BlockControllUseCase
{
    private readonly ControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public BlockControllUseCase(ControlBlocksPresenter controlBlocksPresenter)
    {
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public ControlBlocks Execute(IMoveControlBlocksService moveService, ControlBlocks controlBlocks)
    {
        var newControlBlocks = moveService.Execute(controlBlocks);
        ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        return newControlBlocks;
    }
}
