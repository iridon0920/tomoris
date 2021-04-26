using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class UpdateScoreEvent
{
    private ReactiveProperty<int> Score;
    public IReadOnlyReactiveProperty<int> ScoreObservable
    {
        get { return Score; }
    }

    public UpdateScoreEvent()
    {
        Score = new ReactiveProperty<int>(0);
    }

    public void EmitPoints(int points)
    {
        Score.Value = points;
    }
}
