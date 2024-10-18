using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldLiatrisController : BossControllerBase
{
    //�⺻ ������Ʈ��
    [SerializeField]
    private BossHPBarController _hpBar;

    [SerializeField]
    protected FieldLiatris _bossMain;
    public FieldLiatris bossMain { get { return _bossMain; } }
    
    //Ÿ��
    [SerializeField]
    PlayerMoveController _player;

    [SerializeField]
    FieldLiatrisPartnerManager _partner;
    public FieldLiatrisPartnerManager partner { get { return _partner; } }
    public PlayerMoveController player { get { return _player; } }
    //�ɵ��� ������ �־����.
    [SerializeField]
    List<LiatrisFlowers> _flowers = new List<LiatrisFlowers>();
    [SerializeField]
    List<Transform> _flowersPos = new List<Transform>();
    public Transform _spawnPos;
    //���� ��ȯ���ΰ�
    public bool _isFlowerSpawned;

    //�ǵ� ������Ʈ
    [SerializeField]
    GameObject _shieldObject;
    //�ǵ� ����Ʈ
    List<GameObject> _shield = new List<GameObject>();
    //Ÿ�� �ǵ� ����Ʈ
    List<GameObject> _tanaShield = new List<GameObject>();
    //���� �ǵ� ī��Ʈ
    [SerializeField]
    int _shieldCount;
    //�� �ǵ� ����
    //[HideInInspector]
    //public int _totalShieldCount;
    public int shieldCount { get { return _shieldCount;} }

    //��� ���� �ı��Ǿ��°�
    [HideInInspector]
    public bool _allFlowerCut;

    //wakeup
    public bool _isActive;
    //ù ���� Ÿ�̸�
    public float _firstSpawnTime;
    float _currentTimer;
    //ù ���� ����
    [HideInInspector]
    public bool _firstSpawnStart;
    //�׷α�.
    public bool _grogi;
    [HideInInspector]
    public bool _grogiTimeEnd;

    public float _grogiResetTimer;
    float _currentGrogiTimer;
    //�ڷ�ƾ ����
    protected Coroutine _currentCoroutine;

    //�ǰ�(�� �ı�)
    [SerializeField]
    private ParticleSystem _flowerBreakEffect;
    //�ǵ� �ı�
    [SerializeField]
    private ParticleSystem _shieldBreakEffect;
    [SerializeField]
    private ParticleSystem _tanaShieldBreakEffect;
    //��� ����
    [SerializeField]
    private ParticleSystem _dieEffect;

    [HideInInspector]
    public bool _SpecialPatternStart;
    [HideInInspector]
    public bool _SpecialPatternEnd;

    int _flowerCutCount;
    public int flowerCutCount { get { return _flowerCutCount; } set { _flowerCutCount = value; } }

    [SerializeField]
    GameObject _tana;
    [SerializeField]
    Transform _tanaPos;
    public Transform tanaPos {get{ return _tanaPos; }}

    [SerializeField]
    private SFXPlayer _sfx;

    private void OnEnable()
    {
        if(_tana) _tana.transform.position = _tanaPos.position;
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>(); 
    }
    private void Start()
    {
        _bossMain.damagable.isCanHurt = false;
        _flowerCutCount = 0;
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        _currentGrogiTimer = 0;
        _isFlowerSpawned = false;
        _allFlowerCut = false;
        _isActive = false;
        _firstSpawnStart = false;
        _currentTimer = 0;
        _SpecialPatternStart = false;
        //_totalShieldCount = shieldCount;
        _SpecialPatternEnd = false;
        if (_shieldCount == 0) _shieldCount = 2;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F11)) {
        //    _isActive = true; 
        //    BattleStart();
        //}

        if (_isActive)
        {          
            if (!_bossMain.damagable.isDie)
            {
                if (_firstSpawnStart)
                {
                    //_bossMain.damagable.isCanHurt = false;

                    if (!_grogi && !_isFlowerSpawned)
                    {
                        FlowerSpawn();
                    }

                    if (_grogi && !_grogiTimeEnd)
                    {
                        if (_grogiResetTimer > _currentGrogiTimer)
                        {
                            _currentGrogiTimer += IsterTimeManager.bossDeltaTime;
                            if (_grogiResetTimer < _currentGrogiTimer)
                            {
                                _currentGrogiTimer = 0;
                                _grogi = false;
                                _grogiTimeEnd = true;
                                _tana.GetComponent<Animator>().SetBool("grogiEnd", true);
                            }                            
                        }
                    }
                }
                else
                {
                    _SpecialPatternEnd = true;
                       _currentTimer += IsterTimeManager.bossDeltaTime;
                    if(_currentTimer > _firstSpawnTime)
                    {
                        _currentTimer = 0;
                        _firstSpawnStart = true;
                    }
                }

                _SpecialPatternEnd = _grogi;
              
            }
            else GetComponent<FieldLiatrisAniController>().Die();
        }


        _tana.GetComponent<Animator>().SetBool("isDie", _bossMain.damagable.isDie);
    }


    public void BattleStart()
    {
        FlowerSetting();
        ShieldSetting(); 
        TanaShieldSetting();
        if (_hpBar)
            _hpBar.StartBattle();
    }

    //�ɵ� ����.(��� �ɵ� �ı� ��, ���� �ʱ�ȭ)
    void FlowerSetting()
    {
        _flowerCutCount = 0;

        if (_flowers.Count != 0 && _flowersPos.Count != 0)
        {
            for (int i = 0; i < _flowers.Count; i++)
            {
                _flowers[i].gameObject.SetActive(true);
                _flowers[i]._witherFlower.SetActive(false);
                _flowers[i].transform.position = _flowersPos[i].position;
                _flowers[i]._damagable.currentHP = _flowers[i]._damagable.totalHP;
                _flowers[i].GetComponent<Collider2D>().isTrigger = false;
                _flowers[i]._isActive = true;
                _flowers[i]._isSpawning = false;
                _flowers[i]._isSpawned = false;
            }
        }
    }
    //�� ��ȯ 
    public void FlowerSpawn()
    {
        if (_tana) _tana.GetComponent<Animator>().SetTrigger("Attack");
        //������
        int _flowerNum;
        if (_flowers.Count != 0)
        {
            _flowerNum = Random.Range(0,_flowers.Count);

            if (!_flowers[_flowerNum]._isActive) FlowerSpawn();
            else
            {
                _flowers[_flowerNum]._isSpawning = true;
                _isFlowerSpawned = true;
            }
            if(_sfx) _sfx.PlaySFX("flower_spawning");
        }
    } 
    //�� ���� üũ (��ȯ���� �ƴҶ� allflowerCut = true;)
    public bool CheckAllFlowersCut()
    {
        if (_flowerCutCount < 3) return false;
        else return true;
       
    }
    //�ʱ� �ǵ� ����
    void ShieldSetting()
    {
        for (int i = 0; i< _shieldCount; i++)
        {
            GameObject shield = Instantiate(_shieldObject) as GameObject;
            shield.transform.SetParent(_bossMain.transform, false);
            _shield.Add(shield);
        }

       
    }
    void TanaShieldSetting()
    {
        for (int i = 0; i < _shieldCount; i++)
        {
            GameObject shield = Instantiate(_shieldObject) as GameObject;
            shield.transform.localPosition = _tanaPos.localPosition;
            shield.transform.localScale = new Vector3(0.9f, 1.125f, 0.5f);
            shield.transform.SetParent(_bossMain.transform, false);
            _tanaShield.Add(shield);
        }
    }
    //�ǵ� �ı�.
    void ShieldBreaking()
    {
        if (_shieldCount > 0)
        {
            _shieldCount--;
            GameObject shield = _shield[_shield.Count - 1].gameObject;
            Destroy(shield);
            _shield.RemoveAt(_shield.Count - 1);

            GameObject tanaShield = _tanaShield[_tanaShield.Count - 1].gameObject;
            Destroy(tanaShield);
            _tanaShield.RemoveAt(_tanaShield.Count - 1);
        }
    }
    //����Ʈ���� �׷α� (�� �ı� && �ǵ� ���� ��.)
    public void LiatrisGrogi()
    {
        _shieldBreakEffect.Play();
        _tanaShieldBreakEffect.Play();
        GetComponent<FieldLiatrisAniController>().Grogi();

        if (_tana)
        {
            _tana.GetComponent<Animator>().SetTrigger("Grogi");
        }

        ShieldBreaking();
       // _bossMain.damagable.currentHP = _bossMain.damagable.currentHP -( (_bossMain.damagable.totalHP * (1.000f / _bossMain._maxPhase) + 0.1f));
       
       
    }
    //�׷α� ����
    public void GrogiReset()
    {
        if (!_bossMain.damagable.isDie)
        {
            _firstSpawnStart = false;
            _currentTimer = 0;
            FlowerSetting();
            _isFlowerSpawned = false;
            _allFlowerCut = false;
        }
        if (_sfx)
        {
            _sfx.PlaySFX("phaseChange");
        }

    }
    public void LiatrisHitted()
    {       
        _flowerBreakEffect.Play();
        GetComponent<FieldLiatrisAniController>().Hitted();

        if (_tana) _tana.GetComponent<Animator>().SetTrigger("Hit");
        //ü����                         ���� ü��(��ü)                    (��üü��) * 1 /�ǵ�ī��Ʈ = ��üü�� / �ǵ�ī��Ʈ * 1 / �� ���� = ��üü�� * 1 / �ǵ�ī��Ʈ * �� ����.
        //_bossMain.damagable.currentHP = _bossMain.damagable.currentHP - ((_bossMain.damagable.totalHP / ((_bossMain._maxPhase) * _flowers.Count)));     
    }
    public void LiatrisHit()
    {
        _bossMain.damagable.currentHP = _bossMain.damagable.currentHP - (((_bossMain.damagable.totalHP * (1.000f / _bossMain._maxPhase)) / _flowers.Count) + 0.001f);
        if (_bossMain._phase > 1)
        {
            _flowerBreakEffect.transform.localPosition = this.transform.localPosition;
            if (_sfx) _sfx.PlaySFX("fieldLiatris_hit");
        }
        else
        {
            _flowerBreakEffect.transform.localPosition = tanaPos.transform.localPosition;
            if (_sfx) _sfx.PlaySFX("tana_hit");
        }

    }
    //�ǰ� ȸ��
    public void HittedReset()
    {
        _isFlowerSpawned = false;
        _allFlowerCut = false;
    }


    // �ؿ��� ������ �̺�Ʈ ���� �� ���� ���۰� ���� ���� ---------------------------------
    public override void BossWakeUp()
    {
       _isActive = true;
    }

    public UnityEvent OnDie;
    public override void BossDie()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
        {
            renderer.sortingLayerName = "Foreground";
            renderer.sortingOrder = BlackMaskController.BLACKMASK_ORDER_IN_LAYER + 1;
        }

        if (OnDie != null)
            OnDie.Invoke();

        if (_partner.gameObject.activeSelf) _partner.gameObject.SetActive(false);
    }

    public override void BossDisappear()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            renderer.sortingLayerName = "Dynamic";
    }
}
