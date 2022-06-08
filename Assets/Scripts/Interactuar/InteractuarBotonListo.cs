using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;
using TMPro;
using System.Threading;

public class InteractuarBotonListo : MonoBehaviour, IBoton
{
    private bool puedePresionarse;

    private TextMeshProUGUI texto;

    private Color colorDefault;

    private GameManager instancia;

    private void Start()
    {
        instancia = GameManager.instancia;

        puedePresionarse = true;

        texto = GetComponentInChildren<TextMeshProUGUI>();

        colorDefault = texto.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        texto.fontStyle = FontStyles.Normal;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (puedePresionarse)
        {
            instancia.habilidadesALanzar.OrdenaLista();

            texto.color = Color.white;

            instancia.DesactivaBotonesInterfaz();

            StartCoroutine(EjecutarTurno());
        }
    }

    private IEnumerator EjecutarTurno()
    {
        foreach (KeyValuePair<Habilidad, bool> habilidad in instancia.habilidadesALanzar.listaHabilidadesALanzar)
        {
            yield return new WaitForSeconds(0.3f);

            habilidad.Key.Usar();

            for (int i = 0; i < instancia.listaObjetosPersonajesEscena.Count; i++)
            {
                GameObject personajeEnEscena = instancia.listaObjetosPersonajesEscena[i];

                personajeEnEscena.GetComponentInChildren<BarraDeVida>().ActualizaBarraDeVida();
            }
        }

        for (int i = 0; i < instancia.listaObjetosPersonajesEscena.Count; i++)
        {
            GameObject personajeEnEscena = instancia.listaObjetosPersonajesEscena[i];

            if (personajeEnEscena.GetComponent<InteractuarPersonajes>().personaje.heridasActuales <= 0)
            {
                if(personajeEnEscena.GetComponent<InteractuarPersonajes>().personaje == instancia.jugador)
                {
                    instancia.btnHasMuerto.SetActive(true);
                }

                personajeEnEscena.SetActive(false);

                instancia.listaObjetosPersonajesEscena.Remove(personajeEnEscena);
            }
        }

        if (instancia.listaObjetosPersonajesEscena.Count == 1 &&
            instancia.listaObjetosPersonajesEscena[0].GetComponent<InteractuarPersonajes>().personaje == instancia.jugador)
        {
            instancia.EscenaCompletada();
        }

        instancia.habilidadesALanzar.listaHabilidadesALanzar.Clear();

        instancia.ActualizarListaHabilidades();

        instancia.jugador.accionesActuales = instancia.jugador.accionesMaximas;

        instancia.informacionInterfaz.ActualizaPuntos();

        if(!instancia.btnHasMuerto.activeInHierarchy && !instancia.escenaActual.completada) instancia.ActivaBotonesInterfaz();

        instancia.CargaTurno();
    }

    public void Desactivar()
    {
        texto.color = Color.gray;

        puedePresionarse = false;
    }

    public void Activar()
    {
        texto.color = colorDefault;

        puedePresionarse = true;
    }
}
