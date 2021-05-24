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
    private Text WinnerText;
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
                            WinnerText.text = "引き分け";
                        }
                        else
                        {
                            WinnerText.text = "プレイヤー" + Game.GetWinnerPlayerId() + "の勝利！";
                        }
                    }

                    Instantiate(GameOverController);
                }
            });
    }
}
