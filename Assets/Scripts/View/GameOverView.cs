using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;


public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private Canvas GameOverText;
    [SerializeField]
    private GameObject GameOverController;
    [Inject]
    private readonly GameOverEvent GameOverEvent;

    void Awake()
    {
        GameOverEvent
            .GameOverObservable
            .Subscribe(isGameOver =>
            {
                if (isGameOver)
                {
                    GameOverText.enabled = true;
                    Instantiate(GameOverController);
                }
            });
    }
}
