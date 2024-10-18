using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerEquipment : MonoBehaviour
{
    [SerializeField]
    private LookAtMouse _look;
    [SerializeField]
    private bool _isRotateStart = false;

    private Animator _anim;

    [SerializeField]
    private GameObject[] _swords;
    private int _currentSword;

    public Color currentAttackColor { get; set; }

    public bool isBattle { get; set; }

    private float _currentAngle;
    public float currentAngle { get { return _currentAngle; } }

    [SerializeField]
    private bool _isAttacking;
    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    [SerializeField]
    private NormalBulletCreator _creator;
    private bool _isSting;

    void Start()
    {
        _currentSword = 0;

        _anim = GetComponent<Animator>();
        ChangeSword(_currentSword);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_look) return;

        float angle = CommonFuncs.DirToDegree(_look.dir);
        if (!_isRotateStart)
            angle -= 90.0f;
        transform.localRotation = Quaternion.identity;
        _currentAngle = Mathf.LerpAngle(_currentAngle, angle, 0.3f);

        float angleX = (_isSting ? 0.0f : -45.0f);
        transform.Rotate(new Vector3(-angleX, 0.0f, _currentAngle));

        Animator swordAnim = _swords[_currentSword].GetComponent<Animator>();
        if (swordAnim)
            swordAnim.SetBool("isBattle", isBattle);
    }

    public void Attack()
    {
        if (!_anim) return;

        _anim.SetTrigger("attack");
    }

    [SerializeField]
    private SFXPlayer _sfx;
    public void Appear(bool isAttack = true)
    {
        //if (_sfx)
        //    _sfx.PlaySFX("sword_appear");

        if (isAttack)
        {
            Animator swordAnim = _swords[_currentSword].GetComponent<Animator>();
            if (swordAnim)
                swordAnim.SetTrigger("attack");
        }
    }

    [System.Serializable]
    public struct SwordInfo
    {
        public SWORDTYPE type;
        public GameObject prefab;
    }
    [SerializeField]
    private List<SwordInfo> _infoes;

    public void ChangeSword(int newSword)
    {
        _swords[_currentSword].SetActive(false);
        _currentSword = newSword;
        _swords[_currentSword].SetActive(true);
        GameObject newPrefab = null;
        foreach(var s in _infoes)
        {
            if ((SWORDTYPE)newSword == s.type)
            {
                newPrefab = s.prefab;
                break;
            }
        }

        _isSting = (((SWORDTYPE)newSword)) == SWORDTYPE.RAPIER;
        _creator.effectPrefab = newPrefab;
        _anim.SetBool("isSting", _isSting);

        GlowableObject glow = _swords[_currentSword].GetComponent<GlowableObject>();
        if (glow)
            currentAttackColor = glow.color;
        else
            currentAttackColor = new Color(0.1490196f, 0.003921569f, 0.05490196f, 1.0f);
    }
}
