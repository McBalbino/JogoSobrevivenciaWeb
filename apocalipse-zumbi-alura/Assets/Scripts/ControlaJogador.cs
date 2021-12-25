using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel
{
    private Vector3 direcao;
    public GameObject TextGameOver; //parte do voce perdeu
    public ControlaInterface scriptControlaInterface; //slider de vida
    public AudioClip SomDeDano; //colocando audio no jogador
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;
    public LayerMask MascaraChao; //limitando o raio so ate o chao pra nn pegar no hotel ou buraco etc

    //recomecando jogo
    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //fazendo o jogador andar
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        //configurando animacoes de ficar parado ou andar
        animacaoJogador.Movimentar(direcao.magnitude);
        
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
        scriptControlaInterface.AtualizarSliderVidaJogador(); //slider de vida
        ControlaAudio.instancia.PlayOneShot(SomDeDano); //colocando audio na vida
        //vida
        if (statusJogador.Vida <= 0)
        {
            //parte do voce perdeu
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }
}
