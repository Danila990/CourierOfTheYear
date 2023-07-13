using UnityEngine;
using UnityEngine.Events;

public class MobileInputService : MonoBehaviour, IInputService
{
    public event UnityAction<float> OnPlayerMovedX;

    private bool _isDraging = false;
    private Vector2 _startTouch, _swipeDelta;

    public void Update()
    {
        MobileInput();
        CalculateDistance();
        SwipeMove();
    }

    private void MobileInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDraging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
    }

    private void CalculateDistance()
    {
        _swipeDelta = Vector2.zero;
        if (_isDraging)
        {
            if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }
    }

    private void SwipeMove()
    {
        if (_swipeDelta.magnitude > 125)
        {
            float x = _swipeDelta.x;
                if (x < 0)
                    OnPlayerMovedX?.Invoke(x);
                else
                    OnPlayerMovedX?.Invoke(x);

            Reset();
        }
    }

    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDraging = false;
    }
}
