using UnityEngine;
using Zenject;

public class PresenterMonoInstaller : MonoInstaller
{

    [SerializeField]
    private GameObject BoardPrefab;
    public override void InstallBindings()
    {
        Container.Bind<BoardPresenter>().FromComponentInNewPrefab(BoardPrefab).AsSingle();
    }
}
