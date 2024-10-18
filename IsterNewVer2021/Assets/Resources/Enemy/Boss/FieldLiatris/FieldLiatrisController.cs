using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldLiatrisController : BossControllerBase
{
    //기본 컴포넌트들
    [SerializeField]
    private BossHPBarController _hpBar;

    [SerializeField]
    protected FieldLiatris _bossMain;
    public FieldLiatris bossMain { get { return _bossMain; } }
    
    //타깃
    [SerializeField]
    PlayerMoveController _player;

    [SerializeField]
    FieldLiatrisPartnerManager _partner;
    public FieldLiatrisPartnerManager partner { get { return _partner; } }
    public PlayerMoveController player { get { return _player; } }
    //꽃들을 데리고 있어야함.
    [SerializeField]
    List<LiatrisFlowers> _flowers = new List<LiatrisFlowers>();
    [SerializeField]
    List<Transform> _flowersPos = new List<Transform>();
    public Transform _spawnPos;
    //꽃이 소환중인가
    public bool _isFlowerSpawned;

    //실드 오브젝트
    [SerializeField]
    GameObject _shieldObject;
    //실드 리스트
    List<GameObject> _shield = new List<GameObject>();
    //타나 실드 리스트
    List<GameObject> _tanaShield = new List<GameObject>();
    //현재 실드 카운트
    [SerializeField]
    int _shieldCount;
    //총 실드 갯수
    //[HideInInspector]
    //public int _totalShieldCount;
    public int shieldCount { get { return _shieldCount;} }

    //모든 꽃이 파괴되었는가
    [HideInInspector]
    public bool _allFlowerCut;

    //wakeup
    public bool _isActive;
    //첫 패턴 타이머
    public float _firstSpawnTime;
    float _currentTimer;
    //첫 패턴 시작
    [HideInInspector]
    public bool _firstSpawnStart;
    //그로기.
    public bool _grogi;
    [HideInInspector]
    public bool _grogiTimeEnd;

    public float _grogiResetTimer;
    float _currentGrogiTimer;
    //코루틴 관리
    protected Coroutine _currentCoroutine;

    //피격(꽃 파괴)
    [SerializeField]
    private ParticleSystem _flowerBreakEffect;
    //실드 파괴
    [SerializeField]
    private ParticleSystem _shieldBreakEffect;
    [SerializeField]
    private ParticleSystem _tanaShieldBreakEffect;
    //사망 먼지
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

    //꽃들 리셋.(모든 꽃들 파괴 및, 패턴 초기화)
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
    //꽃 소환 
    public void FlowerSpawn()
    {
        if (_tana) _tana.GetComponent<Animator>().SetTrigger("Attack");
        //들어와짐
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
    //꽃 상태 체크 (소환중이 아닐때 allflowerCut = true;)
    public bool CheckAllFlowersCut()
    {
        if (_flowerCutCount < 3) return false;
        else return true;
       
    }
    //초기 실드 생성
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
    //실드 파괴.
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
    //리아트리스 그로기 (꽃 파괴 && 실드 깨진 후.)
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
    //그로기 리셋
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
        //체력은                         현재 체력(전체)                    (전체체력) * 1 /실드카운트 = 전체체력 / 실드카운트 * 1 / 꽃 갯수 = 전체체력 * 1 / 실드카운트 * 꽃 갯수.
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
    //피격 회복
    public void HittedReset()
    {
        _isFlowerSpawned = false;
        _allFlowerCut = false;
    }


    // 밑에는 시퀀스 이벤트 관련 밑 전투 시작과 종료 관리 ---------------------------------
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
