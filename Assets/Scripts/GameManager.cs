using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using cakeslice;

public class GameManager : MonoBehaviour
{
    public SortedList<string, Escena> copiasEscenas = new SortedList<string, Escena>();

    public Escena escenaActual;

    public static GameManager instancia;

    public CargaEscena cargaEscena;

    public InformacionDescripciones informacionDescripciones;

    public InformacionInterfaz informacionInterfaz;

    public HabilidadesALanzar habilidadesALanzar;

    public XP XP;

    public CargaInterfazHabilidades cargaInterfazHabilidades;

    public InteractuarBotonHabilidad interactuarBotonHabilidad;

    public List<GameObject> listaBotonesInterfaz = new List<GameObject>();

    public List<Personaje> listaScriptPersonajesEscena = new List<Personaje>();

    public List<GameObject> listaObjetosPersonajesEscena = new List<GameObject>();

    public List<GameObject> listaPuertas = new List<GameObject>();

    public Personaje jugador;

    public Personaje tempJugador;

    public GameObject objetoJugador;

    public GameObject Mapa;

    public GameObject mapPlayerPosition;

    public bool habilidadSeleccionada;
    public bool mostrarIndicador;

    public Dictionary<TipoSeleccion, bool[]> tipoSelecciones;

    public GameObject btnHasMuerto;

    public GameObject panelAyuda;

    public Habilidad habilidadLevelUp;

    public bool puedeCambiarseHabilidad;

    //Escena 1 carga fea, corregir
    public Escena escena1;

    public Camera camara;

    //public GameObject indicadorRaton;

    private Transform interfaz;

    private void Awake()
    {
        instancia = this;

        DontDestroyOnLoad(instancia);

        interfaz = transform.Find("CanvasInterfaz");

        interfaz.gameObject.SetActive(false);

        CargaJugador();
    }

    private void Start()
    {
        if (tipoSelecciones == null)
        {
            tipoSelecciones = new Dictionary<TipoSeleccion, bool[]>();

            tipoSelecciones.Add(TipoSeleccion.SoloJugador, new bool[] { true, false, false, false, false });
            tipoSelecciones.Add(TipoSeleccion.SoloUnEnemigo, new bool[] { false, true, true, true, true });
            tipoSelecciones.Add(TipoSeleccion.VariosEnemigos, new bool[] { false, true, true, true, true });
            tipoSelecciones.Add(TipoSeleccion.TodosLosEnemigos, new bool[] { false, true, true, true, true });
            tipoSelecciones.Add(TipoSeleccion.CualquierPersonaje, new bool[] { true, true, true, true, true });
            tipoSelecciones.Add(TipoSeleccion.SinSeleccionar, new bool[] { true, true, true, true, true });
        }
    }

    public void CompruebaYCargaEscenas(Escena escena)
    {
        if (!copiasEscenas.ContainsKey(escena.idEscena))
        {
            copiasEscenas.Add(escena.idEscena, Instantiate(escena));
        }

        CargaEscenas((escenaActual = copiasEscenas[escena.idEscena]).idEscena);
    }

    private void CargaEscenas(string idEscena)
    {
        SceneManager.LoadScene(idEscena);
    }

    public void ActivaInterfaz()
    {
        interfaz.gameObject.SetActive(true);
    }

    public Habilidad BuscaHabilidadInstanciada(string nombreHabilidad)
    {
        foreach (Habilidad habi in jugador.habilidades)
        {
            if (habi.nombre == nombreHabilidad)
            {
                return Instantiate(habi);
            }
        }
        return null;
    }

    public void ActualizarListaHabilidades()
    {
        habilidadesALanzar.ActualizaLista();
    }

    public void ActualizarBotonesHabilidades()
    {
        for (int i = 0; i < listaBotonesInterfaz[0].transform.childCount; i++)
        {
            listaBotonesInterfaz[0].transform.GetChild(i).GetComponent<InteractuarBotonHabilidad>().habilidad = jugador.habilidades[i];

            listaBotonesInterfaz[0].transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = jugador.habilidades[i].nombre;
        }

        instancia.XP.ComprovarNivel();
    }

    public void ActivaBotonesInterfaz()
    {       
        foreach (GameObject boton in listaBotonesInterfaz)
        {
            IBoton[] scripts = boton.GetComponentsInChildren<IBoton>();
       
            foreach (IBoton script in scripts) script.Activar();
        }
    }

    public void ActivaBotonesInterfaz(int indice)
    {
        IBoton[] scripts = listaBotonesInterfaz[indice].GetComponentsInChildren<IBoton>();

        foreach (IBoton script in scripts) script.Activar();
    }

    public void DesactivaBotonesInterfaz()
    {
        foreach (GameObject boton in listaBotonesInterfaz)
        {
            IBoton[] scripts = boton.GetComponentsInChildren<IBoton>();
            
            foreach(IBoton script in scripts) script.Desactivar();
        }
    }

