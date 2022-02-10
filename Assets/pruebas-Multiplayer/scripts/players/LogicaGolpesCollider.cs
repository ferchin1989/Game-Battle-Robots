using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaGolpesCollider : MonoBehaviour
{

    public BoxCollider[] armasBoxCol;
    public CapsuleCollider patadaBoxCol;
    public moviController movicontroller;

    public GameObject[] armas;
    
    void Start()
    {
        DesactivarColloderArmas();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            DesactivarArmas();
        }
    }

    
    public void ActivarArma(int numero)
    {
        for(int i=0; i<armas.Length;i++)
        {
            armas[i].SetActive(false);
        }

        //animacion arma
        //moviController.conArma = true;

    }

    public void DesactivarArmas()
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }

        //Animacion arma;
        movicontroller.conArma = false;
    }

    public void ActivarColliderArmas()
    {
        for(int i=0;i<armasBoxCol.Length;i++ )
        {
            if(movicontroller.conArma)
            {
                if(armasBoxCol[i] != null)
                {
                    armasBoxCol[i].enabled = true;
                }
            }
            else
            {
                patadaBoxCol.enabled = true;    
            }
        }
    }

    public void DesactivarColloderArmas()
    {
        for (int i = 0; i < armasBoxCol.Length; i++)
        {
            
            if (armasBoxCol[i] != null)
            {
                armasBoxCol[i].enabled = false;
            }
            

        }
        patadaBoxCol.enabled = false;
    }
    
    
}
