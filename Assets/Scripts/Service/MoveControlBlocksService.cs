using Zenject;

public class MoveControlBlocksService
{
    private readonly ControlBlocksAdjuster Adjuster;

    [Inject]
    public MoveControlBlocksService(ControlBlocksAdjuster adjuster)
    {
        Adjuster = adjuster;
    }

    public ControlBlocks Execute(
        ControlBlocks controlBlocks,
        float horizontal,
        float vertical,
        bool inputLeftSpin,
        bool InputRightSpin
    )
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
            return Adjuster.AdjustBlocksForSideMove(controlBlocks, newControlBlocks);
        }
        else if (horizontal < 0)
        {
            newControlBlocks.MoveLeft();
            return Adjuster.AdjustBlocksForSideMove(controlBlocks, newControlBlocks);
        }
        else if (inputLeftSpin)
        {
            newControlBlocks.LeftSpin();
            return Adjuster.AdjustBlocksForSpin(controlBlocks, newControlBlocks);
        }
        else if (InputRightSpin)
        {
            newControlBlocks.RightSpin();
            return Adjuster.AdjustBlocksForSpin(controlBlocks, newControlBlocks);
        }

        return controlBlocks;
    }
}
