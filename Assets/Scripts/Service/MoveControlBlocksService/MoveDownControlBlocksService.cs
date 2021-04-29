using Zenject;
public class MoveDownControlBlocksService : IMoveControlBlocksService
{
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
    public MoveDownControlBlocksService(
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

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveDown();

        if (CollisionDetection.IsCollisionPutPosition(newControlBlocks))
        {
            var addBlocks = PutControlBlocksService.Execute(controlBlocks);
            BoardPresenter.AddBlocks(addBlocks);
            if (Board.IsGameOver())
            {
                GameOverEvent.EmitGameOver();
                return controlBlocks;
            }

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

        return newControlBlocks;
    }
}
