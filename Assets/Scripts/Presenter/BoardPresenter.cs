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
    [Inject]
    private readonly BoardBlocksLineEraseUseCase BoardBlocksLineEraseUseCase;
    [Inject]
    private readonly FallBoardBlocksUseCase FallBoardBlocksUseCase;


    public void AddBlocks(List<BoardBlock> blocks)
    {
        blocks.ForEach(block => BoardView.DrawBoardBlock(block));

        BoardBlocksLineEraseUseCase.Execute();
    }

    public void DeleteEraseLineBlocks(List<BoardBlock> blocks)
    {
        blocks.ForEach(block => BoardView.DeleteBoardBlock(block));

        FallBoardBlocksUseCase.Execute();
    }

    public void FallBlocks(List<BoardBlock> blocks)
    {
        blocks.ForEach(block => BoardView.ChangeBoardBlockPosition(block));
    }
}
