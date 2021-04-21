using Zenject;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private readonly ControlBlocksPresenter ControlBlocksPresenter;
    private readonly BoardPresenter BoardPresenter;

    [Inject]
    public BlockControllUseCase(
        MoveControlBlocksService moveControlBlocksService,
        PutControlBlocksService putControlBlocksService,
        GetNextControlBlocksService getNextControlBlocksService,
        ControlBlocksPresenter controlBlocksPresenter,
        BoardPresenter boardPresenter
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
        ControlBlocksPresenter = controlBlocksPresenter;
        BoardPresenter = boardPresenter;
    }

    public ControlBlocks Execute(
        ControlBlocks controlBlocks,
        float horizontal,
        float vertical,
        bool inputLeftSpin,
        bool inputRightSpin
    )
    {
        var newControlBlocks = MoveControlBlocksService.Execute(
            controlBlocks,
            horizontal,
            vertical,
            inputLeftSpin,
            inputRightSpin
        );

        if (newControlBlocks.IsPutable)
        {
            var addBlocks = PutControlBlocksService.Execute(newControlBlocks);
            BoardPresenter.AddBlocks(addBlocks);

            newControlBlocks = GetNextControlBlocksService.Execute();
        }

        ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);

        return newControlBlocks;
    }
}
