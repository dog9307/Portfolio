using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMovableCharacter : MonoBehaviour
{
    #region SINGLETON
    static private MinimapMovableCharacter _instance;
    static public MinimapMovableCharacter instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<MinimapMovableCharacter>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "MinimapMovableCharacter";
                _instance = container.AddComponent<MinimapMovableCharacter>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private float _moveSpeed = 100.0f;

    [SerializeField]
    private MinimapNodes _currentNode;
    public MinimapNodes currentNode { get { return _currentNode; } set { _currentNode = value; } }

    private List<MinimapNodes> _pathList = new List<MinimapNodes>();

    [SerializeField]
    private Transform _movableImage;
    [SerializeField]
    private Transform _partnerRotationPivot;
    [SerializeField]
    private Transform _partnerTargetPos;
    [SerializeField]
    private Transform _partnerPos;

    [SerializeField]
    private float _partnerRatio = 0.04f;
    private float _scaleFactor = -1.0f;

    [SerializeField]
    private Animator _anim;

    private bool _isMoving = false;

    void Update()
    {
        _partnerPos.position = Vector3.Lerp(_partnerPos.position, _partnerTargetPos.position, _partnerRatio);

        Vector3 scale = Vector3.Lerp(_movableImage.localScale, new Vector3(_scaleFactor, 1.0f, 1.0f), 0.04f);
        _movableImage.localScale = scale;
        scale.x *= -1.0f;
        _partnerPos.localScale = scale;
    }

    public void SetPos(MinimapNodes node)
    {
        _currentNode = node;

        transform.parent = node.transform;
        transform.localPosition = Vector3.zero;
    }

    public void MoveStart(MinimapNodes targetNode)
    {
        StopAllCoroutines();

        _pathList.Clear();

        _currentNode.FindNodes(_pathList, _currentNode, targetNode);

        StartCoroutine(MoveAnim());
    }

    IEnumerator MoveAnim()
    {
        _isMoving = true;

        _anim.Play("move");

        int startIndex = _pathList.Count - 1;
        int endIndex = startIndex - 1;

        while (endIndex >= 0)
        {
            _currentNode = _pathList[startIndex];

            Vector2 dir = (_pathList[endIndex].transform.position - _pathList[startIndex].transform.position).normalized;

            float angle = 0.0f;
            if (dir == Vector2.right)
            {
                angle = 0.0f;
                _scaleFactor = -1.0f;
            }
            else if (dir == Vector2.left)
            {
                angle = 180.0f;
                _scaleFactor = 1.0f;
            }
            else if (dir == Vector2.up)
                angle = 90.0f;
            else if (dir == Vector2.down)
                angle = 270.0f;

            _partnerRotationPivot.transform.localRotation = Quaternion.identity;
            _partnerRotationPivot.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

            yield return StartCoroutine(Move(_pathList[startIndex], _pathList[endIndex]));

            startIndex--;
            endIndex--;
        }
        _currentNode = _pathList[0];

        _anim.Play("idle");

        _isMoving = false;
    }

    IEnumerator Move(MinimapNodes startNode, MinimapNodes endNode)
    {
        transform.parent = endNode.transform;
        Vector3 startPos = transform.localPosition;

        float totalTime = Vector3.Distance(startNode.transform.position, endNode.transform.position) / _moveSpeed;
        float currentTime = 0.0f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, ratio);

            yield return null;

            currentTime += TimeManager.originDeltaTime;
        }
        transform.localPosition = Vector3.zero;
    }

    public void MoveEndInterrupt()
    {
        if (!_isMoving) return;

        StopAllCoroutines();

        SetPos(_pathList[0]);

        _anim.Play("idle");

        _isMoving = false;
    }
}
