using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackAssistant : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private GameObject _arrowPrefab;

    public GameObject effectPrefab { get { return _arrowPrefab; } set { _arrowPrefab = value; } }

    private PassiveKnockbackIncreaseUser _user;

    private Movable _owner;
    private NormalBulletCreator _creator;

    private List<KnockbackAssistantArrow> _arrows = new List<KnockbackAssistantArrow>();

    [SerializeField]
    private Color _color;

    // Start is called before the first frame update
    void OnEnable()
    {
        _user = GetComponentInParent<PassiveKnockbackIncreaseUser>();

        _owner = _user.manager.GetComponentInParent<Movable>();
        _creator = _owner.GetComponentInChildren<NormalBulletCreator>();

        if (typeof(MirageController).IsInstanceOfType(_owner))
            _color = new Color(0.0f, 1.0f, 0.7f);
    }

    void OnDisable()
    {
        foreach (var arrow in _arrows)
        {
            if (arrow)
                Destroy(arrow.gameObject);
        }

        _arrows.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        Damagable[] damagables = FindObjectsOfType<Damagable>();
        foreach (var damagable in damagables)
        {
            if (damagable.gameObject.layer != LayerMask.NameToLayer("Enemys")) continue;

            KnockbackAssistantArrow[] arrows = damagable.GetComponentsInChildren<KnockbackAssistantArrow>();
            if (arrows.Length == 0)
                CreateArrow(damagable);
            else if (arrows.Length == 1)
            {
                if (!arrows[0].IsSameOwner(_owner))
                    CreateArrow(damagable);
            }
        }
    }

    public void CreateArrow(Damagable damagable)
    {
        GameObject newArrow = CreateObject();
        newArrow.transform.parent = damagable.transform;
        Movable move = damagable.GetComponent<Movable>();
        if (move)
            newArrow.transform.position = move.center;
        else
            newArrow.transform.localPosition = Vector3.zero;

        KnockbackAssistantArrow arrow = newArrow.GetComponent<KnockbackAssistantArrow>();
        arrow.owner = _owner;
        arrow.creator = _creator;

        GlowableObject glow = arrow.GetComponent<GlowableObject>();
        glow.ApplyColor(_color);

        _arrows.Add(arrow);
    }

    public GameObject CreateObject()
    {
        return Instantiate(effectPrefab);
    }
}
