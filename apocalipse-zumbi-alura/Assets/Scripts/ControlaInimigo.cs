using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    public float Velocidade = 5;
    private AnimacaoPersonagem animacaoInimigo;
    
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        AleatorizarZumbi();
    }
    
    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        //fazendo os zumbis olharem na direcao do jogador
        Vector3 direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            //fazendo os zumbis pararem perto do jogador
            movimentaInimigo.Movimentar(direcao, Velocidade);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            //fazendo os zumbis atacarem
            animacaoInimigo.Atacar(true);
        }
    }
    
    //reiniciando o jogo ao ser atacado
    void AtacaJogador()
    {
        int dano = Random.Range(20, 25);
        //pegando uma variavel de outra classe
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }
}
