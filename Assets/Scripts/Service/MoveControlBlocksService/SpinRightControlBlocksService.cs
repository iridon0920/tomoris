using Zenject;
public class SpinRightControlBlocksService : IMoveControlBlocksService
{
    private readonly ControlBlocksAdjuster Adjuster;

    [Inject]
    public SpinRightControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.RightSpin();

        return Adjuster.AdjustBlocksForSpin(controlBlocks, newControlBlocks);
    }

}
