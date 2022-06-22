using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformacionDescripciones : MonoBehaviour
{
    public TextMeshProUGUI muestraNombre;
    public TextMeshProUGUI muestraDescripcion;
    public TextMeshProUGUI muestraTextoVelocidad;
    public TextMeshProUGUI muestraValorVelocidad;
    public TextMeshProUGUI muestraTextoCosteAccion;
    public TextMeshProUGUI muestraValorCosteAccion;
    public TextMeshProUGUI muestraAtributos;

    void Start()
    {
        LimpiaInformacion();
    }

    public void MuestraInformacionHabilidad(Habilidad habilidad)
    {
        muestraNombre.text = habilidad.nombre;
        muestraDescripcion.text = string.Format(habilidad.descripcion, habilidad.daño);
        muestraTextoVelocidad.text = "V:";
        muestraValorVelocidad.text = habilidad.velocidad.ToString();
        muestraTextoCosteAccion.text = "PA:";
        muestraValorCosteAccion.text = habilidad.coste.ToString();
    }

    public void MuestraInformacionPersonaje(Personaje personaje)
    {
        muestraNombre.text = personaje.nombre;

        foreach (Habilidad hab in personaje.habilidades)
        {
            muestraDescripcion.text += hab.nombre + "\n";
        }

        muestraTextoVelocidad.text = "PA:";
        muestraValorVelocidad.text = personaje.accionesMaximas.ToString();
        muestraTextoCosteAccion.text = personaje.heridasActuales.ToString()+"/";
        muestraValorCosteAccion.text = personaje.heridasMaximas.ToString();
        muestraAtributos.text = 
            "HP_" + personaje.punteria + "\n" +
            "HC_" + personaje.habilidadCombate + "\n" +
            "HE_" + personaje.habilidadEspecial + "\n" +
            "F_" + personaje.fuerza + "\n" +
            "R_" + personaje.resistencia + "\n" +
            "SV_" + personaje.salvacion + "\n" +
            "INV_" + personaje.salvacionInvulnerable;
    }

    public void LimpiaInformacion()
    {
        muestraNombre.text = "";
        muestraDescripcion.text = "";
        muestraTextoVelocidad.text = "";
        muestraValorVelocidad.text = "";
        muestraTextoCosteAccion.text = "";
        muestraValorCosteAccion.text = "";
        muestraAtributos.text = "";
    }
}
