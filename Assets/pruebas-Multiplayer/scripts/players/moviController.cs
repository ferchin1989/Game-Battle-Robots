using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class moviController :MonoBehaviourPunCallbacks//,IPunObservable
{
    
    //movimiento
    public new  Rigidbody rigidbody;
    float speed = 3.0f;
    public float fSalto = 1f;
    public bool puedoSaltar;


    //ataques
    public bool ataque1;
    public bool avanceAtaque1;
    public float impulsoDeGolpe1 = 10f;

    public bool conArma;

    //animacion
    private Animator anim;

    void Start()
    {
        puedoSaltar = true;
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //variablemos movimiento
        float hor = Input.GetAxisRaw("Vertical");
        float ver = Input.GetAxisRaw("Horizontal");

        //implementacion variables al rigidbody
        if (hor != 0.0f || ver != 0.0f)
        {
            if(!ataque1)
            {
                Vector3 dir = transform.forward * ver + transform.right * -hor;
                rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);
            }
            
        }


        //salto
        if (puedoSaltar)
        {
            if(!ataque1)
            {

                if (Input.GetKey(KeyCode.Space))
                {
                    anim.SetBool("Salte", true);
                    rigidbody.AddForce(new Vector3(0, fSalto, 0), ForceMode.Impulse);
                }
            }

            anim.SetBool("TocarSuelo", true);
        }
        else
        {
            EstoyCayendo();
            
        }

        //ataques

        if(avanceAtaque1)
        {
            rigidbody.velocity = -transform.forward * impulsoDeGolpe1;
        }

        if(Input.GetKey(KeyCode.Z) && puedoSaltar && !ataque1)
        {
            

            if(conArma)
            {
                anim.SetTrigger("GolpeEspada1");
                ataque1 = true;
            }
            else
            {
                anim.SetTrigger("GolpePatadaBaja");
                ataque1 = true;
            }
        }

        //animaciones
        anim.SetFloat("VelX", hor);
        anim.SetFloat("VelY", ver);
    }

    public void EstoyCayendo()
    {
        anim.SetBool("Salte", false);
        anim.SetBool("TocarSuelo", false);
    }

    public void TerminoAtaque1()
    {
        ataque1 = false;
    }

    public void AvanceAtaque1()
    {
        avanceAtaque1 = true;
    }

    public void TerminarDeAvanzar1()
    {
        avanceAtaque1 = false;
    }

}