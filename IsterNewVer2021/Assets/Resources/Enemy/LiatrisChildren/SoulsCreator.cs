using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsCreator : MonoBehaviour,IObjectCreator
{

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }
    
    public void Start()
    {
        Reload();
        
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = this.transform.position;
        return newBullet;
    }

    public void Reload()
    {
    }

    public void FireBullets()
    {
        Vector3 virpos = new Vector2(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f));
        Vector2 dir = (virpos - this.transform.position).normalized;
        float dot = Vector3.Dot(Vector2.right, dir);
        float startAngle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            startAngle = Mathf.PI * 2 - startAngle;

        GameObject newBullet = CreateObject();
        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

        float angle = startAngle;

        if (angle > Mathf.PI * 2)
            angle -= Mathf.PI * 2;

        controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
