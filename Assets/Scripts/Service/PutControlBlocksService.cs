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
        var result = new List<BoardBlock>();
        if (controlBlocks.IsPutable)
        {
            result = Board.PutBlocks(controlBlocks);
        }
        return result;
    }
}
