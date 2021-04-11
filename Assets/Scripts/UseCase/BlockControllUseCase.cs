using Zenject;
using UniRx;

public interface IBlockControllUseCase
{
    IReadOnlyReactiveProperty<ControlBlocks> RxControlBlocks { get; }
    void MoveLeft();
    void MoveRight();
    void MoveDown();

}
public class BlockControllUseCase : IBlockControllUseCase
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

    public void MoveLeft()
    {
        ControlBlocks.Value = MoveControlBlocksService.MoveLeft(ControlBlocks.Value);
    }

    public void MoveRight()
    {
        ControlBlocks.Value = MoveControlBlocksService.MoveRight(ControlBlocks.Value);
    }

    public void MoveDown()
    {
        ControlBlocks.Value = MoveControlBlocksService.MoveDown(ControlBlocks.Value);
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
