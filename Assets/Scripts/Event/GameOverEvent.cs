using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class GameOverEvent
{
    private ReactiveProperty<bool> IsGameOver;
    public IReadOnlyReactiveProperty<bool> GameOverObservable
    {
        get { return IsGameOver; }
    }

    public GameOverEvent()
    {
        IsGameOver = new ReactiveProperty<bool>(false);
    }

    public void EmitGameOver()
    {
        IsGameOver.Value = true;
    }
}
