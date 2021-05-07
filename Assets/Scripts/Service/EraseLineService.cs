using Zenject;
using System.Collections.Generic;

public class EraseLineService
{
    private readonly IBoard Board;
    private readonly BoardPresenter BoardPresenter;
    private readonly Score Score;
    private readonly ScorePresenter ScorePresenter;

    public EraseLineService(
        IBoard board,
        BoardPresenter boardPresenter,
        Score score,
        ScorePresenter scorePresenter
    )
    {
        Board = board;
        BoardPresenter = boardPresenter;
        Score = score;
        ScorePresenter = scorePresenter;
    }

    public void Execute()
    {
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
}
