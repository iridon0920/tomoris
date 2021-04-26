using UnityEngine;
using Zenject;

public class ControlBlocksInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject BlockControllerPrefab;
    [SerializeField]
    private GameObject ControlBlocksPrefab;

    public override void InstallBindings()
    {
        Container.Bind<BlockController>()
                .FromComponentInNewPrefab(BlockControllerPrefab)
                .AsSingle();
        Container.Bind<ControlBlocksPresenter>()
                .FromComponentInNewPrefab(ControlBlocksPrefab)
                .AsSingle();

        Container.Bind<BlockControllUseCase>()
                .AsSingle();
        Container.Bind<MoveControlBlocksService>()
                .AsSingle();
        Container.Bind<PutControlBlocksService>()
                .AsSingle();
        Container.Bind<GetNextControlBlocksService>()
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
