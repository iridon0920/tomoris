using Zenject;
using UniRx;
public class BoardBlocksLineEraseUseCase
{
    private readonly IBoard Board;
    private readonly BoardPresenter BoardPresenter;
    private readonly Score Score;
    private readonly UpdateScoreEvent UpdateScoreEvent;

    [Inject]
    public BoardBlocksLineEraseUseCase(IBoard board, BoardPresenter boardPresenter, Score score, UpdateScoreEvent updateScoreEvent)
    {
        Board = board;
        BoardPresenter = boardPresenter;
        Score = score;
        UpdateScoreEvent = updateScoreEvent;
    }

    public void Execute()
    {
        var eraseBlocks = Board.EraseIfAlign();
        BoardPresenter.DeleteEraseLineBlocks(eraseBlocks);

        if (eraseBlocks.Count > 0)
        {
            Score.AddPointFromErasedLines(eraseBlocks.Count / Board.Width);
            UpdateScoreEvent.EmitPoints((int)Score.TotalPoints);
        }

    }
}
