using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class ControlBlocksPresenter : MonoBehaviour
{
    [SerializeField]
    private ControlBlocksView ControlBlocksView;
    [SerializeField]
    private float moveWaitSecond = 0.2f;

    [Inject]
    private readonly IBlockControllUseCase BlockControllUseCase;
    private bool IsWaitMove = false;

    void Awake()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.RightArrow) && IsWaitMove == false)
            .Subscribe(_ =>
            {
                IsWaitMove = true;
                BlockControllUseCase.MoveRight();
            });

        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.LeftArrow) && IsWaitMove == false)
            .Subscribe(_ =>
            {
                IsWaitMove = true;
                BlockControllUseCase.MoveLeft();
            });

        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.DownArrow) && IsWaitMove == false)
            .Subscribe(_ =>
            {
                IsWaitMove = true;
                BlockControllUseCase.MoveDown();
            });

        BlockControllUseCase
            .RxControlBlocks
            .Where(controlBlocks => controlBlocks != null)
            .Subscribe(
                controlBlocks =>
                        ControlBlocksView.DrawControlBlocks(controlBlocks)
            );
    }

    void FixedUpdate()
    {
        if (IsWaitMove)
        {
            StartCoroutine(WaitMove());
        }
    }

    private IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(moveWaitSecond);
        IsWaitMove = false;
    }
}
