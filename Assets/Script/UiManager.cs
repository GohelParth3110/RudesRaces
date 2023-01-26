using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField] private UiGameOver uiGameover;
    [SerializeField] private UiRacerInformation uiRacerInformation;

    private void Awake()
    {
        instance = this;
    }
    public UiGameOver GetUiGameOverScreen()
    {
        return uiGameover;
    }
    public UiRacerInformation GetUiRacerInformation()
    {
        return uiRacerInformation;
    }
}
