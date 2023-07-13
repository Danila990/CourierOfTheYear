using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private SpeedChanger _speedChanger;
    private float _currentSpeed;
    private Transform _entityTransform;

    private void Start()
    {
        _entityTransform = GetComponent<Transform>();
    }

    private void OnDestroy() => _speedChanger.OnSpeedChanged -= SpeedChange;

    private void SpeedChange(float currentSpeed) => _currentSpeed = currentSpeed;

    private void Update()
    {
        _entityTransform.Translate(-transform.forward * _currentSpeed * Time.deltaTime);

        if (_entityTransform.position.z < -5)
            this.gameObject.SetActive(false);
    }

    public void SetStartParameters(SpeedChanger speedChanger)
    {
        _speedChanger = speedChanger;
        _speedChanger.OnSpeedChanged += SpeedChange;
    }
}