    public void ActivaPersonajes()
    {
        foreach (GameObject personaje in listaObjetosPersonajesEscena) personaje.GetComponentInChildren<InteractuarPersonajes>().Activar();
    }

    public void DesactivaPersonajes()
    {
        foreach (GameObject personaje in listaObjetosPersonajesEscena) personaje.GetComponentInChildren<InteractuarPersonajes>().Desactivar();
    }

    public void CargaPersonajesEscena()
    {
        Personaje scriptPersonajeInstanciado;

        string idEscena = SceneManager.GetActiveScene().name;

        for (int i = 0; i < copiasEscenas[idEscena].personajes.Count; i++)
        {
            scriptPersonajeInstanciado = Instantiate(copiasEscenas[idEscena].personajes[i]);

            listaObjetosPersonajesEscena[i].transform.Find("Canvas").GetComponent<RectTransform>().localPosition = new Vector2(listaObjetosPersonajesEscena[i].GetComponent<BoxCollider2D>().offset.x, listaObjetosPersonajesEscena[i].GetComponent<BoxCollider2D>().size.y + 0.2f);

            listaObjetosPersonajesEscena[i].GetComponent<InteractuarPersonajes>().personaje = scriptPersonajeInstanciado;

            listaObjetosPersonajesEscena[i].GetComponentInChildren<BarraDeVida>().personaje = scriptPersonajeInstanciado;

            listaObjetosPersonajesEscena[i].GetComponentInChildren<BarraDeVida>().barraAncho = listaObjetosPersonajesEscena[i].GetComponent<BoxCollider2D>().size.x;
        }

        listaObjetosPersonajesEscena.Insert(0, objetoJugador);

        ResetearObjetivosSeleccionables();
    }

    public void EscenaCompletada()
    {
        if (escenaActual.completada)
        {
            Destroy(cargaEscena.enemigos);
        }

        AbrirCerrarPuertas(true);

        escenaActual.completada = true;

        ResetearAttributosJugador();

        DesactivaBotonesInterfaz();



        //!!!!!!!!!!!!!aqui poner xp
        //!!!!!!!!!!!!!aqui poner xp
        //!!!!!!!!!!!!!aqui poner xp
        //!!!!!!!!!!!!!aqui poner xp
        //!!!!!!!!!!!!!aqui poner xp
    }

    public void MuestraObjetivosSeleccionables(Habilidad habilidad, bool soyJugador)
    {
        switch (habilidad.tipoSeleccion)
        {
            case TipoSeleccion.SoloJugador:
            case TipoSeleccion.SoloUnEnemigo:
            case TipoSeleccion.CualquierPersonaje:
            case TipoSeleccion.VariosEnemigos:

                for (int i = 0; i < listaObjetosPersonajesEscena.Count; i++)
                {
                    GameObject personaje = listaObjetosPersonajesEscena[i];

                    instancia.CambiaColorOutline(personaje.GetComponent<InteractuarPersonajes>().personaje, soyJugador, personaje);

                    personaje.GetComponent<Outline>().enabled = personaje.GetComponent<InteractuarPersonajes>().elegible = tipoSelecciones[habilidad.tipoSeleccion][i];
                }

                break;

            case TipoSeleccion.TodosLosEnemigos:

                foreach (GameObject personaje in listaObjetosPersonajesEscena.Skip(1))
                {
                    instancia.CambiaColorOutline(personaje.GetComponent<InteractuarPersonajes>().personaje, soyJugador, personaje);

                    personaje.GetComponent<Outline>().enabled = true;
                }

                break;
        }
    }

    public void SeleccionaTodosLosPersonajes(Habilidad habilidad, bool SinJugador)
    {
        for (int i = 0; i < listaObjetosPersonajesEscena.Count; i++)
        {
            habilidad.objetivos.Add(listaObjetosPersonajesEscena[i].GetComponent<InteractuarPersonajes>().personaje);
        }

        if (SinJugador)
        {
            habilidad.objetivos.RemoveAt(0);
        }
    }

    public void ResetearObjetivosSeleccionables()
    {
        foreach (GameObject personaje in listaObjetosPersonajesEscena)
        {
            personaje.GetComponent<Outline>().enabled = personaje.GetComponent<InteractuarPersonajes>().elegible = false;
        }
    }

    public void AbrirCerrarPuertas(bool puedePresionarse)
    {
        foreach (GameObject puerta in listaPuertas)
        {
            puerta.GetComponent<Outline>().enabled = puerta.GetComponent<InteractuarPuerta>().puedePresionarse = puedePresionarse;
        }
    }

    public List<GameObject> ToListEnemigos()
    {
        List<GameObject> listaEnemigos = listaObjetosPersonajesEscena.ToList();

        listaEnemigos.RemoveAt(0);

        return listaEnemigos;
    }

