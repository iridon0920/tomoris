using Zenject;
using UniRx;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
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
        GetNextControlBlocksService getNextControlBlocksService,
        IBlocksQueue queue,
        IBoard board,
        BoardPresenter boardPresenter
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        GetNextControlBlocksService = getNextControlBlocksService;
        Queue = queue;
        Board = board;
        BoardPresenter = boardPresenter;

        ControlBlocks = new ReactiveProperty<ControlBlocks>();
        ControlBlocks.Value = GetNextControlBlocksService.Execute();
    }

    public void Execute(float horizontal, float vertical)
    {
        ControlBlocks.Value = MoveControlBlocksService.Execute(ControlBlocks.Value, horizontal, vertical);
        var addBlocks = PutControlBlocksService.Execute(ControlBlocks.Value);
        BoardPresenter.AddBlocks(addBlocks);
        if (ControlBlocks.Value.IsPutable)
        {
            ControlBlocks.Value = GetNextControlBlocksService.Execute();
        }
    }
}
