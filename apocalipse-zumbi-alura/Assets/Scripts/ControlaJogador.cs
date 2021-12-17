using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;

    private Vector3 direcao;
    // Update is called once per frame
    void Update()
    {
        //fazendo o jogador andar
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        //configurando animacoes de ficar parado ou andar
        if (direcao != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Movendo", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Movendo", false);
        }
    }

    //movendo jogador
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition
        (GetComponent<Rigidbody>().position + 
         (direcao * Velocidade * Time.deltaTime));
    }
}
