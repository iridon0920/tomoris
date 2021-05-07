public class MoveDownControlBlocksService : IMoveControlBlocksService
{
    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        controlBlocks.MoveDown();
        return controlBlocks;
    }
}
