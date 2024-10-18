using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEvent : MonoBehaviour, IObjectCreator
{
    Damagable _damagable;

    [SerializeField]
    private GameObject _chestItem;
    public GameObject effectPrefab { get { return _chestItem; } set { _chestItem = value; } }

    Animator _animator;
    [HideInInspector]
    public bool _isCanActive;
    public bool _isReset;
    public float _resetCooltime;

    [HideInInspector]
    [SerializeField]
    private Transform _itemDropPos;

    [SerializeField]
    private ParticleSystem _effect;

    // Start is called before the first frame update
     void Start()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
        _animator = GetComponent<Animator>();
        _damagable = GetComponent<Damagable>();
        _isCanActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isCanActive)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            {
                _isCanActive = false;
                _animator.SetTrigger("ChestOpen");
                CameraShakeController.instance.CameraShake(10.0f);

                if (_effect)
                    _effect.Stop();
            }
        }
    }

    public void ResetChest()
    {
        if (_isReset)
        {
            StartCoroutine(ChestCooltime());
        }
        else return;
    }
    IEnumerator ChestCooltime()
    {
        yield return new WaitForSeconds(_resetCooltime);
        
        _animator.SetTrigger("ChestClose");

    }

    public GameObject CreateObject()
    {
        GameObject newItem = Instantiate(effectPrefab);
        newItem.transform.position = _itemDropPos.position;

        return newItem;
    }
}
