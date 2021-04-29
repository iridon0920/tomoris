using Zenject;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private readonly IBoard Board;
    private readonly CollisionDetection CollisionDetection;
    private readonly ControlBlocksPresenter ControlBlocksPresenter;
    private readonly BoardPresenter BoardPresenter;
    private readonly GameOverEvent GameOverEvent;
    private readonly Score Score;
    private readonly ScorePresenter ScorePresenter;

    [Inject]
    public BlockControllUseCase(
        MoveControlBlocksService moveControlBlocksService,
        PutControlBlocksService putControlBlocksService,
        GetNextControlBlocksService getNextControlBlocksService,
        IBoard board,
        CollisionDetection collisionDetection,
        ControlBlocksPresenter controlBlocksPresenter,
        BoardPresenter boardPresenter,
        GameOverEvent gameOverEvent,
        Score score,
        ScorePresenter scorePresenter
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
        Board = board;
        CollisionDetection = collisionDetection;
        ControlBlocksPresenter = controlBlocksPresenter;
        BoardPresenter = boardPresenter;
        GameOverEvent = gameOverEvent;
        Score = score;
        ScorePresenter = scorePresenter;
    }

    public ControlBlocks MoveRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.MoveRight(controlBlocks);
        ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        return newControlBlocks;
    }
    public ControlBlocks MoveLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.MoveLeft(controlBlocks);
        ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        return newControlBlocks;
    }
    public ControlBlocks MoveDown(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.MoveDown(controlBlocks);
        if (CollisionDetection.IsCollisionPutPosition(newControlBlocks))
        {
            var addBlocks = PutControlBlocksService.Execute(controlBlocks);
            BoardPresenter.AddBlocks(addBlocks);

            newControlBlocks = GetNextControlBlocksService.Execute();

            var eraseBlocks = Board.EraseIfAlign();
            BoardPresenter.DeleteEraseLineBlocks(eraseBlocks);
            if (eraseBlocks.Count > 0)
            {
                Score.AddPointFromErasedLines(eraseBlocks.Count / Board.Width);
                ScorePresenter.UpdatePoints((int)Score.TotalPoints);
            }

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
    public ControlBlocks SpinRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.SpinRight(controlBlocks);
        ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        return newControlBlocks;
    }
    public ControlBlocks SpinLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = MoveControlBlocksService.SpinLeft(controlBlocks);
        ControlBlocksPresenter.DrawControlBlocks(newControlBlocks);
        return newControlBlocks;
    }
}
