using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargaInterfazHabilidades : MonoBehaviour
{
    private TextMeshProUGUI[] habilidades;

    private void Start()
    {
        GameManager.instancia.cargaInterfazHabilidades = this;

        habilidades = GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < GameManager.instancia.jugador.habilidades.Count; i++)
        {
            habilidades[i].text = GameManager.instancia.jugador.habilidades[i].nombre;
        }
    }
}
