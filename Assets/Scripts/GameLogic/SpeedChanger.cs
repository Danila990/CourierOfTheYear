using System.Collections;
using UnityEngine;
using System;

public class SpeedChanger : MonoBehaviour
{
    [SerializeField] private float _startSpeedChange = 3f;
    [SerializeField] private float _speedModifier = 100f;

    private int _speedPoint = 0;
    private float _speedChange;

    public Action<float> OnSpeedChanged;

    private void Awake()
    {
        StartCoroutine(SpeedPointCount());
    }

    private IEnumerator SpeedPointCount()
    {
        while (Time.timeScale == 1)
        {
            _speedPoint++;
            SpeedChange();
            yield return new WaitForSeconds(1);
        }
    }

    private void SpeedChange()
    {
        _speedChange = _startSpeedChange + (_speedModifier * _speedPoint / 100);
        OnSpeedChanged?.Invoke(_speedChange);
    }

    public float GetStartSpeedChange() => _startSpeedChange;
}
