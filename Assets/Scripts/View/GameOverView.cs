using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;


public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private Game Game;
    [SerializeField]
    private Canvas GameOverText;
    [SerializeField]
    private Image Winner1P;
    [SerializeField]
    private Image Winner2P;
    [SerializeField]
    private Image Win;
    [SerializeField]
    private Image Draw;
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

                    if (Game.PlayerCount > 1)
                    {
                        if (Game.GetWinnerPlayerId() == -1)
                        {
                            Draw.enabled = true;
                        }
                        else
                        {
                            Win.enabled = true;
                            if (Game.GetWinnerPlayerId() == 1)
                            {
                                Winner1P.enabled = true;
                            }
                            else if (Game.GetWinnerPlayerId() == 2)
                            {
                                Winner2P.enabled = true;
                            }
                        }
                    }

                    Instantiate(GameOverController);
                }
            });
    }
}
