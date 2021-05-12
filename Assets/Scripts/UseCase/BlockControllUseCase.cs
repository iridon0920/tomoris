using Zenject;

public class BlockControllUseCase
{
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly EraseLineService EraseLineService;
    private readonly IBoard Board;
    private readonly GameOverEvent GameOverEvent;
    private readonly IControlBlocksPresenter ControlBlocksPresenter;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;



    [Inject]
    public BlockControllUseCase(
        PutControlBlocksService putControlBlocksService,
        GetNextControlBlocksService getNextControlBlocksService,
        EraseLineService eraseLineService,
        IBoard board,
        GameOverEvent gameOverEvent,
        IControlBlocksPresenter controlBlocksPresenter
    )
    {
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
        EraseLineService = eraseLineService;
        Board = board;
        GameOverEvent = gameOverEvent;
        ControlBlocksPresenter = controlBlocksPresenter;
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
                var newControlBlocks = GetNextControlBlocksService.Execute();
                return newControlBlocks;
            }
        }

        ControlBlocksPresenter.ChangeControlBlocks(movedControlBlocks);
        return movedControlBlocks;
    }
}
