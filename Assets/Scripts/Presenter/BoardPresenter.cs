using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class BoardPresenter : MonoBehaviour
{
    [SerializeField]
    private BoardView BoardView;

    [Inject]
    private readonly IBoard Board;

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
