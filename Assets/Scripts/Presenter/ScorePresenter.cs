using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

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
}
