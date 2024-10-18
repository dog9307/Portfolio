using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerComponent : MonoBehaviour
{
    protected abstract void OnEnable();

    public void ChangeCharacter(System.Type newComponent)
    {
        if (newComponent != null)
            gameObject.AddComponent(newComponent);
        Destroy(this);
    }
}
