using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;
using TMPro;
using System.Threading;
using System.Linq;

public class InteractuarBotonListo : MonoBehaviour, IBoton
{
    private bool puedePresionarse;

    private TextMeshProUGUI texto;

    private Color colorDefault;

    private GameManager instancia;

    private List<Personaje> muertos = new List<Personaje>();

    private void Start()
    {
        instancia = GameManager.instance;

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
            //usar audio . duration para duracion de wait
            yield return new WaitForSeconds(habilidad.Key.sonido != null ? habilidad.Key.sonido.length -0.1f : 0.3f);

            if(!muertos.Contains(habilidad.Key.personaje)) habilidad.Key.Usar();          

            for (int i = 0; i < instancia.listaObjetosPersonajesEscena.Count; i++)
            {
                GameObject personajeEnEscena = instancia.listaObjetosPersonajesEscena[i];

                if (personajeEnEscena.GetComponent<InteractuarPersonajes>().personaje.heridasActuales <= 0)
                {
                    muertos.Add(personajeEnEscena.GetComponent<InteractuarPersonajes>().personaje);

                    if (personajeEnEscena.GetComponent<InteractuarPersonajes>().personaje == instancia.jugador)
                    {
                        instancia.btnHasMuerto.SetActive(true);
                    }

                    personajeEnEscena.GetComponent<Animator>().SetTrigger("Muerto");

                    personajeEnEscena.SetActive(false);

                    instancia.listaObjetosPersonajesEscena.Remove(personajeEnEscena);
                }
            }
        }

        if (instancia.listaObjetosPersonajesEscena.Count == 1 &&
            instancia.listaObjetosPersonajesEscena[0].GetComponent<InteractuarPersonajes>().personaje == instancia.jugador)
        {
            instancia.EscenaCompletada();

            instancia.jugador.experienciaActual += instancia.XP.xp;
            
            instancia.XP.ComprovarNivel();
        }

        muertos.Clear();

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
