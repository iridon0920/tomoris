using Zenject;
using UnityEngine;
using System.Collections.Generic;
public class ControlBlocksInstaller : MonoInstaller<ControlBlocksInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<BlockControllUseCase>().To<BlockControllUseCase>().AsCached();
        Container.Bind<MoveControlBlocksService>().To<MoveControlBlocksService>().AsCached();
        Container.Bind<PutControlBlocksService>().To<PutControlBlocksService>().AsCached();

        Container.Bind<ControlBlocksAdjuster>().To<ControlBlocksAdjuster>().AsCached();

        Container.Bind<CollisionDetection>().To<CollisionDetection>().AsCached();

        Container.Bind<IBlocksQueue>().To<BlocksQueue>().AsCached().WithArguments(4);

        Container.Bind<IBoard>().To<Board>().AsSingle().WithArguments(10, 20);

        Container.Bind<BoardBlocksLineEraseUseCase>().To<BoardBlocksLineEraseUseCase>().AsCached();
        Container.Bind<FallBoardBlocksUseCase>().To<FallBoardBlocksUseCase>().AsCached();
    }
}
