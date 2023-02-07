using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class HabilidadesALanzar : MonoBehaviour
{
    public List<KeyValuePair<Habilidad, bool>> listaHabilidadesALanzar = new List<KeyValuePair<Habilidad, bool>>();

    public TextMeshProUGUI textoNombreHabilidad;

    private Transform listadoHabilidades;


    private void Start()
    {
        listadoHabilidades = this.transform;

        GameManager.instance.habilidadesALanzar = this;
    }

    public void ActualizaLista()
    {
        transform.Clear();

        OrdenaLista();

        foreach(KeyValuePair<Habilidad, bool> habilidad in listaHabilidadesALanzar)
        {
            TextMeshProUGUI instanciaCajaTexto = GameObject.Instantiate(textoNombreHabilidad) as TextMeshProUGUI;
            instanciaCajaTexto.text = habilidad.Key.nombre;
            instanciaCajaTexto.transform.SetParent(listadoHabilidades);
            instanciaCajaTexto.GetComponent<InteractuarTextoListaHabilidad>().habilidad = habilidad.Key;
            instanciaCajaTexto.GetComponent<InteractuarTextoListaHabilidad>().soyJugador = habilidad.Value;
        }
    }

    public void RemueveIndice(int index)
    {
        GameManager.instance.jugador.accionesActuales += listaHabilidadesALanzar.ElementAt(index).Key.coste;
        
        listaHabilidadesALanzar.Remove(listaHabilidadesALanzar[index]);

        ActualizaLista();

        GameManager.instance.informacionInterfaz.ActualizaPuntos();
    }

    public void OrdenaLista()
    {
        var repeatedSpeeds = listaHabilidadesALanzar.GroupBy(x => x.Key.velocidad).Where(g => g.Count() > 1).Select(y => new { Value = y.Key, Count = y.Count() });
        
        foreach (var speed in repeatedSpeeds)
        {
            int startIndex = listaHabilidadesALanzar.IndexOf(listaHabilidadesALanzar.First(x => x.Key.velocidad == speed.Value));
            int repetitionCount = speed.Count;
        
            listaHabilidadesALanzar.ShuffleRange(startIndex, repetitionCount);
        }

        listaHabilidadesALanzar = listaHabilidadesALanzar.OrderByDescending(o => o.Key.velocidad).ToList();
    }
}
