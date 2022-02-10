using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class cogerArmas : MonoBehaviourPunCallbacks
{
    public moviController movicontroller;
    public GameObject[] armas;
    public bool cooldown = false;

    void Update()
    {

        if(cooldown == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                cooldown = true;
            }

        }
    }

    [PunRPC]
    public void ActivarArmas(int numero)
    {
        for(int i = 0; i < armas.Length ; i++)
        {
            armas[i].SetActive(false);
        }
        armas[numero].SetActive(true);
        //anim arma
        movicontroller.conArma = true;
    }
    [PunRPC]
    public void DesactivarArmas()
    {
        
            if (armas[0].gameObject.activeInHierarchy == true)
            {
                armas[0].SetActive(false);
            }
            else if (armas[1].gameObject.activeInHierarchy == true)
            {
                armas[1].SetActive(false);
            }

            //anim arma
            movicontroller.conArma = false;


    }



}
