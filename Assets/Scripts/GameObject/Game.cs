using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Zenject;
using DG.Tweening;
public class Game : MonoBehaviour
{
    [Inject]
    private Player.Factory PlayerFactory;
    private List<Player> Players = new List<Player>();
    public int PlayerCount { get; private set; }

    void Awake()
    {
        DOTween.
            SetTweensCapacity(tweenersCapacity: 500, sequencesCapacity: 50);

        PlayerCount = TitleToGameDataSender.PlayerCount;

        for (var i = 1; i <= PlayerCount; i++)
        {
            Players.Add(PlayerFactory.Create(i));
        }
    }

    public int GetWinnerPlayerId()
    {
        double topScorePoints = Players
                            .Select(player => player.GetScoreTotalPoints())
                            .Max();

        var topScorePlayers = Players
                            .Where(player => player.GetScoreTotalPoints() == topScorePoints);
        if (topScorePlayers.Count() == 1)
        {
            return topScorePlayers.FirstOrDefault().PlayerId;
        }
        else
        {
            // トップスコアが複数ある場合、引き分けの意味で-1を返す
            return -1;
        }
    }
}
