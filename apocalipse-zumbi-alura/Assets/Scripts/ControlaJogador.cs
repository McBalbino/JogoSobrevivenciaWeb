using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;

    private Vector3 direcao;

    private Rigidbody rigibodyJogador;
    private Animator animatorJogador;
    
    //criando a barra de vida
    public int Vida = 100;
    
    //parte do voce perdeu
    public GameObject TextGameOver;
    
    //slider de vida
    public ControlaInterface scriptControlaInterface;

    //recomecando jogo
    private void Start()
    {
        Time.timeScale = 1;
        rigibodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
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
        if (direcao != Vector3.zero)
        {
            animatorJogador.SetBool("Movendo", true);
        }
        else
        {
            animatorJogador.SetBool("Movendo", false);
        }

        if (Vida <= 0)
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
        rigibodyJogador.MovePosition
        (rigibodyJogador.position + 
         (direcao * Velocidade * Time.deltaTime));
        
        //rotacao do jogador a partir do mouse
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;
        
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            
            rigibodyJogador.MoveRotation(novaRotacao);
        }
    }

    public void TomarDano(int dano)
    {
        Vida -= dano;
        //slider de vida
        scriptControlaInterface.AtualizarSliderVidaJogador();
        //vida
        if (Vida <= 0)
        {
            //parte do voce perdeu da parte 1
            Time.timeScale = 0;
            TextGameOver.SetActive(true);
        }
    }
}
