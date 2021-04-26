using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private ScoreView ScoreView;
    [Inject]
    UpdateScoreEvent UpdateScoreEvent;

    void Awake()
    {
        UpdateScoreEvent
            .ScoreObservable
            .Subscribe(points => ScoreView.UpdatePointsText(points));
    }
}
