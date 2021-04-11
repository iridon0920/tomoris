using Zenject;

public class MoveControlBlocksService
{
    private ControlBlocksAdjuster Adjuster;

    [Inject]
    public MoveControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks, float horizontal, float vertical)
    {
        var newControlBlocks = controlBlocks.Clone();

        if (horizontal > 0)
        {
            newControlBlocks.MoveRight();
            return Adjuster.AdjustBlocksForMove(controlBlocks, newControlBlocks);
        }
        else if (horizontal < 0)
        {
            newControlBlocks.MoveLeft();
            return Adjuster.AdjustBlocksForMove(controlBlocks, newControlBlocks);
        }
        else if (vertical < 0)
        {
            newControlBlocks.MoveDown();
            return newControlBlocks;
        }

        return controlBlocks;
    }
}
