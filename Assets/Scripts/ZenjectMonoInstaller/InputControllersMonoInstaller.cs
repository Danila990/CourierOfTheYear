using UnityEngine;
using Zenject;

public class InputControllersMonoInstaller : MonoInstaller
{
    [SerializeField] private bool _isMobile;
    [SerializeField] private PCInputService _PCInputServicePrefab;
    [SerializeField] private MobileInputService _mobileInputServicePrefab;

    public override void InstallBindings()
    {
        InputControllersInNewPrefab();
    }

    private void InputControllersInNewPrefab()
    {
        if (_isMobile)
            Container.Bind<IInputService>().To<MobileInputService>().FromComponentInNewPrefab(_mobileInputServicePrefab).AsSingle();
        else Container.Bind<IInputService>().To<PCInputService>().FromComponentInNewPrefab(_PCInputServicePrefab).AsSingle();
    }
}