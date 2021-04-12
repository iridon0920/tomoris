using Zenject;

public class BoardBlocksLineEraseUseCase
{
    private readonly IBoard Board;
    private readonly BoardPresenter BoardPresenter;

    [Inject]
    public BoardBlocksLineEraseUseCase(IBoard board, BoardPresenter boardPresenter)
    {
        Board = board;
        BoardPresenter = boardPresenter;
    }

    public void Execute()
    {
        var eraseBlocks = Board.EraseIfAlign();
        BoardPresenter.DeleteEraseLineBlocks(eraseBlocks);
    }
}
