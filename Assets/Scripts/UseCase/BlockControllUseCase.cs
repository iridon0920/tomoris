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
            ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        }

        return newControlBlocks;
    }
}
