using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformacionInterfaz : MonoBehaviour
{
    public TextMeshProUGUI puntosVida;
    public TextMeshProUGUI puntosAccion;

    private string pv = "PV_{0}/{1}";
    private string pa = "PA_{0}/{1}";

    private Personaje jugador;
    private GameManager instancia;

    private void Start()
    {
        instancia = GameManager.instance;

        jugador = instancia.jugador;

        ActualizaPuntos();
    }

    public void ActualizaPuntos()
    {
        puntosVida.text = string.Format(pv, jugador.heridasActuales, jugador.heridasMaximas);
        puntosAccion.text = string.Format(pa, jugador.accionesActuales, jugador.accionesMaximas);
    }
}
