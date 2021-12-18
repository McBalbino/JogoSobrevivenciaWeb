using System;
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

    //destruindo um inimigo com a bala
    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
        }
        
        //destruindo a bala depois dela colidir com qualquer coisa
        Destroy(gameObject);
    }
}
