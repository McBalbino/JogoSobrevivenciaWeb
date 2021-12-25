using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    
    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador")
            .GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;

    }
    

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = 
            "Você sobreviveu por " + minutos + "min e " + segundos +"s";
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }
}
