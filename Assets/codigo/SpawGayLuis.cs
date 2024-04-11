using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawGayLuis : MonoBehaviour
{
    public float intervaloSpawn = 0.5f;
    public Transform prefabColetavel;
    public Transform areaSpawn;
    public static int pontuacao = 0;
    public TextMeshProUGUI textoPontuacao;
    private bool inimigoNaCena = false;
    public int vidaInimigo = 3; // N�mero de cliques necess�rios para destruir o inimigo
    void Start()
    {
        // Inicia o spawn apenas uma vez no in�cio
        SpawnarInimigo();
    }

    void Update()
    {
        // Verifica se o inimigo atual foi destru�do e, em seguida, permite o spawn de um novo inimigo
        if (!inimigoNaCena)
        {
            SpawnarInimigo();
        }

        // Verifica se houve um toque na tela
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Converte a posi��o do toque para um raio no mundo do jogo
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Verifica se o raio atinge algum inimigo
            if (Physics.Raycast(ray, out hit))
            {
                // Se o objeto atingido for um inimigo, reduz a vida e destrua-o se a vida chegar a zero
                if (hit.transform.CompareTag("Inimigo"))
                {

                    vidaInimigo = vidaInimigo - Loja.dano; // Reduz a vida do inimigo em 1
                    if (vidaInimigo <= 0)
                    {
                        Destroy(hit.transform.gameObject);
                        AdicionarPontos(10);
                        InimigoDestruido(); // Chama o m�todo para atualizar a vari�vel inimigoNaCena
                    }
                }
            }
        }
    }

    void SpawnarInimigo()
    {
        float areaX = areaSpawn.localScale.x / 2;
        float areaZ = areaSpawn.localScale.z / 2;

        float randomX = Random.Range(-areaX, areaX);
        float randomZ = Random.Range(-areaZ, areaZ);

        Vector3 localSpawn = new Vector3(randomX, 0.5f, randomZ);

        Transform instancia = Instantiate(prefabColetavel, localSpawn, Quaternion.identity);
        instancia.position = localSpawn;
        instancia.tag = "Inimigo";

        // Define inimigoNaCena como true quando um inimigo � gerado
        inimigoNaCena = true;
        vidaInimigo = 3; // Reinicia a vida do inimigo ao gerar um novo inimigo
    }

    // M�todo para ser chamado quando um inimigo � destru�do
    public void InimigoDestruido()
    {
        // Define inimigoNaCena como false quando um inimigo � destru�do
        inimigoNaCena = false;
    }

    // M�todo para adicionar pontos
    public void AdicionarPontos(int pontos)
    {
        pontuacao += pontos;
        if (textoPontuacao != null)
        {
            textoPontuacao.text = pontuacao.ToString();
        }
    }

    // Restante do seu c�digo permanece inalterado...
}