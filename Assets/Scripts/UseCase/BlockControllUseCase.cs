using Zenject;
using UniRx;

public class BlockControllUseCase
{
    private readonly MoveControlBlocksService MoveControlBlocksService;
    private readonly PutControlBlocksService PutControlBlocksService;
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
        IBoard board
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        PutControlBlocksService = putControlBlocksService;
        Queue = queue;
        Board = board;

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
        if (PutControlBlocksService.Execute(ControlBlocks.Value))
        {
            ControlBlocks.Value = new ControlBlocks(
                Board.InsertPositionX,
                Board.Height - 1,
                Queue.Dequeue()
            );
        }
    }
}
