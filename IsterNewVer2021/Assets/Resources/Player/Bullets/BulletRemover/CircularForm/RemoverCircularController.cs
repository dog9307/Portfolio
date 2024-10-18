using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverCircularController : RemoverController
{
    public bool isSecondBullet { get; set; }

    private void OnDestroy()
    {
        if (isSecondBullet) return;

        if (user.isDoubleShoot)
        {
            GameObject second = user.CreateObject();

            float angle = CommonFuncs.DirToDegree(dir);
            second.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            second.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

            RemoverCircularController secondController = second.GetComponentInChildren<RemoverCircularController>();
            if (secondController)
                secondController.isSecondBullet = true;
        }
    }

    public override void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected override void ComputeVelocity()
    {
        _targetVelocity = Vector2.zero;
    }
}
