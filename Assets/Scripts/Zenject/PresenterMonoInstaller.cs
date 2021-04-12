using UnityEngine;
using Zenject;

public class PresenterMonoInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject prefab;
    public override void InstallBindings()
    {
        Container.Bind<BoardPresenter>().To<BoardPresenter>().FromComponentInNewPrefab(prefab).AsSingle();

    }
}
