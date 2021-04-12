using Zenject;

public class FallBoardBlocksUseCase
{
    private readonly IBoard Board;
    private readonly BoardPresenter BoardPresenter;

    [Inject]
    public FallBoardBlocksUseCase(IBoard board, BoardPresenter boardPresenter)
    {
        Board = board;
        BoardPresenter = boardPresenter;
    }

    public void Execute()
    {
        var fallBlocks = Board.FallToEmptyLine();
        BoardPresenter.FallBlocks(fallBlocks);
    }
}
