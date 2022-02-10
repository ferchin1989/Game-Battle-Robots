using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class playerSetup : MonoBehaviourPunCallbacks
{
    public Camera camare;


    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine == true)
        {
            transform.GetComponent<moviController>().enabled = true;
            transform.GetComponent<cogerArmas>().enabled = true;
            transform.GetComponent<ActivarArmaPlayer>().enabled = true;
            camare.enabled = true;
        }
        else
        {
            transform.GetComponent<moviController>().enabled = false;
            transform.GetComponent<cogerArmas>().enabled = false;
            transform.GetComponent<ActivarArmaPlayer>().enabled = false;
            camare.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {

        }
    }

}
