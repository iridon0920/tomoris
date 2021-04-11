using Zenject;
using UniRx;

public class BlockControllUseCase
{
    private MoveControlBlocksService MoveControlBlocksService;
    private CollisionDetection CollisionDetection;
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
        CollisionDetection collisionDetection,
        IBlocksQueue queue,
        IBoard board
    )
    {
        MoveControlBlocksService = moveControlBlocksService;
        CollisionDetection = collisionDetection;
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
        if (CollisionDetection.IsCollisionPutPosition(ControlBlocks.Value))
        {
            ControlBlocks.Value.MoveUp();
            Board.PutBlocks(ControlBlocks.Value);
            ControlBlocks.Value = new ControlBlocks(
                Board.InsertPositionX,
                Board.Height - 1,
                Queue.Dequeue()
            );
        }
    }
}
