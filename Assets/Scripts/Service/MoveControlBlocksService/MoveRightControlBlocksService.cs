using Zenject;
public class MoveRightControlBlocksService : IMoveControlBlocksService
{
    private readonly CollisionDetection CollisionDetection;

    [Inject]
    public MoveRightControlBlocksService(CollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveRight();

        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return controlBlocks;
        }
        return newControlBlocks;
    }
}
