using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _distanseWithoutPositions = 3f;
    [SerializeField] private float _timeToWaitMove = 0.2f;
    [SerializeField] private float _playerMoveVelocity = 1f;

    private IInputService _playerInputs;
    private Vector3[] _movePoints;
    private Transform _playerTransform;
    private bool _isCanMove;
    private int _currentPlayerPint = 1;

    [Inject]
    private void Construct(IInputService currentinput)
    {
        _playerInputs = currentinput;
    }

    private void Start()
    {
        _isCanMove = true;
        _playerTransform = GetComponent<Transform>();
        _playerInputs.OnPlayerMovedX += PlayerMoveX;
        SetMovePoint();
    }

    private void OnDisable() => _playerInputs.OnPlayerMovedX -= PlayerMoveX;

    private void SetMovePoint()
    {
        _movePoints = new Vector3[3];
        _movePoints[1] = _playerTransform.position;
        _movePoints[0] = new Vector3(-_distanseWithoutPositions, _movePoints[1].y, _movePoints[1].z);
        _movePoints[2] = new Vector3(_distanseWithoutPositions, _movePoints[1].y, _movePoints[1].z);
    }

    private void PlayerMoveX(float x)
    {
        if (!_isCanMove)
            return;

        if (x > 0.1 && _currentPlayerPint < 2)
        {
            _currentPlayerPint++;
            StopAllCoroutines();
            StartCoroutine(PlayerMove(_movePoints[_currentPlayerPint]));
            return;
        }
        if (x < -0.1 && _currentPlayerPint > 0)
        {
            _currentPlayerPint--;
            StopAllCoroutines();
            StartCoroutine(PlayerMove(_movePoints[_currentPlayerPint]));
        }
    }

    private IEnumerator WaitToMove()
    {
        _isCanMove = false;
        yield return new WaitForSeconds(_timeToWaitMove);
        _isCanMove = true;
    }
    
    private IEnumerator PlayerMove(Vector3 targetPoint)
    {
        StartCoroutine(WaitToMove());
        while (true)
        {
            if (Vector3.Distance(_playerTransform.position, targetPoint)  <= 0.01f)
            {
                _playerTransform.position = targetPoint;
                yield return null;
            }
            yield return new WaitUntil(() => transform.position != targetPoint);
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, targetPoint, Time.fixedDeltaTime * _playerMoveVelocity);
        }
    }
}
