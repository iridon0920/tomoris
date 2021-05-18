using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class ScorePresenter
{
    const float PLAYER_1_POSITION_X = -124f;
    const float PLAYER_2_POSITION_X = 180f;
    const float COMMON_POSITION_Y = -225f;
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
            ScoreView.ChangePointsPosition(PLAYER_1_POSITION_X, COMMON_POSITION_Y);
        }
        else if (playerId == 2)
        {
            ScoreView.ChangePointsPosition(PLAYER_2_POSITION_X, COMMON_POSITION_Y);
        }
    }
}
