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
    private IControlBlocksAdjuster ControlBlocksAdjuster;
    private IBlocksQueue Queue;
    private IBoard Board;
    private ReactiveProperty<IControlBlocks> ControlBlocks;
    public IReadOnlyReactiveProperty<IControlBlocks> RxControlBlocks
    {
        get { return ControlBlocks; }
    }

    [Inject]
    public BlockControllUseCase(IControlBlocksAdjuster controlBlocksAdjuster, IBlocksQueue queue, IBoard board)
    {
        ControlBlocksAdjuster = controlBlocksAdjuster;
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
        ControlBlocksAdjuster.AdjustBlocksForMoveLeft(newControlBlocks);

        ControlBlocks.Value = newControlBlocks;
    }

    public void MoveRight()
    {
        var newControlBlocks = ControlBlocks.Value.Clone();
        newControlBlocks.MoveRight();
        ControlBlocksAdjuster.AdjustBlocksForMoveRight(newControlBlocks);

        ControlBlocks.Value = newControlBlocks;
    }

    public void MoveDown()
    {
        var newControlBlocks = ControlBlocks.Value.Clone();
        newControlBlocks.MoveDown();
        ControlBlocksAdjuster.AdjustBlocksForMoveDown(newControlBlocks);

        ControlBlocks.Value = newControlBlocks;
    }
}
