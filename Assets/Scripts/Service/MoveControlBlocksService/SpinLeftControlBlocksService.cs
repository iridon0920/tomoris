using Zenject;
public class SpinLeftControlBlocksService : IMoveControlBlocksService
{
    private readonly ControlBlocksAdjuster Adjuster;

    [Inject]
    public SpinLeftControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.LeftSpin();

        return Adjuster.AdjustBlocksForSpin(controlBlocks, newControlBlocks);
    }

}
