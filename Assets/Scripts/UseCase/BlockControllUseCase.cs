using Zenject;

public class BlockControllUseCase
{
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly EraseLineService EraseLineService;
    private readonly IBoard Board;
    private readonly GameOverEvent GameOverEvent;
    private readonly ControlBlocksPresenter ControlBlocksPresenter;


    [Inject]
    public BlockControllUseCase(
        PutControlBlocksService putControlBlocksService,
        EraseLineService eraseLineService,
        IBoard board,
        GameOverEvent gameOverEvent,
        ControlBlocksPresenter controlBlocksPresenter
    )
    {
        PutControlBlocksService = putControlBlocksService;
        EraseLineService = eraseLineService;
        Board = board;
        GameOverEvent = gameOverEvent;
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public ControlBlocks Execute(IMoveControlBlocksService moveService, int playerId, ControlBlocks controlBlocks)
    {
        var movedControlBlocks = moveService.Execute(controlBlocks);

        var newControlBlocks = PutControlBlocksService.Execute(movedControlBlocks);

        EraseLineService.Execute();

        if (Board.IsGameOver())
        {
            GameOverEvent.EmitGameOver();
        }
        else
        {
            ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        }

        return newControlBlocks;
    }
}
