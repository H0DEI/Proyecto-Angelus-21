using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBotones : MonoBehaviour
{
    public void NuevaPartida(Escena escena)
    {
        GameManager.instancia.ActivaInterfaz();

        GameManager.instancia.CompruebaYCargaEscenas(escena);
    }
}
