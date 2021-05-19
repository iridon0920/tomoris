using Zenject;

public class BlockControllUseCase
{
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly EraseLineService EraseLineService;
    private readonly IBoard Board;
    private readonly GameOverEvent GameOverEvent;
    private readonly IControlBlocksPresenter ControlBlocksPresenter;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private readonly ControlBlocksAdjuster Adjuster;


    [Inject]
    public BlockControllUseCase(
        PutControlBlocksService putControlBlocksService,
        GetNextControlBlocksService getNextControlBlocksService,
        EraseLineService eraseLineService,
        IBoard board,
        GameOverEvent gameOverEvent,
        IControlBlocksPresenter controlBlocksPresenter,
        ControlBlocksAdjuster adjuster
    )
    {
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
        EraseLineService = eraseLineService;
        Board = board;
        GameOverEvent = gameOverEvent;
        ControlBlocksPresenter = controlBlocksPresenter;
        Adjuster = adjuster;
    }

    public ControlBlocks Execute(IMoveControlBlocksService moveService, int playerId, ControlBlocks controlBlocks)
    {
        var movedControlBlocks = moveService.Execute(controlBlocks);

        if (PutControlBlocksService.Execute(movedControlBlocks))
        {
            EraseLineService.Execute();
            if (Board.IsGameOver())
            {
                GameOverEvent.EmitGameOver();
            }
            else
            {
                var newControlBlocks = GetNextControlBlocksService.Execute(playerId);
                return newControlBlocks;
            }
        }
        var adjustedControlBlocks = Adjuster.AdjustBlocksByPutBlocks(movedControlBlocks);
        ControlBlocksPresenter.ChangeControlBlocks(adjustedControlBlocks);
        return adjustedControlBlocks;
    }
}
