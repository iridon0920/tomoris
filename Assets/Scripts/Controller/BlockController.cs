using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    private int QueueSize = 4;
    [SerializeField]
    private float ControlWaitSeconds;
    [SerializeField]
    private float MoveDownBySeconds;

    [Inject]
    private readonly BlockControllUseCase BlockControllUseCase;
    [Inject]
    private readonly InitializeBlocksQueueService InitializeBlocksQueueService;
    [Inject]
    private readonly GetNextControlBlocksService GetNextControlBlocksService;

    [Inject]
    private readonly NotMoveControlBlocksService NotMoveControlBlocksService;
    [Inject]
    private readonly MoveLeftControlBlocksService MoveLeftControlBlocksService;
    [Inject]
    private readonly MoveRightControlBlocksService MoveRightControlBlocksService;
    [Inject]
    private readonly MoveDownControlBlocksService MoveDownControlBlocksService;
    [Inject]
    private readonly SpinLeftControlBlocksService SpinLeftControlBlocksService;
    [Inject]
    private readonly SpinRightControlBlocksService SpinRightControlBlocksService;

    [Inject]
    private readonly GameOverEvent GameOverEvent;

    [Inject]
    private readonly InitializeUiUseCase InitializeUiUseCase;
    private ControlBlocks ControlBlocks;

    public int PlayerId;

    private bool IsWaitMove = false;

    private bool IsGameOver = false;

    async void Awake()
    {

        await InitializeBlocksQueueService.Execute(QueueSize);
        ControlBlocks = GetNextControlBlocksService.Execute();
    }

    void Start()
    {
        InitializeUiUseCase.Execute(PlayerId);

        StartCoroutine(MoveDownOverTime());

        GameOverEvent
            .GameOverObservable
            .Subscribe(isGameOver =>
                {
                    if (isGameOver)
                    {
                        IsGameOver = isGameOver;
                    }
                }
            );
    }

    void Update()
    {
        if (!IsGameOver)
        {
            ExecuteMoveService(ReceiveSpinInput());
            if (!IsWaitMove)
            {
                ExecuteMoveServiceAfterWait(ReceiveMoveInput());
            }
        }
    }

    private void ExecuteMoveService(IMoveControlBlocksService moveService)
    {
        if (ControlBlocks is null)
        {
            return;
        }
        ControlBlocks = BlockControllUseCase.Execute(moveService, PlayerId, ControlBlocks);
    }

    private void ExecuteMoveServiceAfterWait(IMoveControlBlocksService moveService)
    {
        ExecuteMoveService(moveService);
        IsWaitMove = true;
        StartCoroutine(WaitControl());
    }

    private IMoveControlBlocksService ReceiveMoveInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return MoveLeftControlBlocksService;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            return MoveRightControlBlocksService;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            return MoveDownControlBlocksService;
        }

        return NotMoveControlBlocksService;
    }

    private IMoveControlBlocksService ReceiveSpinInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            return SpinLeftControlBlocksService;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            return SpinRightControlBlocksService;
        }

        return NotMoveControlBlocksService;
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
            if (!IsGameOver)
            {
                ExecuteMoveService(MoveDownControlBlocksService);
            }
            else
            {
                break;
            }
        }
    }
}
