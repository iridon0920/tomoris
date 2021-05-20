using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField]
    private GameObject BoardPrefab;
    [SerializeField]
    private GameObject GameContextPrefab;

    public override void InstallBindings()
    {
        Container.Bind<BoardPresenter>()
                .AsSingle();

        Container.Bind<BoardView>()
                .FromComponentInNewPrefab(BoardPrefab)
                .AsSingle();

        Container.Bind<IBoard>()
                .To<Board>()
                .AsSingle()
                .WithArguments(10, 20);

        Container.BindFactory<int, Player, Player.Factory>().AsTransient();

        Container.Bind<BlockController>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(GameContextPrefab).AsTransient();

        Container.Bind<GameOverEvent>()
                .AsSingle();


        Container.Bind<System.Random>()
                .AsSingle();
    }
}
