using Zenject;
public class MoveRightControlBlocksService : IMoveControlBlocksService
{
    private readonly CollisionDetection CollisionDetection;
    private readonly IControlBlocksPresenter ControlBlocksPresenter;

    [Inject]
    public MoveRightControlBlocksService(
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
        newControlBlocks.MoveRight();

        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            ControlBlocksPresenter.PlayCollisionSound();
            return controlBlocks;
        }
        return newControlBlocks;
    }
}
