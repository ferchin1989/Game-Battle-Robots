using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Autoloby : MonoBehaviourPunCallbacks
{
    //public Button btnConexion;
    public Button btnJungar;
    public Text log;
    public Text playerCount;
    public int playersCount;

    public byte maxPlayersSala;

    public void Conectar()
    {
        if(!PhotonNetwork.IsConnected)
        {
            if(PhotonNetwork.ConnectUsingSettings())
            {
                log.text += "\n Conectado al Servidor";
            }
            else
            {
                log.text += "\n fallo al conectar al servidor";
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        //btnConexion.interactable = false;
        btnJungar.interactable = true;
    }

    public void ConexionAleatoria()
    {
        if(!PhotonNetwork.JoinRandomRoom())
        {
            log.text += "\n fallo al Unirse a la sala";
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        log.text = "\n No hay salas a las que unirse, Creando sala ...";
        log.text = "\n Creando sala ...";

        if (PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() {MaxPlayers = maxPlayersSala }))
        {
            log.text += "\n Sala creada";
        }
        else
        {
            log.text += "\n Creando sala";
        }

    }

    public override void OnJoinedRoom()
    {
        log.text += "\n Estas en la sala";
        btnJungar.interactable = false;
    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.CurrentRoom != null)
        
            playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
            playerCount.text = playersCount + "/" + maxPlayersSala;
        
        

    }

}
          