using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class ScorePresenter
{
    private readonly ScoreView ScoreView;

    [Inject]
    public ScorePresenter(ScoreView scoreView)
    {
        ScoreView = scoreView;
    }

    public void UpdatePoints(int points)
    {
        ScoreView.UpdatePointsText(points);
    }

    public void ChangeScorePositionByPlayerId(int playerId)
    {
        if (playerId == 1)
        {
            ScoreView.ChangePointsPosition(-124, 82);
        }
        else if (playerId == 2)
        {
            ScoreView.ChangePointsPosition(180, 82);
        }
    }
}
