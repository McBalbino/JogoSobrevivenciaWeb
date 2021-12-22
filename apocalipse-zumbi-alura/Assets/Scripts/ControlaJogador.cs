﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    private Vector3 direcao;
    //parte do voce perdeu
    public GameObject TextGameOver;
    //slider de vida
    public ControlaInterface scriptControlaInterface;
    //colocando audio no jogador
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    //recomecando jogo
    private void Start()
    {
        Time.timeScale = 1;
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    //limitando o raio so ate o chao pra nn pegar no hotel ou buraco etc
    public LayerMask MascaraChao;
    // Update is called once per frame
    void Update()
    {
        //fazendo o jogador andar
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        //configurando animacoes de ficar parado ou andar
        animacaoJogador.Movimentar(direcao.magnitude);


        if (statusJogador.Vida <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    //movendo jogador
    private void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        //slider de vida
        scriptControlaInterface.AtualizarSliderVidaJogador();
        //colocando audio na vida
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        //vida
        if (statusJogador.Vida <= 0)
        {
            //parte do voce perdeu da parte 1
            Time.timeScale = 0;
            TextGameOver.SetActive(true);
        }
    }
}
