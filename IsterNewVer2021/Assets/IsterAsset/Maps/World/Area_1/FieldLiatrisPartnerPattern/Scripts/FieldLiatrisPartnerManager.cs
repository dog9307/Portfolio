using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisPartnerManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private GameObject _nodePrefab;

    private List<FieldLiatrisPartnerNodeController> _nodes = new List<FieldLiatrisPartnerNodeController>();

    [Header("분신 갯수")]
    [SerializeField]
    private int _partnerCount = 10;

    [Header("사이 시간 설정")]
    [SerializeField]
    private float _startDelay = 0.3f;
    [SerializeField]
    private AnimationCurve _intervalCurve;
    [SerializeField]
    private float _endDelay = 1.0f;

    [Header("노드 사이 거리 설정")]
    [SerializeField]
    private float _distance = 10.0f;
    [SerializeField]
    private float _randomRange = 0.5f;
    [SerializeField]
    private float _movingTime = 0.1f;

    [Header("앵글 차이 랜덤")]
    [SerializeField]
    private float _angleRandomRange = 5.0f;

    private Coroutine _duringPattern;

    [HideInInspector]
    [SerializeField]
    private FieldLiatrisPartnerController _movingPartner;

    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private Texture2D[] _textures;

    [HideInInspector]
    [SerializeField]
    private GameObject _linePrefab;
    private List<GameObject> _lines = new List<GameObject>();
    [HideInInspector]
    [SerializeField]
    private GameObject _slashBulletPrefab;

    [SerializeField]
    private SFXPlayer _sfx;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _partnerCount; ++i)
        {
            GameObject newPartner = Instantiate(_nodePrefab);

            FieldLiatrisPartnerNodeController controller = newPartner.GetComponent<FieldLiatrisPartnerNodeController>();
            if (controller)
            {
                controller.manager = this;
                controller.transform.parent = transform;
                controller.transform.localPosition = Vector3.zero;
                _nodes.Add(controller);
            }
        }

        _movingPartner.manager = this;
    }

    // test
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F10))
    //    {
    //        StartPattern();
    //    }
    //}

    Vector3 CalcOffset(float targetAngle)
    {
        float randomRange = Random.Range(-_randomRange, _randomRange);
        float x = Mathf.Cos(targetAngle * Mathf.Deg2Rad) * (_distance / 2.0f + randomRange);
        float y = Mathf.Sin(targetAngle * Mathf.Deg2Rad) * (_distance / 2.0f + randomRange);

        return new Vector3(x, y, 0.0f);
    }

    public void StartPattern()
    {
        if (_duringPattern != null)
        {
            StopCoroutine(_duringPattern);
            _duringPattern = null;

            foreach (var l in _lines)
                Destroy(l);
            _lines.Clear();
        }

        FieldLiatrisPartnerNodeController firstNode = _nodes[0];

        float angle = Random.Range(0.0f, 360.0f);
        firstNode.transform.localPosition = CalcOffset(angle);

        firstNode.TurnOnNode(_movingPartner.currentSprite);

        _movingPartner.ReadyToMove(firstNode);

        _lines.Clear();

        _duringPattern = StartCoroutine(Pattern(angle));
    }

    public void EndPattern()
    {
        if (_duringPattern != null)
        {
            StopCoroutine(_duringPattern);
            _duringPattern = null;

            foreach (var l in _lines)
                Destroy(l);
            _lines.Clear();

            if (_movingPartner.isMove)
                _movingPartner.EndMove();
        }
    }

    IEnumerator Pattern(float currentAngle)
    {
        yield return new WaitForSeconds(_startDelay);

        float ratio = 1.0f / (float)_partnerCount;
        float interval = _intervalCurve.Evaluate(ratio);
        for (int i = 1; i < _partnerCount; ++i)
        {
            FieldLiatrisPartnerNodeController currentNode = _nodes[i];

            int randomSeed = Random.Range(2, _partnerCount / 2 + 1);

            currentAngle = currentAngle + 360.0f / _partnerCount * randomSeed;
            currentAngle += Random.Range(-_angleRandomRange, _angleRandomRange);

            currentNode.transform.localPosition = CalcOffset(currentAngle);

            int rnd = Random.Range(0, _sprites.Length);
            currentNode.TurnOnNode(_sprites[rnd]);

            _movingPartner.Move(currentNode, _sprites[rnd], _textures[rnd]);

            if (_sfx)
                _sfx.PlaySFX("tana_move");

            // create line
            GameObject newLine = Instantiate(_linePrefab);
            newLine.transform.position = (currentNode.transform.position + _nodes[i - 1].transform.position) / 2.0f;

            Vector3 scale = newLine.transform.localScale;
            scale.x = Vector3.Distance(currentNode.transform.position, _nodes[i - 1].transform.position);
            newLine.transform.localScale = scale;

            Vector2 dir = CommonFuncs.CalcDir(_nodes[i - 1], currentNode);
            float angle = CommonFuncs.DirToDegree(dir);
            newLine.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

            _lines.Add(newLine);

            yield return new WaitForSeconds(interval);

            ratio += 1.0f / (float)_partnerCount;
            interval = _intervalCurve.Evaluate(ratio);
        }

        yield return new WaitForSeconds(_endDelay);

        foreach (var node in _nodes)
            node.TurnOffNode();

        _movingPartner.EndMove();

        foreach (var l in _lines)
        {
            GameObject newSlash = Instantiate(_slashBulletPrefab);

            newSlash.transform.position = l.transform.position;
            newSlash.transform.localScale = l.transform.localScale / 7.0f;
            Vector3 rot = l.transform.rotation.eulerAngles;
            rot.x = 10.0f;
            newSlash.transform.Rotate(rot);

            Destroy(l);
        }
        _lines.Clear();

        if (_sfx)
            _sfx.PlaySFX("tana_attack");

        CameraShakeController.instance.CameraShake(10.0f);

        _duringPattern = null;
    }
}
