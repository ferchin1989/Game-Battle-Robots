using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmManager : MonoBehaviourPunCallbacks
{
    public static GmManager instance;
    public GameObject playerPr;
    public Transform spawn1;
    public Transform spawn2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            PhotonNetwork.Instantiate(this.playerPr.name,spawn1.position, Quaternion.identity, 0);
        }
        else if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
           GameObject personaje = PhotonNetwork.Instantiate(this.playerPr.name, spawn2.position, Quaternion.identity, 0);
            personaje.transform.Rotate(0, 180, 0);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }

}
