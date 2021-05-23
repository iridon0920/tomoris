using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using DG.Tweening;
public class Game : MonoBehaviour
{
    [Inject]
    Player.Factory PlayerFactory;
    List<Player> Players = new List<Player>();

    void Awake()
    {
        DOTween.
            SetTweensCapacity(tweenersCapacity: 500, sequencesCapacity: 50);

        var PlayerCount = TitleToGameDataSender.PlayerCount;
        for (var i = 1; i <= PlayerCount; i++)
        {
            Players.Add(PlayerFactory.Create(i));
        }
    }
}
