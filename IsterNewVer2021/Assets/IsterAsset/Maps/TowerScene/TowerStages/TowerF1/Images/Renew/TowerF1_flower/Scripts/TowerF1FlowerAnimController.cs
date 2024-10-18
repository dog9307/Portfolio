using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1FlowerAnimController : AnimController
{
    [SerializeField]
    private bool _isAreaFlower = false;
    [SerializeField]
    private GameObject _relativeCutScene;

    public void Hit()
    {
        _anim.SetTrigger("hit");
    }

    public override void Die()
    {
        base.Die();

        GetComponent<Collider2D>().enabled = false;

        // test
        if (_isAreaFlower)
        {
            SavableNode node = new SavableNode();
            node.key = "FieldFlowerDie";
            node.value = 100;

            SavableDataManager.instance.AddSavableObject(node);

            if (_relativeCutScene)
                _relativeCutScene.SetActive(true);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
    //        Hit();
    //}
}
