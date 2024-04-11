using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;


public class Loja : MonoBehaviour
{



    public TextMeshProUGUI textoPontuacao;
    private int pontuacaoJogador;

    public static int preco = 200;
    public static int dano = 1;


    // Métodos para comprar itens na loja
    public void ComprarItem()
    {
        if (SpawGayLuis.pontuacao >= preco)
        {
            SpawGayLuis.pontuacao -= preco;
            // Adicione os efeitos do item comprado

            // Atualizar a tela de pontos
            GameObject.Find("Spaw").GetComponent<SpawGayLuis>().AdicionarPontos(0);
        }
        else
        {
            Debug.Log("Pontuação insuficiente para comprar este item!");
        }
    }

}