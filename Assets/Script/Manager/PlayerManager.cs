using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public  static PlayerManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject currenPlayer;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]private bool isPlayerLive = false;
   

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }

    public void SpawnPlayer()
    {
         currenPlayer =   Instantiate(player, transform.position, transform.rotation);
       
        cinemachineVirtualCamera.Follow = currenPlayer.transform;
        isPlayerLive = true;
    }
    public GameObject GetPlayer()
    {
        return currenPlayer;
    }
    public bool GetPlayerStatus()
    {
        return isPlayerLive;
    }
    public void SetPlayerStatus(bool value)
    {
        isPlayerLive = value;
    }

}
