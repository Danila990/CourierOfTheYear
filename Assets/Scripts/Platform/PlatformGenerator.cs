using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private int _countClonePlatform = 7;
    [SerializeField] private GameObject[] _platformPrefabs;
    [SerializeField] private int _startPlatformCount = 5;

    private List<GameObject> _pullPlatforms = new List<GameObject>();
    private SpeedChanger _speedChanger;
    private PullObject _pullObject;
    private float _platformSizeZ;
    private float _platformSpeed;

    public Action OnPlatformDeactivate;
    public Action<GameObject> OnPlatformActivate;

    [Inject]
    private void Construct(SpeedChanger platformSpeed)
    {
        _speedChanger = platformSpeed;
    }

    private void Awake()
    {
        _pullObject = gameObject.AddComponent<PullObject>();
        _pullPlatforms = _pullObject.GeneratePullObject(_countClonePlatform, _platformPrefabs);
        foreach (GameObject p in _pullPlatforms)
        {
            p.GetComponent<PlatformMover>().SetStartParameters(this, _speedChanger);
        }
    }

    private void Start()
    {
        _platformSpeed = _speedChanger.GetStartSpeedChange();
        _speedChanger.OnSpeedChanged += SpeedChange;
        _platformSizeZ = _pullPlatforms[0].GetComponent<BoxCollider>().size.z;

        ActivateStartPlatform();
    }

    private void OnDisable() => _speedChanger.OnSpeedChanged -= SpeedChange;

    private void ActivateStartPlatform()
    {
        for (int i = 0; i < _startPlatformCount; i++)
            ActivatePlatform(i, 0f);
    }

    private void ActivatePlatform(int i, float positionDifference)
    {
        GameObject platform = _pullPlatforms[UnityEngine.Random.Range(0, _pullPlatforms.Count - 1)];
        if (!platform.activeSelf)
        {
            platform.SetActive(true);
            platform.transform.position = new Vector3(0, 0, i * _platformSizeZ - positionDifference);
            platform.GetComponent<PlatformMover>().SetParameters(_platformSpeed, _platformSizeZ);
            OnPlatformActivate?.Invoke(platform);
        }
        else ActivatePlatform(i, positionDifference);
    }

    private void SpeedChange(float currentPlatformSpeed) => _platformSpeed = currentPlatformSpeed;

    public void DeactivatePlatform(GameObject platform, float positionDifference)
    {
        platform.SetActive(false);
        OnPlatformDeactivate?.Invoke();
        ActivatePlatform(_startPlatformCount - 1, positionDifference);
    }
}
