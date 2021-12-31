using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    public GameObject KitMedicoPrefab;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradorZumbis meuGerador;
    
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        AleatorizarZumbi();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }
    
    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        
        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);
        
        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;
            //fazendo os zumbis pararem perto do jogador
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            //fazendo os zumbis atacarem
            animacaoInimigo.Atacar(true);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        }

    }
    
    //criando posicao aleatoria
    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }
    
    //reiniciando o jogo ao ser atacado
    void AtacaJogador()
    {
        int dano = Random.Range(5, 15);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if (statusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
