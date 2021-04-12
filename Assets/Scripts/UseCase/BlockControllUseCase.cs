using Zenject;
using UniRx;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly BoardPresenter BoardPresenter;
    private IBlocksQueue Queue;
    private IBoard Board;
    private ReactiveProperty<ControlBlocks> ControlBlocks;
    public IReadOnlyReactiveProperty<ControlBlocks> RxControlBlocks
    {
        get { return ControlBlocks; }
    }

    [Inject]
    public BlockControllUseCase(
        MoveControlBlocksService moveControlBlocksService,
        PutControlBlocksService putControlBlocksService,
        IBlocksQueue queue,
        IBoard board,
        BoardPresenter boardPresenter
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        Queue = queue;
        Board = board;
        BoardPresenter = boardPresenter;

        ControlBlocks = new ReactiveProperty<ControlBlocks>();
        ControlBlocks.Value = new ControlBlocks(
            Board.InsertPositionX,
            Board.Height - 1,
            Queue.Dequeue()
        );
    }

    public void Execute(float horizontal, float vertical)
    {
        ControlBlocks.Value = MoveControlBlocksService.Execute(ControlBlocks.Value, horizontal, vertical);
        var addBlocks = PutControlBlocksService.Execute(ControlBlocks.Value);
        if (addBlocks.Count > 0)
        {
            BoardPresenter.AddBlocks(addBlocks);
            ControlBlocks.Value = new ControlBlocks(
                Board.InsertPositionX,
                Board.Height - 1,
                Queue.Dequeue()
            );
        }
    }
}
