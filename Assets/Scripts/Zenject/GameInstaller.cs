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
                .FromComponentInNewPrefab(BoardPrefab)
                .AsSingle();

        Container.Bind<IBoard>()
                .To<Board>()
                .AsSingle()
                .WithArguments(10, 20);

        Container.Bind<BoardBlocksLineEraseUseCase>()
                .AsSingle();
        Container.Bind<FallBoardBlocksUseCase>()
                .AsSingle();
        Container.Bind<Score>()
                .AsSingle();

        Container.BindFactory<BlockController, BlockController.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(GameContextPrefab).AsCached();

        Container.Bind<GameOverEvent>()
                .AsSingle();
        Container.Bind<UpdateScoreEvent>()
                .AsSingle();
    }
}