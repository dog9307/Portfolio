using System.Collections;
using UnityEngine;

public class TowerLobbyDamagableRestarter : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _towerAmbient;

    [SerializeField]
    private TutorialPopUp _firstTowerPopUp;
    [SerializeField]
    private TutorialPopUp _returnWorldPopUp;

    private PlayerAnimController _playerAnim;

    private void OnEnable()
    {

        if (!_playerAnim)
            _playerAnim = FindObjectOfType<PlayerAnimController>();

        if (_playerAnim)
            _playerAnim.ReflectionOn(true);

        int firstTower = PlayerPrefs.GetInt("FirstTowerTutoEnd", 0);
        if (firstTower != 0)
        {
            if (_firstTowerPopUp)
                Destroy(_firstTowerPopUp.gameObject);
        }
        else
            StartCoroutine(WaitForTuto());

        int gardenIn = PlayerPrefs.GetInt("TowerGardenIn", 0);
        if (gardenIn >= 100)
        {
            int tuto = PlayerPrefs.GetInt("ReturnWorldTuto", 0);
            if (tuto >= 100)
            {
                if (_returnWorldPopUp)
                    Destroy(_returnWorldPopUp.gameObject);
            }
            else
                StartCoroutine(WaitForReturnTuto());
        }

    }

    IEnumerator WaitForTuto()
    {
        if (_firstTowerPopUp)
        {
            //_firstTowerPopUp.StartPopUp();
            while (!_firstTowerPopUp.isPopUpOver)
                yield return null;
        }

        PlayerPrefs.SetInt("FirstTowerTutoEnd", 100);
    }

    IEnumerator WaitForReturnTuto()
    {
        if (_returnWorldPopUp)
        {
            //_returnWorldPopUp.StartPopUp();
            while (!_returnWorldPopUp.isPopUpOver)
                yield return null;
        }

        PlayerPrefs.SetInt("ReturnWorldTuto", 100);
    }

    private void OnDisable()
    {
        if (_towerAmbient)
            _towerAmbient.Stop();

        if (!_playerAnim)
            _playerAnim = FindObjectOfType<PlayerAnimController>();

        if (_playerAnim)
            _playerAnim.ReflectionOn(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>().isAleardyHitted = false;
    }
}
