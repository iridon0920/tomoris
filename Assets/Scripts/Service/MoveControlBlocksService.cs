using Zenject;

public class MoveControlBlocksService
{
    private readonly ControlBlocksAdjuster Adjuster;

    [Inject]
    public MoveControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks, float horizontal, float vertical)
    {
        var newControlBlocks = controlBlocks.Clone();

        if (vertical < 0)
        {
            newControlBlocks.MoveDown();
            return Adjuster.AdjustBlocksForDownMove(controlBlocks, newControlBlocks);
        }
        else if (horizontal > 0)
        {
            newControlBlocks.MoveRight();
        }
        else if (horizontal < 0)
        {
            newControlBlocks.MoveLeft();
        }

        return Adjuster.AdjustBlocksForSideMove(controlBlocks, newControlBlocks);
    }
}
