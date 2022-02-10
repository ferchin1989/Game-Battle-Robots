using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuControlador : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [Header("Pantallas")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject crearSalaPantalla;
    [SerializeField] private GameObject lobbyPantalla;
    [SerializeField] private GameObject lobbyNavegadorPantalla;

    [Header("Menu Principal")]
    [SerializeField] private Button btnCrearSala;
    [SerializeField] private Button btnEncontrarSala;
    [SerializeField] private Button btnSalaAleatoria;


    [Header("Lobby")]
    [SerializeField] private TextMeshProUGUI txtListaJugadores;
    [SerializeField] private TextMeshProUGUI txtInfoSala;
    [SerializeField] private Button btnIniciarJuego;

    [Header("Lobby Navegador")]
    [SerializeField] private RectTransform ContenedorSala;
    [SerializeField] private GameObject elementoPrefabSala;

    private List<GameObject> salaElementos = new List<GameObject>();
    private List<RoomInfo> listaSalas = new List<RoomInfo>();


    void Start()
    {
        Debug.Log("Start");
        btnCrearSala.interactable = false;
        btnEncontrarSala.interactable = false;
        btnSalaAleatoria.interactable = false;

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true; 
        }
    }

    //variables pantallas u objetos pantallas
    public void SetPantalla(GameObject screen)
    {
        menuPrincipal.SetActive(false);
        crearSalaPantalla.SetActive(false);
        lobbyPantalla.SetActive(false);
        lobbyNavegadorPantalla.SetActive(false);

        screen.SetActive(true);
        Debug.Log(screen);

        if(screen == lobbyNavegadorPantalla)
        {
            ActualizarLobbyNavegador();
        }
    }

    public void OnNombreJugadorCambia (TMP_InputField inpJugadorNombre)
    {
        PhotonNetwork.NickName = inpJugadorNombre.text;
        Debug.Log($"PhotonNetwork.NickName = {PhotonNetwork.NickName}");
    }


    //conexion
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        btnCrearSala.interactable = true;
        btnEncontrarSala.interactable = true;
        btnSalaAleatoria.interactable = true;
    }


    //pantallas 
    public void OnCrearSalaClicked()
    {
        SetPantalla(crearSalaPantalla);
    }

    public void onEncontrarSalaClicked()
    {
        SetPantalla(lobbyNavegadorPantalla);
    }

    //accion botones
    public void onCrearSalaBoton(TMP_InputField nombre)
    {
        Debug.Log("onCrearSalaBoton");
        NetworkManager.instancia.CrearSala(nombre.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom - lobbyPantalla");
        SetPantalla(lobbyPantalla);
        photonView.RPC("ActualizarLobby", RpcTarget.All);
       

    }
    [PunRPC]

    void ActualizarLobby()
    {
        btnIniciarJuego.interactable = PhotonNetwork.IsMasterClient;
        txtListaJugadores.text = "";
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            txtListaJugadores.text += p.NickName + "\n"; 
        }

        txtInfoSala.text = string.Format(@"<b>Nombre Sala: <b>{0}{1}", "\n", PhotonNetwork.CurrentRoom.Name);
    }


    private GameObject CrearSalaBoton()
    {
        GameObject obj = Instantiate(elementoPrefabSala, ContenedorSala.transform);
        salaElementos.Add(obj);
        return obj;
    }

    void ActualizarLobbyNavegador()
    {
        foreach(GameObject b in salaElementos)
        {
            b.SetActive(false);
        }

        for (int x = 0; x < listaSalas.Count; x++)
        {
            GameObject boton = x >= salaElementos.Count ? CrearSalaBoton() : salaElementos[x];
            boton.SetActive(true);

            boton.transform.Find("txtNombreSala").GetComponent<TextMeshProUGUI>().text = listaSalas[x].Name;
            boton.transform.Find("txtCantidadJugadores").GetComponent<TextMeshProUGUI>().text = listaSalas[x].PlayerCount + "/" + listaSalas[x].MaxPlayers;

            Button b1 = boton.GetComponent<Button>();
            string nombre = listaSalas[x].Name;
            b1.onClick.RemoveAllListeners();
            b1.onClick.AddListener(() => { OnUnirseSalaClicked(nombre); });
        }
    }

    public void OnResfrescarClicked()
    {
        ActualizarLobbyNavegador();
    }

    private void OnUnirseSalaClicked(string nombre)
    {
        NetworkManager.instancia.UnirseSala(nombre);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        listaSalas = roomList;
    }

}
