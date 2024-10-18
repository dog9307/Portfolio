using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShadow : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _parent;
    private SpriteRenderer _sprite;

    private float _scaleFactorX;
    private float _scaleFactorY;

    [SerializeField]
    private bool _isAffectedLightDir = true;

    private bool isDynamicOrder { get { if (!_parent) return false; return (_parent.sortingLayerName == "Dynamic"); } }

    [SerializeField]
    private bool isParentRot = true;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        _scaleFactorX = transform.parent.localScale.x;
        _scaleFactorY = transform.parent.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        _sprite.sprite = _parent.sprite;

        Vector3 scale = new Vector3(_scaleFactorX, _scaleFactorY, 1.0f);
        float angle = 0.0f;

        TestShadowController controller = FindObjectOfType<TestShadowController>();
        if (controller && _isAffectedLightDir)
        {
            Vector3 dir = CommonFuncs.CalcDir(controller, transform.parent);
            angle = CommonFuncs.DirToDegree(dir);
            angle += 90.0f;

            if (angle > 180.0f)
                angle -= 360.0f;

            float ratio = Mathf.Abs(angle) / 90.0f;
            float scaleFactor = Mathf.Lerp(_scaleFactorX, 0.0f, ratio);
            scale = transform.parent.localScale;
            scale.x = scaleFactor;

            float distance = Vector2.Distance(controller.transform.position, transform.parent.position);
            if (distance > controller.maxRange)
                scale.y = _scaleFactorY;
            else
            {
                ratio = distance / controller.maxRange;
                scale.y = Mathf.Lerp(_scaleFactorY, 5.0f, (1.0f - ratio));
            }
        }

        if (isParentRot)
        {
            transform.parent.rotation = Quaternion.identity;
            transform.parent.Rotate(new Vector3(0.0f, 0.0f, angle));
        }

        transform.parent.localScale = scale;

        if (isDynamicOrder)
            _sprite.sortingOrder = _parent.sortingOrder;
    }
}
