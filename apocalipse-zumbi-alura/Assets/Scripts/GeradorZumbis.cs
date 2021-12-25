using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// criando zumbis
public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;

    private float contadorTempo = 0;

    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;

        if (contadorTempo >= TempoGerarZumbi)
        {
            GerarNovoZumbi();
            contadorTempo = 0;
        }
    }

    void GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        if (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        }
        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 3;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
