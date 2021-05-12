using Zenject;
public class MoveLeftControlBlocksService : IMoveControlBlocksService
{
    private readonly CollisionDetection CollisionDetection;
    private readonly IControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public MoveLeftControlBlocksService(
        CollisionDetection collisionDetection,
        IControlBlocksPresenter controlBlocksPresenter
    )
    {
        CollisionDetection = collisionDetection;
        ControlBlocksPresenter = controlBlocksPresenter;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        var newControlBlocks = controlBlocks.Clone();
        newControlBlocks.MoveLeft();

        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            ControlBlocksPresenter.PlayCollisionSound();
            return controlBlocks;
        }
        return newControlBlocks;
    }
}
