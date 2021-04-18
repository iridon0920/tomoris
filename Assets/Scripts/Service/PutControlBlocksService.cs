using Zenject;
using System.Collections.Generic;

public class PutControlBlocksService
{
    private readonly IBoard Board;

    [Inject]
    public PutControlBlocksService(IBoard board)
    {
        Board = board;
    }

    public List<BoardBlock> Execute(ControlBlocks controlBlocks)
    {
        return Board.PutBlocks(controlBlocks);
    }
}
