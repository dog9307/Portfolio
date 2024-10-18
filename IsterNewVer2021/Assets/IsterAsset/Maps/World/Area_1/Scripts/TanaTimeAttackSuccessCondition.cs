using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanaTimeAttackSuccessCondition : FieldTimeAttackSuccessCondition
{
    [SerializeField]
    private Damagable[] _flowers;

    [SerializeField]
    private GameObject _lineEffectPrefab;
    [SerializeField]
    private GameObject _slashPrefab;

    [SerializeField]
    private GameObject _killPlayerBullet;

    [HideInInspector]
    [SerializeField]
    private Transform _tana;

    [SerializeField]
    private SFXPlayer _sfx;

    public override bool IsSuccess()
    {
        bool isAllDie = true;
        for (int i = 0; i < _flowers.Length; ++i)
        {
            if (!_flowers[i]) continue;

            if (!_flowers[i].isDie)
            {
                isAllDie = false;
                break;
            }
        }

        return isAllDie;
    }

    private PlayerMoveController _player;
    private GameObject _newLine;
    public void CreateEffect()
    {
        _player = FindObjectOfType<PlayerMoveController>();

        float distance = Vector2.Distance(_tana.position, _player.transform.position);

        _newLine = Instantiate(_lineEffectPrefab);
        _newLine.transform.position = _player.transform.position;

        Vector3 scale = _newLine.transform.localScale;
        scale.x = distance * 2.0f;
        _newLine.transform.localScale = scale;

        Vector2 dir = CommonFuncs.CalcDir(_tana, _player);
        float angle = CommonFuncs.DirToDegree(dir);
        _newLine.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        if (_sfx)
            _sfx.PlaySFX("tana_move");

        StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject newSlash = Instantiate(_slashPrefab);

        newSlash.transform.position = _newLine.transform.position;
        newSlash.transform.localScale = _newLine.transform.localScale / 7.0f;
        Vector3 rot = _newLine.transform.rotation.eulerAngles;
        rot.x = 10.0f;
        newSlash.transform.Rotate(rot);

        BoxCollider2D col = newSlash.GetComponent<BoxCollider2D>();
        if (col)
        {
            Vector2 size = col.size;
            size.y = 2.0f;
            col.size = size;
        }

        GameObject killPlayer = Instantiate(_killPlayerBullet);
        killPlayer.transform.position = _player.transform.position;
        //Damager damager = newSlash.GetComponent<Damager>();
        Damager damager = killPlayer.GetComponent<Damager>();
        if (damager)
        {
            Damage damage = damager.damage;
            damage.damage = _player.GetComponent<Damagable>().currentHP;
            damager.damage = damage;
        }

        if (_sfx)
            _sfx.PlaySFX("tana_attack");

        Destroy(_newLine);
    }

    public void TestSuccess()
    {
        print("¼º°ø");
    }
}