    public void CargaJugador()
    {
        jugador = Instantiate(jugador);
        
        objetoJugador.transform.Find("Canvas").GetComponent<RectTransform>().localPosition = new Vector2(objetoJugador.GetComponent<BoxCollider2D>().offset.x, objetoJugador.GetComponent<BoxCollider2D>().size.y + 0.05f);

        objetoJugador.GetComponent<InteractuarPersonajes>().personaje = jugador;

        objetoJugador.GetComponentInChildren<BarraDeVida>().personaje = jugador;

        objetoJugador.GetComponentInChildren<BarraDeVida>().barraAncho = objetoJugador.GetComponent<BoxCollider2D>().size.x;
    }

    public void CargaTurno()
    {
        List<Personaje> objetivosDisponibles = new List<Personaje>();

        List<GameObject> listaEnemigos = ToListEnemigos();

        List<Habilidad> lHabilidades = new List<Habilidad>();

        Habilidad habilidad;

        foreach (GameObject objetoEnemigo in listaEnemigos)
        {
            Personaje enemigo = objetoEnemigo.GetComponent<InteractuarPersonajes>().personaje;

            foreach (Habilidad habi in enemigo.habilidades)
            {
                lHabilidades.Add(habi);
            }

            for (int puntosAcciones = enemigo.accionesMaximas; puntosAcciones > 0; puntosAcciones -= habilidad.coste)
            {
                do {
                    habilidad = lHabilidades[Random.Range(0, enemigo.habilidades.Count())];

                    habilidad.velocidad += enemigo.agilidad;

                } while (habilidad.coste > puntosAcciones);
               
                switch (habilidad.tipoSeleccion)
                {
                    case TipoSeleccion.SoloJugador:

                        habilidad.objetivos.Add(instancia.jugador);

                        break;

                    case TipoSeleccion.SoloUnEnemigo:

                        habilidad.objetivos.Add(listaEnemigos[Random.Range(0, listaEnemigos.Count())].GetComponent<InteractuarPersonajes>().personaje);

                        break;

                    case TipoSeleccion.CualquierPersonaje:

                        habilidad.objetivos.Add(listaObjetosPersonajesEscena[Random.Range(0, listaObjetosPersonajesEscena.Count())].GetComponent<InteractuarPersonajes>().personaje);

                        break;

                    case TipoSeleccion.TodosLosEnemigos:

                        foreach (GameObject objetoPersonaje in listaEnemigos)
                        {
                            habilidad.objetivos.Add(objetoPersonaje.GetComponent<InteractuarPersonajes>().personaje);
                        }

                        break;

                    case TipoSeleccion.SinSeleccionar:

                        foreach (GameObject objetoPersonaje in listaObjetosPersonajesEscena)
                        {
                            habilidad.objetivos.Add(objetoPersonaje.GetComponent<InteractuarPersonajes>().personaje);
                        }

                        break;
                }

                habilidad.personaje = enemigo;
                
                habilidadesALanzar.listaHabilidadesALanzar.Add(new KeyValuePair<Habilidad, bool>(Instantiate(habilidad), false));
              
                habilidad.objetivos.Clear();
            }
        }

        lHabilidades.Clear();

        ActualizarListaHabilidades();
    }

    public void CambiaColorOutline(Personaje objetivo, bool soyJugador, GameObject personaje)
    {
        if (objetivo == jugador && soyJugador || objetivo != jugador && !soyJugador) personaje.GetComponent<Outline>().color = 1;
        else personaje.GetComponent<Outline>().color = 0;
    }

    public void ResetearAttributosJugador()
    {
        jugador.punteria = tempJugador.punteria;
        jugador.habilidadCombate = tempJugador.habilidadCombate;
        jugador.habilidadEspecial = tempJugador.habilidadEspecial;
        jugador.fuerza = tempJugador.fuerza;
        jugador.resistencia = tempJugador.resistencia;
        jugador.salvacion = tempJugador.salvacion;
        jugador.accionesActuales = tempJugador.accionesActuales;
        jugador.accionesMaximas = tempJugador.accionesMaximas;

        GameObject panelHabilidades = listaBotonesInterfaz.ElementAt(0);

        foreach(InteractuarBotonHabilidad scr in panelHabilidades.GetComponentsInChildren<InteractuarBotonHabilidad>())
        {

            //probar con jugador temporal
            scr.ResetearHabilidad();
        }
    }

    public void Reiniciar()
    {
        copiasEscenas.Clear();

        objetoJugador.SetActive(true);

        btnHasMuerto.SetActive(false);

        jugador.heridasActuales = jugador.heridasMaximas;

        CompruebaYCargaEscenas(escena1);

        ActivaBotonesInterfaz();
    }

    public void Salir()
    {
        Application.Quit();
    }
}
