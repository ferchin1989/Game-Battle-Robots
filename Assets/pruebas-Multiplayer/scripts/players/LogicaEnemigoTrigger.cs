using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEnemigoTrigger : MonoBehaviour
{
    public int hp;
    public int dañoPatada;
    public int dañoArma;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PatadaImpacto")
        {
            if(anim != null)
            {
                anim.Play("EnemigoDañoResibido");
            }

            hp -= dañoPatada;
        }

        if (other.gameObject.tag == "armaImpacto")
        {
            if (anim != null)
            {
                anim.Play("EnemigoDañoResibido");
            }

            hp -= dañoArma;
        }

        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }
}
