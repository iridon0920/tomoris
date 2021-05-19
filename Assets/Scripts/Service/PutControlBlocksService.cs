using Zenject;
using System.Collections.Generic;

public class PutControlBlocksService
{
    private readonly IBoard Board;
    private readonly CollisionDetection CollisionDetection;
    private readonly BoardPresenter BoardPresenter;
    private readonly ControlBlocksAdjuster Adjuster;

    [Inject]
    public PutControlBlocksService(
        IBoard board,
        CollisionDetection collisionDetection,
        BoardPresenter boardPresenter,
        ControlBlocksAdjuster adjuster
    )
    {
        Board = board;
        CollisionDetection = collisionDetection;
        BoardPresenter = boardPresenter;
        Adjuster = adjuster;
    }

    public bool Execute(ControlBlocks controlBlocks)
    {
        if (CollisionDetection.IsCollisionPutPosition(controlBlocks))
        {
            var adjustedControlBlocks = Adjuster.AdjustBlocksByPutBlocks(controlBlocks);
            var addBlocks = Board.PutBlocks(adjustedControlBlocks);
            BoardPresenter.AddBlocks(addBlocks);
            return true;
        }

        return false;
    }
}
