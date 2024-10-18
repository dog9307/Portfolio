using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingIllustSignal : MonoBehaviour
{
    [SerializeField]
    private string _illustKeyward = "";

    public void SendIllustKeyward()
    {
        EndingIllustSender sender = FindObjectOfType<EndingIllustSender>();
        if (!sender) return;

        sender.SendEndingKeyward(_illustKeyward);
    }

    public void EndSignal()
    {
        BlackMaskController.instance.AddEvent(GoToDieScene, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.StartFading();
    }

    public void GoToDieScene()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
            player.PlayerMoveFreeze(false);

        SavableDataManager.instance.PlayerDieSave();

        SceneManager.LoadScene("PlayerDieScene", LoadSceneMode.Single);
    }
}
