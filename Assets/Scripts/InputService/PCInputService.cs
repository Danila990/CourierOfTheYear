using UnityEngine;
using UnityEngine.Events;

public class PCInputService : MonoBehaviour, IInputService
{
    public event UnityAction<float> OnPlayerMovedX;

    public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
            OnPlayerMovedX?.Invoke(x);
    }
}
