using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Nueva Escena")]
public class Escena : ScriptableObject
{
    public string idEscena;

    public bool completada;

    public List<Personaje> personajes = new List<Personaje>();
}