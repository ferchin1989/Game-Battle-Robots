using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectarPiso : MonoBehaviour
{

    public moviController detectarSuelo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        detectarSuelo.puedoSaltar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        detectarSuelo.puedoSaltar = false;
    }
}
