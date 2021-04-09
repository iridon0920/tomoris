using Zenject;
using UniRx;

public interface IBlockControllUseCase
{
    IReadOnlyReactiveProperty<IControlBlocks> RxControlBlocks { get; }
    void MoveLeft();
    void MoveRight();
    void MoveDown();

}
public class BlockControllUseCase : IBlockControllUseCase
{
    private IControlBlocksAdjuster Adjuster;
    private ICollisionDetection CollisionDetection;
    private IBlocksQueue Queue;
    private IBoard Board;
    private ReactiveProperty<IControlBlocks> ControlBlocks;
    public IReadOnlyReactiveProperty<IControlBlocks> RxControlBlocks
    {
        get { return ControlBlocks; }
    }

    [Inject]
    public BlockControllUseCase(
        IControlBlocksAdjuster adjuster,
        ICollisionDetection collisionDetection,
        IBlocksQueue queue,
        IBoard board
    )
    {
        Adjuster = adjuster;
        CollisionDetection = collisionDetection;
        Queue = queue;
        Board = board;

        ControlBlocks = new ReactiveProperty<IControlBlocks>();
        ControlBlocks.Value = new ControlBlocks(
            Board.InsertPositionX,
            Board.Height - 1,
            Queue.Dequeue()
        );
    }

    public void MoveLeft()
    {
        var newControlBlocks = ControlBlocks.Value.Clone();
        newControlBlocks.MoveLeft();
        Adjuster.AdjustBlocksForMoveLeft(newControlBlocks);

        ControlBlocks.Value = newControlBlocks;
    }

    public void MoveRight()
    {
        var newControlBlocks = ControlBlocks.Value.Clone();
        newControlBlocks.MoveRight();
        Adjuster.AdjustBlocksForMoveRight(newControlBlocks);

        ControlBlocks.Value = newControlBlocks;
    }

    public void MoveDown()
    {
        var newControlBlocks = ControlBlocks.Value.Clone();
        newControlBlocks.MoveDown();
        if (CollisionDetection.IsCollisionPutPosition(newControlBlocks))
        {
            Board.PutBlocks(ControlBlocks.Value);
            ControlBlocks.Value = new ControlBlocks(
                Board.InsertPositionX,
                Board.Height - 1,
                Queue.Dequeue()
            );
        }
        else
        {
            ControlBlocks.Value = newControlBlocks;
        }
    }
}
