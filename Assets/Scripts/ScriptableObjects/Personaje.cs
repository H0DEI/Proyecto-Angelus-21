using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Nuevo Personaje")]
public class Personaje : ScriptableObject
{
    public string nombre;

    public int nivel;
    public int punteria;
    public int habilidadCombate;
    public int habilidadEspecial;
    public int fuerza;
    public int resistencia;
    public int heridasMaximas;
    public int heridasActuales;
    public int salvacion;
    public int salvacionInvulnerable;
    public int accionesMaximas;
    public int accionesActuales;

    public List<Estados> estados = new List<Estados>();

    public List<Habilidad> habilidades = new List<Habilidad>();
}
