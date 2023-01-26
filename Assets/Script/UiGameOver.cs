using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_PlayerRank;

    private void Start()
    {
        txt_PlayerRank.text = "player rank is " + RaceManger.instance.GetPlayerRank();
    }

    public void OnClickOn_RacerBtnInformation()
    {
        UiManager.instance.GetUiGameOverScreen().gameObject.SetActive(false);
        UiManager.instance.GetUiRacerInformation().gameObject.SetActive(true);
    }
}
