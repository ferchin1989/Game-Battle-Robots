using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public byte maximoJugadores = 2;
    string gameVersion = "1";
    bool isConnecting;

    public static NetworkManager instancia;

    private void Awake()
    {
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {        
            PhotonNetwork.JoinLobby();
            isConnecting = false;
            Debug.LogFormat("Conectados al servidor principal");
        }

        
    }

    public void Conectar()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("JoinRandomRoom");
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public void CrearSala(string nombre)
    {
        RoomOptions opciones = new RoomOptions
        {
            MaxPlayers = (byte)maximoJugadores
        };

        PhotonNetwork.CreateRoom(nombre, opciones);
        Debug.Log("CreateRoom - sala creada");
    }

    public void UnirseSala(string nombre)
    {
        PhotonNetwork.JoinRoom(nombre);
    }

    [PunRPC]

    public void CambiarEscena(string escena)
    {
        PhotonNetwork.LoadLevel(escena);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("inicio");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //MenuControlador.instan.UpdatePlayerInfoText();

        if(PhotonNetwork.IsMasterClient)
        {
            //GameManager.instance.CheckWinCondition();
        }

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Fallo al conectar");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maximoJugadores });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Se conecto a la sala");
        PhotonNetwork.LoadLevel("juego");
    }

}
