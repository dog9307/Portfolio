using UnityEngine;

interface IUpdatable
{
    void Update();
}

public abstract class UpdatableComponent : MonoBehaviour
{
    public abstract void UpdateManually(float deltaTime);
    public abstract void ResetUpdateManually();
}
