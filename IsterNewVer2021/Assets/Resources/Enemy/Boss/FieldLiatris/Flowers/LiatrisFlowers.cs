using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisFlowers : MonoBehaviour
{
    [HideInInspector]
    protected FieldLiatris _liatris;
    [HideInInspector]
    protected FieldLiatrisController _liatrisController;
    [SerializeField]
    FieldFlowerAniController _aniCon;
    //���� ü��
    public Damagable _damagable;

    [SerializeField]
    public GameObject _witherFlower;

    //���� Ȱ��ȭ ���ΰ�.
    public bool _isActive;
    //��ȯ��
    [SerializeField]
    public bool _isSpawning;
    [HideInInspector]
    public bool _isSpawnOn;
    //��ȯ �Ǿ��°� 
    public bool _isSpawned;

    //�� ��ȥ �̵� �ӵ�
    public float _speed;
    //��ȯ ����Ʈ
    public ParticleSystem _spwanEffect;
    public ParticleSystem _trailEffect;

    [HideInInspector]
    public Coroutine _coroutine;

    [SerializeField]
    protected SFXPlayer _sfx;
    public Color _color;
    
    protected virtual void OnEnable()
    {
        _liatris = GetComponentInParent<FieldLiatris>();
        _liatrisController = GetComponentInParent<FieldLiatrisController>();
        _aniCon = GetComponent<FieldFlowerAniController>();
        _isActive = true;
        _isSpawning = false;
        _isSpawned = false;
        _isSpawnOn = false;
        _damagable.isCanHurt = false;

        _spwanEffect.Play();

        _coroutine = null;
    }
    

    // Update is called once per frame
    protected virtual void Update()
    {
        this.GetComponent<SpriteRenderer>().color = _color;

        if (!_liatris.damagable.isDie)
        {
            if (!_damagable.isDie)
            {
                FlowerUpdate(); 

                if (_isSpawning && !_isSpawned)
                {
                    Spawning();
                }
            }
            else
            {
                FlowerDie();
            }

            if(_damagable.isHurt)
            {
                if (_sfx) _sfx.PlaySFX("flower_hit");
            }
        }
    }
    //��ȯ ��.
    public void Spawning()
    {
        _trailEffect.gameObject.SetActive(true);
        _spwanEffect.Play();
        _isSpawnOn = true;
        _witherFlower.SetActive(true);
        _coroutine = null;
        _coroutine = StartCoroutine(FlowerSpawn());
    }

    //�� ����
    protected IEnumerator FlowerSpawn()
    {
        _isSpawning = false;
        if (!_isSpawned)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = _liatrisController._spawnPos.position;

            var dir = CommonFuncs.CalcDir(startPos, endPos);

            float currentTime = 0.0f;
            float totalTime = Vector3.Distance(startPos, endPos) / _speed;
            while (currentTime < totalTime)
            {
                float ratio = currentTime / totalTime;

                Vector3 newPos = Vector3.Lerp(startPos, endPos, ratio);
                transform.position = newPos;

                yield return null;

                currentTime += IsterTimeManager.enemyDeltaTime;
            }
            _trailEffect.gameObject.SetActive(false);

            _spwanEffect.Play();

            transform.position = endPos;

            _isSpawned = true;
            _liatrisController._SpecialPatternStart = true;
            _isSpawnOn = !_isSpawned;

            _sfx.PlaySFX("flower_spawned");

            dir = Vector2.zero;

            currentTime = 0.0f;
        }

        StopCoroutine(_coroutine);
    }
    //�� �ı�
    public IEnumerator FlowerDestroy()
    {
        _isSpawned = false;
        GetComponent<Collider2D>().isTrigger = true;

        if (_isActive)
        {
            Vector3 startPos; 
            Vector3 endPos;
            if (_liatrisController.bossMain._phase > 1)
            {
               startPos = transform.position;
               endPos = _liatrisController.transform.position;
            }
            else 
            {
                startPos = transform.localPosition;
                endPos = _liatrisController.tanaPos.transform.localPosition;
            }
            

            var dir = CommonFuncs.CalcDir(startPos, endPos);

            float currentTime = 0.0f;
            float totalTime = Vector3.Distance(startPos, endPos) / _speed;
            while (currentTime < totalTime)
            {
                float ratio = currentTime / totalTime;

                Vector3 newPos = Vector3.Lerp(startPos, endPos, ratio);

                if (_liatrisController.bossMain._phase > 1)
                {
                    transform.position = newPos;
                }
                else
                {
                    transform.localPosition = newPos;
                }

                yield return null;

                currentTime += IsterTimeManager.enemyDeltaTime;
            }

            if (_liatrisController.bossMain._phase > 1)
            {
                transform.position = endPos;
            }
            else
            {
                transform.localPosition = endPos;
            }


            _sfx.PlaySFX("flower_die");

            _isActive = false;
            _liatrisController.flowerCutCount = _liatrisController.flowerCutCount + 1;
            if (_liatrisController.CheckAllFlowersCut()) _liatrisController.LiatrisGrogi();            
            else _liatrisController.LiatrisHitted();

            _liatrisController.LiatrisHit();

            _trailEffect.gameObject.SetActive(false);

            dir = Vector2.zero;
            currentTime = 0.0f;

            this.gameObject.SetActive(false);

            StopCoroutine(_coroutine);
        }
    }
    //��ӹ����� ������
    protected virtual void FlowerUpdate()
    {
        if (_isSpawned) {
            _damagable.isCanHurt = true;
        }
        else _damagable.isCanHurt = false;

    }
    protected virtual void FlowerDie()
    {
    }
}
