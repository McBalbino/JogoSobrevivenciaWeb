using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;

    public GameObject CanoDaArma;
    
    //colocando som no tiro
    public AudioClip SomDoTiro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calculando clique no botao do mouse
        if (Input.GetButtonDown("Fire1"))
        {
            //criando balas
            Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
            //som do tiro
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
        }
    }
}
