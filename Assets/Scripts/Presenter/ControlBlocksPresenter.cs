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
    private float ControlWaitSeconds;
    [SerializeField]
    private float MoveDownBySeconds;

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

        StartCoroutine(MoveDownOverTime());
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveControlBlocks(0, 0, true, false);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MoveControlBlocks(0, 0, false, true);
        }
    }

    void FixedUpdate()
    {
        if (!IsWaitMove)
        {
            if (Horizontal != 0 || Vertical != 0)
            {
                IsWaitMove = true;
                MoveControlBlocks(Horizontal, Vertical, false, false);

                StartCoroutine(WaitControl());
            }
        }
    }

    void MoveControlBlocks(float horizontal, float vertical, bool inputLeftSpin, bool inputRightSpin)
    {
        ControlBlocks = BlockControllUseCase.Execute(
            ControlBlocks,
            horizontal,
            vertical,
            inputLeftSpin,
            inputRightSpin
        );
        ControlBlocksView.DrawControlBlocks(ControlBlocks);
    }
    private IEnumerator WaitControl()
    {
        yield return new WaitForSeconds(ControlWaitSeconds);
        IsWaitMove = false;
    }

    private IEnumerator MoveDownOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(MoveDownBySeconds);
            MoveControlBlocks(0, -1, false, false);
        }
    }
}
