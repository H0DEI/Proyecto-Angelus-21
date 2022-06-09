using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Nueva Habilidad")]
public class Habilidad : ScriptableObject, IComparable
{
    public string nombre;

    [TextArea(10,15)]
    public string descripcion;

    public int coste;
    public int velocidad;
    public int fuerza;
    public int penetracion;
    public int daño;

    public List<Accion> acciones = new List<Accion>();

    public Personaje personaje;

    public List<Personaje> objetivos = new List<Personaje>();

    public TipoSeleccion tipoSeleccion;

    public int cantidad = 2;

    public bool usosLimitados;

    public int numeroDeUsos;

    public void Usar()
    {
                    foreach (Accion accion in acciones)
            {
                switch (accion)
                {
                    case Accion.Disparo:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.punteria, fuerza, objetivo, daño);
                        }

                        break;

                    case Accion.GolpeMasFuerza:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + fuerza, objetivo, daño);
                        }

                        break;

                    case Accion.Golpe:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.habilidadCombate, personaje.fuerza, objetivo, daño);
                        }

                        break;

                    case Accion.DisparoPierdeSaludRestante:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.punteria, fuerza, objetivo, daño);
                        }

                        personaje.heridasActuales -= personaje.heridasActuales / 8;

                        break;

                    case Accion.DisparoDañoPorcentualVidaActual:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.punteria, fuerza, objetivo, objetivo.heridasActuales * 15 / 100);
                        }

                        break;

                    case Accion.MejoraFuerzaYAccionesMaximas:

                        personaje.fuerza += fuerza;

                        personaje.accionesMaximas += 1;

                        break;

                case Accion.ReduccionResistencia:

                    personaje.resistencia -= 1;

                    break;

                case Accion.GolpeUnObjetivoMasFuerzaMasDañoPorcentual:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + fuerza, objetivo, daño + objetivo.heridasActuales * 10 / 100);
                        }

                        break;

                    case Accion.GolpeYSiMataCuracion:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + fuerza, objetivo, daño);

                            if (objetivo.heridasActuales <= 0) Curar(personaje);
                        }

                        break;

                    case Accion.Curar:

                        foreach (Personaje objetivo in objetivos) Curar(objetivo);

                        break;

                    case Accion.CurarUnoMismo:

                        Curar(personaje);

                        break;

                    case Accion.DisparoDañoMasDañoAleatorio:

                        foreach (Personaje objetivo in objetivos)
                        {
                            RealizaTiradas(personaje.punteria, fuerza, objetivo, daño + Roll());
                        }

                        break;

                    case Accion.MejoraPunteria:

                        foreach (Personaje objetivo in objetivos)
                        {
                            objetivo.punteria -= personaje.habilidadEspecial / 2;
                        }

                        break;

                    case Accion.Disparo1d6:

                        foreach (Personaje objetivo in objetivos)
                        {
                            for (int i = 0; i < Roll(); i++) RealizaTiradas(personaje.punteria, fuerza, objetivo, daño);
                        }

                        break;

                    case Accion.Disparo1d3DañoMas1d3:

                        foreach (Personaje objetivo in objetivos)
                        {
                            for (int i = 0; i < Roll(1, 3); i++) RealizaTiradas(personaje.punteria, fuerza, objetivo, daño + Roll(1, 3));
                        }

                        break;

                case Accion.DisparoDañoMitadVidaActualObjetivo:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.punteria, fuerza, objetivo, objetivo.heridasActuales/2);
                    }

                    break;
                }
            }
        
    }

    public int CompareTo(object obj)
    {
        Habilidad hab = (Habilidad)obj;

        return velocidad > hab.velocidad ? 1 : 0;
    }

    private void RealizaTiradas(int punteria, int fuerza, Personaje objetivo, int daño)
    {
        if (HitRoll(punteria)) if (WoundRoll(fuerza, objetivo)) if (!SavingThrow(objetivo)) objetivo.heridasActuales -= daño;        
    }

    private bool HitRoll(int punteria)
    {
        int resultado = Roll();

        if (resultado >= personaje.punteria && resultado != 1 || resultado == 6) return true;
        else return false;
    }
    
    private bool WoundRoll(int fuerza, Personaje objetivo)
    {
        int resultado = Roll();
        int requisito;

        if (fuerza >= objetivo.resistencia * 2)
        {
            requisito = 2;
        }
        else if (fuerza > objetivo.resistencia)
        {
            requisito = 3;
        }
        else if (fuerza == objetivo.resistencia)
        {
            requisito = 4;
        }
        else
        {
            requisito = 5;
        }

        if (resultado >= requisito && resultado != 1 || resultado == 6) return true;
        else return false;
    }

    private bool SavingThrow(Personaje objetivo)
    {
        int resultado = Roll();

        if(personaje.salvacion+penetracion>personaje.salvacionInvulnerable && personaje.salvacionInvulnerable != 0)
        {
            if (resultado >= personaje.salvacionInvulnerable && resultado != 1) return true;
            else return false;
        }
        else
        {
            if (resultado - penetracion >= personaje.salvacion && resultado != 1) return true;
            else return false;
        }        
    }

    private int Roll()
    {
        return Random.Range(1, 7);
    }

    private int Roll(int n1, int n2)
    {
        return Random.Range(n1, n2+1);
    }

    private void Curar(Personaje objetivo)
    {
        if (objetivo.heridasActuales > 0 && objetivo.heridasActuales < objetivo.heridasMaximas)
        {
            objetivo.heridasActuales += personaje.habilidadEspecial + Roll();

            if (objetivo.heridasActuales > objetivo.heridasMaximas) objetivo.heridasActuales = objetivo.heridasMaximas;
        }
    }
}
