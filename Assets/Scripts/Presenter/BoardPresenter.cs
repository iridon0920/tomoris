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

    void Awake()
    {
        Board.RxFallBlock.Where(block => block != null).Subscribe(block => BoardView.ChangeBoardBlockPosition(block));
    }

    public void AddBlocks(List<BoardBlock> blocks)
    {
        Debug.Log(blocks.Count);
        blocks.ForEach(block =>
            {
                Debug.Log(BoardView);
                BoardView.DrawBoardBlock(block);
            });
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
