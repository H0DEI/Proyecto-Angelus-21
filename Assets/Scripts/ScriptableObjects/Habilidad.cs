using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using SHG.AnimatorCoder;

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
    public int da�o;

    public List<Accion> acciones = new List<Accion>();

    public Personaje personaje;

    public List<Personaje> objetivos = new List<Personaje>();

    public TipoSeleccion tipoSeleccion;

    public int cantidad = 2;

    public bool usosLimitados;

    public bool melee;

    public int numeroDeUsos;

    public TierHabilidad tier;

    public AudioClip sonido;

    private void Play(string id, AnimationData animationData, int layer = 0)
    {
        GameManager.instance.animationManager.PlayAnimation(id, animationData, layer);
    }

    private void PlayCanvas(string id, AnimationData animationData, int layer = 0)
    {
        GameManager.instance.animationManager.PlayCanvas(id, animationData, layer);
    }

    public void Usar()
    {
        if(sonido != null) GameManager.instance.soundEffect.PlayOneShot(sonido);

        foreach (Accion accion in acciones) 
        {
            switch (accion) 
            {
                case Accion.Disparo:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.punteria, fuerza, objetivo, da�o);

                        Play(personaje.gameObject.GetInstanceID().ToString(), new(Animations.SHOOT1, true, new(), 0.2f));
                    }

                    break;

                case Accion.GolpeMasFuerza:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + fuerza, objetivo, da�o);
                    }

                    break;

                case Accion.GolpeMasFuerzaMasHabilidad:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + personaje.habilidadEspecial + fuerza, objetivo, da�o);
                    }

                    break;

                case Accion.Golpe:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.habilidadCombate, personaje.fuerza, objetivo, da�o);
                    }

                    break;

                case Accion.DisparoPierdeSaludRestante:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.punteria, fuerza, objetivo, da�o);
                    }

                    personaje.heridasActuales -= personaje.heridasActuales / 8;

                    break;

                case Accion.DisparoDa�oPorcentualVidaActual:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.punteria, fuerza, objetivo, objetivo.heridasActuales * 15 / 100);
                    }

                    break;

                case Accion.MejoraFuerzaYAccionesMaximas:

                    personaje.fuerza += fuerza;

                    personaje.accionesMaximas += 1;

                    //Anima(personaje, "Mejora");

                    break;

                case Accion.MejoraAgilidad:

                    personaje.agilidad += fuerza;

                    //Anima(personaje, "Mejora");

                    break;

                case Accion.ReduccionResistencia:

                personaje.resistencia -= 1;

                //Anima(personaje, "Desmejora");

                    break;

                case Accion.GolpeUnObjetivoMasFuerzaMasDa�oPorcentual:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + fuerza, objetivo, da�o + objetivo.heridasActuales * 10 / 100);
                    }

                    break;

                case Accion.GolpeYSiMataCuracion:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.habilidadCombate, personaje.fuerza + fuerza, objetivo, da�o);

                        if (objetivo.heridasActuales <= 0) Curar(personaje);
                    }

                    break;

                case Accion.Curar:

                    foreach (Personaje objetivo in objetivos) Curar(objetivo);

                    break;

                case Accion.CurarUnoMismo:

                    Curar(personaje);

                    break;

                case Accion.DisparoDa�oMasDa�oAleatorio:

                    foreach (Personaje objetivo in objetivos)
                    {
                        RealizaTiradas(personaje.punteria, fuerza, objetivo, da�o + Roll(6));
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
                        for (int i = 0; i < Roll(6); i++) RealizaTiradas(personaje.punteria, fuerza, objetivo, da�o);
                    }

                    break;

                case Accion.Disparo1d3Da�oMas1d3:

                    foreach (Personaje objetivo in objetivos)
                    {
                        for (int i = 0; i < Roll(1, 3); i++) RealizaTiradas(personaje.punteria, fuerza, objetivo, da�o + Roll(1, 3));
                    }

                    break;

                case Accion.DisparoDa�oMitadVidaActualObjetivo:

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

    private void RealizaTiradas(int punteria, int fuerza, Personaje objetivo, int da�o)
    {
        if (HitRoll(punteria - objetivo.agilidad))
        {
            if (WoundRoll(fuerza, objetivo))
            {
                if (!SavingThrow(objetivo))
                {
                    objetivo.heridasActuales -= da�o;

                    Anima(objetivo, da�o.ToString(), Color.red);
                }
                else
                {
                    Anima(objetivo, "Saved", Color.yellow);
                }
            }
            else
            {
                Anima(objetivo, "Resisted", Color.cyan);
            }
        }
        else
        {
            Anima(objetivo, "Miss", Color.grey);
        }
    }

    private bool HitRoll(int punteria)
    {
        int resultado = Roll(punteria);

        if (resultado > 5 && resultado != 1 || resultado == punteria) return true;
        else return false;
    }
    
    private bool WoundRoll(int fuerza, Personaje objetivo)
    {
        int resultado = Roll(6);
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
        else if (fuerza < objetivo.resistencia && fuerza > objetivo.resistencia / 2)
        {
            requisito = 5;
        }
        else
        {
            requisito = 6;
        }

        if (resultado >= requisito && resultado != 1) return true;
        else return false;
    }

    private bool SavingThrow(Personaje objetivo)
    {
        int resultado;

        if (objetivo.salvacion - penetracion >= objetivo.salvacionInvulnerable) resultado = Roll(objetivo.salvacion - penetracion);
        else resultado = Roll(objetivo.salvacionInvulnerable);

        if (resultado > 5 && resultado != 1) return true;
        else return false;
    }

    private int Roll(int maxRange)
    {
        return Random.Range(1, maxRange + 1);
    }

    private int Roll(int n1, int n2)
    {
        return Random.Range(n1, n2 + 1);
    }

    private void Curar(Personaje objetivo)
    {
        int heal;

        if (objetivo.heridasActuales > 0 && objetivo.heridasActuales < objetivo.heridasMaximas)
        {
            heal = Roll(personaje.habilidadEspecial);

            objetivo.heridasActuales += heal;

            if (objetivo.heridasActuales > objetivo.heridasMaximas) objetivo.heridasActuales = objetivo.heridasMaximas;

            AnimaValue(objetivo, "Curar", heal.ToString());
        }
    }

    private void Anima(Personaje objetivo, String animacion, Color color)
    {
        GameManager.instance.textManager.ShowFloatingText(objetivo.gameObject, animacion, color);

        //objetivo.gameObject.GetComponent<Animator>().SetTrigger(animacion);

        //PlayCanvas(objetivo.gameObject.GetInstanceID().ToString(), new(Animations.MISS, true, new(), 0.2f));
    }

    private void AnimaValue(Personaje objetivo, String text, String value)
    {
        //objetivo.gameObject.transform.Find("Canvas").transform.Find("wounded").GetComponent<TextMeshProUGUI>().text = value;
        //!!!!!!!!!!!!!!!!!!!!



        //objetivo.gameObject.GetComponent<Animator>().SetTrigger(text);
    }
}
