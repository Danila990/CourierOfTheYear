using UnityEngine;
using Zenject;

public class PlayerMonoInstaller : MonoInstaller
{
    [SerializeField] private PlayerMover _playerPrefab;

    public override void InstallBindings()
    {
        PlayerInNewPrefab();
    }
    
    private void PlayerInNewPrefab()
    {
        Container.Bind<PlayerMover>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
    }
}