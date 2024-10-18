using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalDoorAnimBase : AnimController
{
    public abstract void CloseAnim();
    public abstract void OpenAnim();
    public abstract void ReCloseAnim();
}
