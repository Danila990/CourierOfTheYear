using UnityEngine.Events;

public interface IInputService
{
    public event UnityAction<float> OnPlayerMovedX;

    void Update();
}
