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
    [Inject]
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private ControlBlocks ControlBlocks;
    private bool IsWaitMove = false;

    void Awake()
    {
        ControlBlocks = GetNextControlBlocksService.Execute();
        ControlBlocksView.DrawControlBlocks(ControlBlocks);

        this.UpdateAsObservable()
            .Where(_ => (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                         && IsWaitMove == false)
            .Subscribe(_ =>
            {
                IsWaitMove = true;
                ControlBlocks = BlockControllUseCase.Execute(ControlBlocks, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                ControlBlocksView.DrawControlBlocks(ControlBlocks);
            });
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
