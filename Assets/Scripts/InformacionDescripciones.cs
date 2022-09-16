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
    public TextMeshProUGUI muestraTextoTier;
    public TextMeshProUGUI muestraTextoCosteAccion;
    public TextMeshProUGUI muestraValorCosteAccion;
    public TextMeshProUGUI muestraAtributos;

    public bool bloqueoDescripcion;

    private string fuerza;
    private string penetracion;

    public void MuestraInformacionHabilidad(Habilidad habilidad)
    {
        if (habilidad.penetracion == 0) penetracion = habilidad.penetracion.ToString();
        else penetracion = " -" + habilidad.penetracion.ToString();

        if(!(habilidad.descripcion.IndexOf(" ") < 0))
        {
            if (habilidad.descripcion.Substring(0, habilidad.descripcion.IndexOf(" ")) == "Range") fuerza = habilidad.fuerza.ToString();
            else fuerza = " +" + habilidad.fuerza.ToString();
        }
        
        muestraNombre.text = habilidad.nombre;
        muestraDescripcion.text = string.Format(habilidad.descripcion, fuerza, penetracion, " "+habilidad.daño);
        muestraTextoVelocidad.text = "V:";
        muestraValorVelocidad.text = habilidad.velocidad.ToString();
        if (muestraTextoTier != null) muestraTextoTier.text = habilidad.tier.ToString();
        muestraTextoCosteAccion.text = "PA:";
        muestraValorCosteAccion.text = habilidad.coste.ToString();
    }

    public void MuestraInformacionPersonaje(Personaje personaje)
    {
        muestraDescripcion.text = "";

        muestraNombre.text = personaje.nombre;

        foreach (Habilidad hab in personaje.habilidades)
        {
            muestraDescripcion.text += hab.nombre + "\n";
        }

        muestraTextoVelocidad.text = "PA:";
        muestraValorVelocidad.text = personaje.accionesMaximas.ToString();
        if (muestraTextoTier != null) muestraTextoTier.text = "lvl:"+personaje.nivel.ToString();
        muestraTextoCosteAccion.text = personaje.heridasActuales.ToString()+"/";
        muestraValorCosteAccion.text = personaje.heridasMaximas.ToString();

        if(muestraAtributos != null) { 
        muestraAtributos.text = 
            "HP_" + personaje.punteria + "\n" +
            "HC_" + personaje.habilidadCombate + "\n" +
            "HE_" + personaje.habilidadEspecial + "\n" +
            "F_" + personaje.fuerza + "\n" +
            "R_" + personaje.resistencia + "\n" +
            "A_" + personaje.agilidad + "\n" +
            "SV_" + personaje.salvacion + "\n" +
            "INV_" + personaje.salvacionInvulnerable;
        }
    }

   //public void LimpiaInformacion()
   //{
   //    muestraNombre.text = "";
   //    muestraDescripcion.text = "";
   //    muestraTextoVelocidad.text = "";
   //    muestraValorVelocidad.text = "";
   //    if (muestraTextoTier != null) muestraTextoTier.text = "";
   //    muestraTextoCosteAccion.text = "";
   //    muestraValorCosteAccion.text = "";
   //    if (muestraAtributos != null) muestraAtributos.text = "";
   //}
}
