using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;
    
    private Rigidbody rigibodyInimigo;
    private Animator animatorInimigo;

    public float Velocidade = 5;
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
        rigibodyInimigo = GetComponent<Rigidbody>();
        animatorInimigo = GetComponent<Animator>();
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
            rigibodyInimigo.MovePosition
            (rigibodyInimigo.position + 
             direcao.normalized * Velocidade * Time.deltaTime);
            
            animatorInimigo.SetBool("Atacando", false);

        }
        else
        {
            //fazendo os zumbis atacarem
            animatorInimigo.SetBool("Atacando", true);
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
