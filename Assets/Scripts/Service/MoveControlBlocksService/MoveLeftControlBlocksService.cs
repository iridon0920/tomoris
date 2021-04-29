using Zenject;
public class MoveLeftControlBlocksService : IMoveControlBlocksService
{
    private readonly CollisionDetection CollisionDetection;

    [Inject]
    public MoveLeftControlBlocksService(CollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveLeft();

        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return controlBlocks;
        }
        return newControlBlocks;
    }
}
