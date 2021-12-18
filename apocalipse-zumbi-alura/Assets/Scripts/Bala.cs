using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    // Start is called before the first frame update

    // fazendo bala ir em linha reta
    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition
            (GetComponent<Rigidbody>().position + 
             transform.forward * Velocidade * Time.deltaTime);
    }
}
