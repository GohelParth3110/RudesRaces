using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiRacerInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] all_RacerName;
    [SerializeField] private TextMeshProUGUI[] all_Time;
    [SerializeField] private TextMeshProUGUI[] all_Rank;
    private List<string> list_name;
    private List<float> list_Time;

    private void Start()
    {
        list_name = RaceManger.instance.GetListOfRacerName();
        list_Time = RaceManger.instance.GetListOfRacertime();
        for (int i = 0; i < all_RacerName.Length; i++)
        {
            all_Rank[i].text = (i+1).ToString();
            all_RacerName[i].text = list_name[i];
            all_Time[i].text = list_Time[i].ToString();
        }
    }
}
