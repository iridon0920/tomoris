using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using UniRx;
public class Game : MonoBehaviour
{
    [Inject]
    BlockController.Factory BlockControllerFactory;
    List<BlockController> BlockControllers = new List<BlockController>();

    [SerializeField]
    private Canvas GameOverText;
    [SerializeField]
    private GameObject GameOverController;
    [Inject]
    private readonly GameOverEvent GameOverEvent;
    void Awake()
    {
        BlockControllers.Add(BlockControllerFactory.Create());

        GameOverEvent
            .GameOverObservable
            .Subscribe(isGameOver =>
                {
                    if (isGameOver)
                    {
                        GameOverText.enabled = true;
                        Instantiate(GameOverController);
                    }
                }
            );
    }
}
