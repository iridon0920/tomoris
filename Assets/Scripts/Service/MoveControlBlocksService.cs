using Zenject;

public class MoveControlBlocksService
{
    private readonly ControlBlocksAdjuster Adjuster;

    [Inject]
    public MoveControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks MoveRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveRight();
        return Adjuster.AdjustBlocksForSideMove(controlBlocks, newControlBlocks);
    }

    public ControlBlocks MoveLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveLeft();
        return Adjuster.AdjustBlocksForSideMove(controlBlocks, newControlBlocks);
    }

    public ControlBlocks MoveDown(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveDown();
        return Adjuster.AdjustBlocksForDownMove(controlBlocks, newControlBlocks);
    }

    public ControlBlocks SpinRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.RightSpin();
        return Adjuster.AdjustBlocksForSpin(controlBlocks, newControlBlocks);
    }

    public ControlBlocks SpinLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.LeftSpin();
        return Adjuster.AdjustBlocksForSpin(controlBlocks, newControlBlocks);
    }
}
