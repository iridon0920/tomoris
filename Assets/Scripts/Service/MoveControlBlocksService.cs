using Zenject;

public class MoveControlBlocksService
{
    private ControlBlocksAdjuster Adjuster;

    [Inject]
    public MoveControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks MoveRight(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveRight();
        return Adjuster.AdjustBlocksForMove(controlBlocks, newControlBlocks);

    }

    public ControlBlocks MoveLeft(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveLeft();
        return Adjuster.AdjustBlocksForMove(controlBlocks, newControlBlocks);
    }

    public ControlBlocks MoveDown(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveDown();
        return newControlBlocks;
    }
}
