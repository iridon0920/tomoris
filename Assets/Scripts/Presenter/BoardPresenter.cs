using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class BoardPresenter
{
    private readonly BoardView BoardView;

    [Inject]
    public BoardPresenter(BoardView boardView)
    {
        BoardView = boardView;
    }

    public void AddBlocks(List<BoardPutBlock> blocks)
    {
        blocks.ForEach(block => BoardView.DrawBoardPutBlock(block));

    }

    public void DeleteEraseLineBlocks(List<BoardPutBlock> blocks)
    {
        blocks.ForEach(block => BoardView.DeleteBoardPutBlock(block));
    }

    public void FallBlocks(List<BoardPutBlock> blocks)
    {
        blocks.ForEach(block => BoardView.ChangeBoardPutBlockPosition(block));
    }
}
