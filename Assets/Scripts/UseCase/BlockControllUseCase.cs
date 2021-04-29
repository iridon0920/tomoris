using Zenject;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private readonly IBoard Board;
    private readonly ControlBlocksPresenter ControlBlocksPresenter;
    private readonly BoardPresenter BoardPresenter;
    private readonly GameOverEvent GameOverEvent;

    [Inject]
    public BlockControllUseCase(
        MoveControlBlocksService moveControlBlocksService,
        PutControlBlocksService putControlBlocksService,
        GetNextControlBlocksService getNextControlBlocksService,
        IBoard board,
        ControlBlocksPresenter controlBlocksPresenter,
        BoardPresenter boardPresenter,
        GameOverEvent gameOverEvent
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
        Board = board;
        ControlBlocksPresenter = controlBlocksPresenter;
        BoardPresenter = boardPresenter;
        GameOverEvent = gameOverEvent;
    }

    public ControlBlocks MoveRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.MoveRight(controlBlocks);
        return ProccessForAfterMove(newControlBlocks);
    }
    public ControlBlocks MoveLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.MoveLeft(controlBlocks);
        return ProccessForAfterMove(newControlBlocks);
    }
    public ControlBlocks MoveDown(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.MoveDown(controlBlocks);
        return ProccessForAfterMove(newControlBlocks);
    }
    public ControlBlocks SpinRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.SpinRight(controlBlocks);
        return ProccessForAfterMove(newControlBlocks);
    }
    public ControlBlocks SpinLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.SpinLeft(controlBlocks);
        return ProccessForAfterMove(newControlBlocks);
    }

    private ControlBlocks ProccessForAfterMove(ControlBlocks controlBlocks)
    {
        if (controlBlocks.IsPutable)
        {
            var addBlocks = PutControlBlocksService.Execute(controlBlocks);
            BoardPresenter.AddBlocks(addBlocks);

            controlBlocks = GetNextControlBlocksService.Execute();

            var eraseBlocks = Board.EraseIfAlign();
            BoardPresenter.DeleteEraseLineBlocks(eraseBlocks);

            var fallBlocks = Board.FallToEmptyLine();
            BoardPresenter.FallBlocks(fallBlocks);

        }

        if (Board.IsGameOver())
        {
            GameOverEvent.EmitGameOver();
        }
        else
        {
            ControlBlocksPresenter.DrawControlBlocks(controlBlocks);
        }

        return controlBlocks;
    }
}
