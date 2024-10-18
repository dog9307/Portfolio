using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreationFoothold : FootholdBase 
{
    [SerializeField]
    Transform _createPos;
   
    [SerializeField]
    GameObject _floorPrefab;

    [SerializeField]
    private Transform _returnPos;

    Damagable _damagable;

    public float _creationDelay;
    private float _currentTime;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private FadingGuideUI _arrow;

    public UnityEvent OnHit;

    [SerializeField]
    private GameObject _hitEffectPrefab;

    [SerializeField]
    private bool _isFloorNotRemove = false;

    [SerializeField]
    private DisposableObject _disposable;

    // Start is called before the first frame update
    protected override void Start()
    {        
        base.Start();

        _damagable = GetComponent<Damagable>();
        _currentTime = 0;

        _collider.isTrigger = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            _sfx.PlaySFX("hit");
            CameraShakeController.instance.CameraShake(10.0f);

            if (_isCanActive)
            {
                CreateObject();

                if (OnHit != null)
                    OnHit.Invoke();

                if (_disposable)
                    _disposable.UseObject();

                _sfx.PlaySFX("create");
            }
        }
    }

    public void CreateFloor()
    {
        CreateObject();
    }

    public GameObject CreateObject()
    {
        _isCanActive = false;

        GameObject newfloor = Instantiate<GameObject>(_floorPrefab);
        newfloor.transform.position = _createPos.position;

        PlayerReturnPosHelper posHelper = newfloor.GetComponent<PlayerReturnPosHelper>();
        if (posHelper)
            posHelper.returnPos = _returnPos;

        StartCoroutine(creation(newfloor));

        //GameObject effect =  Instantiate(_hitEffectPrefab);
        //effect.transform.position = transform.position;

        if (_isFloorNotRemove)
        {
            MaybeTrash trash = newfloor.GetComponent<MaybeTrash>();
            if (trash)
            {
                trash.isNotTrash = true;
                Destroy(trash);
            }
        }

        return newfloor;
    }

    IEnumerator creation(GameObject gameObject)
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;

        color.a = Mathf.Lerp(0.0f, 1.0f, 0.0f);

        while (_currentTime < _creationDelay)
        {
            _currentTime += IsterTimeManager.deltaTime;

            float ratio = _currentTime / _creationDelay;

            color.a = Mathf.Lerp(0.0f, 1.0f, ratio);

            gameObject.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }
    }

    private void OnBecameVisible()
    {
        if (_arrow && _isCanActive)
            _arrow.StartFading(1.0f);
    }

    private void OnBecameInvisible()
    {
        if (_arrow && _isCanActive)
            _arrow.StartFading(0.0f);
    }
}
