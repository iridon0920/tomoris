using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
public class Game : MonoBehaviour
{
    [Inject]
    Player.Factory PlayerFactory;
    List<Player> Players = new List<Player>();

    void Awake()
    {
        Players.Add(PlayerFactory.Create(1));
        Players.Add(PlayerFactory.Create(2));
    }
}
