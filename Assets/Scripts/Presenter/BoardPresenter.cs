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

    void Awake()
    {
        Board
            .RxBlocks
            .ObserveAdd()
            .Subscribe(block => BoardView.DrawBoardBlock(block.Value));

        Board
            .RxBlocks
            .ObserveRemove()
            .Subscribe(block => BoardView.DeleteBoardBlock(block.Value));

        Board.RxFallBlock.Where(block => block != null).Subscribe(block => BoardView.ChangeBoardBlockPosition(block));
    }
}
