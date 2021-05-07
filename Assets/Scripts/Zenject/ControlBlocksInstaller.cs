using UnityEngine;
using Zenject;

public class ControlBlocksInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject BlockControllerPrefab;
    [SerializeField]
    private GameObject ControlBlocksPrefab;
    [SerializeField]
    private GameObject ScorePrefab;

    public override void InstallBindings()
    {
        Container.Bind<Player>()
                .AsSingle();

        Container.Bind<BlockController>()
                .FromComponentInNewPrefab(BlockControllerPrefab)
                .AsSingle();

        Container.Bind<ControlBlocksPresenter>()
                .AsSingle();

        Container.Bind<ControlBlocksView>()
                .FromComponentInNewPrefab(ControlBlocksPrefab)
                .AsSingle();

        Container.Bind<BlockControllUseCase>()
                .AsSingle();

        Container.Bind<NotMoveControlBlocksService>()
                .AsSingle();
        Container.Bind<MoveLeftControlBlocksService>()
                .AsSingle();
        Container.Bind<MoveRightControlBlocksService>()
                .AsSingle();
        Container.Bind<MoveDownControlBlocksService>()
                .AsSingle();
        Container.Bind<SpinLeftControlBlocksService>()
                .AsSingle();
        Container.Bind<SpinRightControlBlocksService>()
                .AsSingle();


        Container.Bind<PutControlBlocksService>()
                .AsSingle();
        Container.Bind<GetNextControlBlocksService>()
                .AsSingle();
        Container.Bind<EraseLineService>()
                .AsSingle();

        Container.Bind<Score>()
                .AsSingle();
        Container.Bind<ScorePresenter>()
                .AsSingle();
        Container.Bind<ScoreView>()
                .FromComponentInNewPrefab(ScorePrefab)
                .AsSingle();



        Container.Bind<BlocksFactory>()
                .AsSingle();
        Container.Bind<ControlBlocksAdjuster>()
                .AsSingle();
        Container.Bind<CollisionDetection>()
                .AsSingle();
        Container.Bind<IBlocksQueue>()
                .To<BlocksQueue>()
                .AsSingle()
                .WithArguments(4);

        Container.Bind<System.Random>().AsSingle();
    }
}
