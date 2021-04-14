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
    private float ControlWaitSecond;

    [Inject]
    private readonly BlockControllUseCase BlockControllUseCase;
    [Inject]
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    private ControlBlocks ControlBlocks;

    private float Horizontal;
    private float Vertical;
    private bool IsWaitMove = false;

    void Awake()
    {
        ControlBlocks = GetNextControlBlocksService.Execute();
        ControlBlocksView.DrawControlBlocks(ControlBlocks);
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveControlBlocks(true, false);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MoveControlBlocks(false, true);
        }
    }

    void FixedUpdate()
    {
        if (!IsWaitMove && (Horizontal != 0 || Vertical != 0))
        {
            IsWaitMove = true;
            MoveControlBlocks(false, false);
            StartCoroutine(WaitControl());
        }
    }

    void MoveControlBlocks(bool inputLeftSpin, bool inputRightSpin)
    {
        ControlBlocks = BlockControllUseCase.Execute(
            ControlBlocks,
            Horizontal,
            Vertical,
            inputLeftSpin,
            inputRightSpin
        );
        ControlBlocksView.DrawControlBlocks(ControlBlocks);
    }
    private IEnumerator WaitControl()
    {
        yield return new WaitForSeconds(ControlWaitSecond);
        IsWaitMove = false;
    }
}
