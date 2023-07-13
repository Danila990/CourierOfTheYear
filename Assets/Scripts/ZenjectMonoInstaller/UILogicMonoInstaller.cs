using UnityEngine;
using Zenject;

public class UILogicMonoInstaller : MonoInstaller
{
    [SerializeField] private EndGame _endGameGameObject;
    [SerializeField] private ScoreOutput _scoreOutputGameObject;
    public override void InstallBindings()
    {
        UILogicFromInstance();
    }

    private void UILogicFromInstance()
    {
        Container.Bind<EndGame>().FromInstance(_endGameGameObject).AsSingle().NonLazy();
        Container.Bind<ScoreOutput>().FromInstance(_scoreOutputGameObject).AsSingle().NonLazy();
    }
}
