using Zenject;
using UnityEngine;
using System.Collections.Generic;
public class ControlBlocksInstaller : MonoInstaller<ControlBlocksInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IBlockControllUseCase>().To<BlockControllUseCase>().AsCached();

        Container.Bind<IControlBlocksAdjuster>().To<ControlBlocksAdjuster>().AsCached();

        Container.Bind<IControlBlocksCollisionDetection>().To<ControlBlocksCollisionDetection>().AsCached();

        Container.Bind<IBlocksQueue>().To<BlocksQueue>().AsCached().WithArguments(4);

        Container.Bind<IBoard>().To<Board>().AsSingle().WithArguments(10, 20);
    }
}
