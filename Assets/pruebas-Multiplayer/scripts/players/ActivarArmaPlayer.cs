using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ActivarArmaPlayer : MonoBehaviourPunCallbacks
{

    public cogerArmas CogerArmas;
    public int numeroArma;

    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        { 
            if(Input.GetKeyDown(KeyCode.R))
            {
                photonView.RPC("Desactivar_Arma", RpcTarget.All);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn") 
        {
            photonView.RPC("Desactivar_Arma",RpcTarget.All);
        }


        if(other.tag == "Arma1" )
        {
            numeroArma = 0;
            print("toque arma cero");
        }
        else if(other.tag == "Arma2")
        {
            numeroArma = 1;
            print("toque arma uno");
        }



        if(other.tag == "Arma1" || other.tag == "Arma2")
        {
            photonView.RPC("AgarrarArma",RpcTarget.All);
        }
    }
    [PunRPC]
    public void AgarrarArma()
    {
         CogerArmas.ActivarArmas(numeroArma);
        //Destroy(arma.gameObject);  
        print("Agarre Arma");
    }
    [PunRPC]
    public void Desactivar_Arma()
    {     
        CogerArmas.DesactivarArmas();
        print("Desactive arma");
    }

}
    