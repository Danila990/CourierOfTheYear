using UnityEngine;
using Zenject;

public class PlatformMonoInstaller : MonoInstaller
{
    [SerializeField] private PlatformGenerator _platformGeneratorPrefab;

    public override void InstallBindings()
    {
        PlatformInNewPrefab();
    }

    private void PlatformInNewPrefab()
    {
        Container.Bind<PlatformGenerator>().FromComponentInNewPrefab(_platformGeneratorPrefab).AsSingle().NonLazy();
    }
}