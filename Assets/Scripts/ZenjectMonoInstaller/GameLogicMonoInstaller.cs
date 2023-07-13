using UnityEngine;
using Zenject;

public class GameLogicMonoInstaller : MonoInstaller
{
    [SerializeField] private SpeedChanger _speedChangerPrefab;
    [SerializeField] private ScoreCounter _scoreCounterPrefab;

    public override void InstallBindings()
    {
        GameLogicMInNewPrefab();
    }

    private void GameLogicMInNewPrefab()
    {
        Container.Bind<SpeedChanger>().FromComponentInNewPrefab(_speedChangerPrefab).AsSingle().NonLazy();
        Container.Bind<ScoreCounter>().FromComponentInNewPrefab(_scoreCounterPrefab).AsSingle().NonLazy();
    }
}