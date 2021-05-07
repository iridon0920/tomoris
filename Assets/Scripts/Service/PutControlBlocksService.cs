using Zenject;
using System.Collections.Generic;

public class PutControlBlocksService
{
    private readonly IBoard Board;
    private readonly CollisionDetection CollisionDetection;
    private readonly BoardPresenter BoardPresenter;
    private readonly GetNextControlBlocksService GetNextControlBlocksService;



    [Inject]
    public PutControlBlocksService(
        IBoard board,
        CollisionDetection collisionDetection,
        BoardPresenter boardPresenter,
        GetNextControlBlocksService getNextControlBlocksService
    )
    {
        Board = board;
        CollisionDetection = collisionDetection;
        BoardPresenter = boardPresenter;
        GetNextControlBlocksService = getNextControlBlocksService;
    }

    public ControlBlocks Execute(ControlBlocks controlBlocks)
    {
        if (CollisionDetection.IsCollisionPutPosition(controlBlocks))
        {
            controlBlocks.MoveUp();
            var addBlocks = Board.PutBlocks(controlBlocks);
            BoardPresenter.AddBlocks(addBlocks);
            return GetNextControlBlocksService.Execute();
        }

        return controlBlocks;
    }
}
