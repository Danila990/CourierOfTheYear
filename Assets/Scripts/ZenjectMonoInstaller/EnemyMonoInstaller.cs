using UnityEngine;
using Zenject;

public class EnemyMonoInstaller : MonoInstaller
{
    [SerializeField] private EnemyGenerator _enemyGeneratorPrefab;

    public override void InstallBindings()
    {
        EnemyInNewPrefab();
    }

    private void EnemyInNewPrefab()
    {
        Container.Bind<EnemyGenerator>().FromComponentInNewPrefab(_enemyGeneratorPrefab).AsSingle().NonLazy();
    }
}