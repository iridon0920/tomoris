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
    private float ControlWaitSeconds;
    [SerializeField]
    private float MoveDownBySeconds;

    [Inject]
    private readonly BlockControllUseCase BlockControllUseCase;
    [Inject]
    private readonly GetNextControlBlocksService GetNextControlBlocksService;
    [Inject]
    private readonly GameOverEvent GameOverEvent;
    private ControlBlocks ControlBlocks;

    private float Horizontal;
    private float Vertical;
    private bool IsWaitMove = false;

    private bool IsGameOver = false;

    void Start()
    {
        ControlBlocks = GetNextControlBlocksService.Execute();

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
        if (!IsWaitMove && !IsGameOver)
        {
            ReceiveInput();
        }
    }

    private void ReceiveInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            IsWaitMove = true;
            ControlBlocks = BlockControllUseCase.MoveLeft(ControlBlocks);
            StartCoroutine(WaitControl());
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            IsWaitMove = true;
            ControlBlocks = BlockControllUseCase.MoveRight(ControlBlocks);
            StartCoroutine(WaitControl());
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            IsWaitMove = true;
            ControlBlocks = BlockControllUseCase.MoveDown(ControlBlocks);
            StartCoroutine(WaitControl());
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            IsWaitMove = true;
            ControlBlocks = BlockControllUseCase.SpinLeft(ControlBlocks);
            StartCoroutine(WaitControl());
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            IsWaitMove = true;
            ControlBlocks = BlockControllUseCase.SpinRight(ControlBlocks);
            StartCoroutine(WaitControl());
        }
    }
    private IEnumerator WaitControl()
    {
        yield return new WaitForSeconds(ControlWaitSeconds);
        IsWaitMove = false;
    }

    private IEnumerator MoveDownOverTime()
    {
        while (!IsGameOver)
        {
            yield return new WaitForSeconds(MoveDownBySeconds);
            ControlBlocks = BlockControllUseCase.MoveDown(ControlBlocks);
        }
    }

    public class Factory : PlaceholderFactory<BlockController>
    { }
}
