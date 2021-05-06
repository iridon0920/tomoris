using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
public class Game : MonoBehaviour
{
    [Inject]
    BlockController.Factory BlockControllerFactory;
    List<BlockController> BlockControllers = new List<BlockController>();

    void Awake()
    {
        BlockControllers.Add(BlockControllerFactory.Create());
    }
}
