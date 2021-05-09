using Zenject;
using System.Collections.Generic;

public class PutControlBlocksService
{
    private readonly IBoard Board;
    private readonly CollisionDetection CollisionDetection;
    private readonly BoardPresenter BoardPresenter;

    [Inject]
    public PutControlBlocksService(
        IBoard board,
        CollisionDetection collisionDetection,
        BoardPresenter boardPresenter
    )
    {
        Board = board;
        CollisionDetection = collisionDetection;
        BoardPresenter = boardPresenter;
    }

    public bool Execute(ControlBlocks controlBlocks)
    {
        if (CollisionDetection.IsCollisionPutPosition(controlBlocks))
        {
            controlBlocks.MoveUp();
            var addBlocks = Board.PutBlocks(controlBlocks);
            BoardPresenter.AddBlocks(addBlocks);
            return true;
        }

        return false;
    }
}
