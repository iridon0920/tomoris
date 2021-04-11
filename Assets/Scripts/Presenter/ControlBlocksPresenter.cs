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
    private readonly BlockControllUseCase BlockControllUseCase;
    private bool IsWaitMove = false;

    void Awake()
    {
        this.UpdateAsObservable()
            .Where(_ => (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                         && IsWaitMove == false)
            .Subscribe(_ =>
            {
                IsWaitMove = true;
                BlockControllUseCase.Execute(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
