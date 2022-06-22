using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargaInterfazHabilidades : MonoBehaviour
{
    private TextMeshProUGUI[] habilidades;

    private InteractuarBotonHabilidad[] scriptsBotones;
    private void Start()
    {
        GameManager.instancia.cargaInterfazHabilidades = this;

        habilidades = GetComponentsInChildren<TextMeshProUGUI>();

        scriptsBotones = GetComponentsInChildren<InteractuarBotonHabilidad>();

        for (int i = 0; i < GameManager.instancia.jugador.habilidades.Count; i++)
        {
            habilidades[i].text = GameManager.instancia.jugador.habilidades[i].nombre;
        }
    }

    public void ReseteaTextoHabilidades()
    {
        for (int i = 0; i < habilidades.Length; i++)
        {
            habilidades[i].text = scriptsBotones[i].habilidad.nombre;
        }
    }
}
