using Zenject;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private readonly BoardPresenter BoardPresenter;

    [Inject]
    public BlockControllUseCase(
        MoveControlBlocksService moveControlBlocksService,
        PutControlBlocksService putControlBlocksService,
        GetNextControlBlocksService getNextControlBlocksService,
        BoardPresenter boardPresenter
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
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

        var addBlocks = PutControlBlocksService.Execute(newControlBlocks);
        BoardPresenter.AddBlocks(addBlocks);

        if (newControlBlocks.IsPutable)
        {
            newControlBlocks = GetNextControlBlocksService.Execute();
        }
        return newControlBlocks;
    }
}
