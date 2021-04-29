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

    public void AddBlocks(List<BoardBlock> blocks)
    {
        blocks.ForEach(block => BoardView.DrawBoardBlock(block));

    }

    public void DeleteEraseLineBlocks(List<BoardBlock> blocks)
    {
        blocks.ForEach(block => BoardView.DeleteBoardBlock(block));
    }

    public void FallBlocks(List<BoardBlock> blocks)
    {
        blocks.ForEach(block => BoardView.ChangeBoardBlockPosition(block));
    }
}
