using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private float _platformSpeed;
    private float _platformSizeZ;
    private Transform _platformTransform;
    private PlatformGenerator _platformGenerator;
    private SpeedChanger _speedChanger;

    private void Start()
    {
        _platformTransform = GetComponent<Transform>();
    }

    private void OnDestroy() => _speedChanger.OnSpeedChanged -= SpeedChange;

    private void Update()
    {
        _platformTransform.Translate(-_platformTransform.forward * _platformSpeed * Time.deltaTime);

        if (_platformTransform.position.z <= -_platformSizeZ)
            _platformGenerator.DeactivatePlatform(this.gameObject, -_platformSizeZ - _platformTransform.position.z);
    }

    private void SpeedChange(float currentSpeed) => _platformSpeed = currentSpeed;

    public void SetParameters(float platformSpeed,float platformSizeZ)
    {
        _platformSpeed = platformSpeed;
        _platformSizeZ = platformSizeZ;
    }

    public void SetStartParameters(PlatformGenerator platformGenerator, SpeedChanger speedChanger)
    {
        _platformGenerator = platformGenerator;
        _speedChanger = speedChanger;
        _speedChanger.OnSpeedChanged += SpeedChange;
    }
}
