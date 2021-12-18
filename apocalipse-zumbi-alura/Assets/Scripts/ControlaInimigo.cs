using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;

    public float Velocidade = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        //fazendo os zumbis olharem na direcao do jogador
        Vector3 direcao = Jogador.transform.position - transform.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
                
        GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        if (distancia > 2.5)
        {
            //fazendo os zumbis pararem perto do jogador
            GetComponent<Rigidbody>().MovePosition
            (GetComponent<Rigidbody>().position + 
             direcao.normalized * Velocidade * Time.deltaTime);
            
            GetComponent<Animator>().SetBool("Atacando", false);

        }
        else
        {
            //fazendo os zumbis atacarem
            GetComponent<Animator>().SetBool("Atacando", true);
        }
    }
    
    //reiniciando o jogo ao ser atacado
    void AtacaJogador()
    {
        Time.timeScale = 0;
        //parte do voce perdeu
        Jogador.GetComponent<ControlaJogador>().TextGameOver.SetActive(true);
        Jogador.GetComponent<ControlaJogador>().Vivo = false;
    }
}
