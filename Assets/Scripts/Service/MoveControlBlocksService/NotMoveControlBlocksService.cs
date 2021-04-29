using Zenject;
public class NotMoveControlBlocksService : IMoveControlBlocksService
{
    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        return controlBlocks;
    }

}
