using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class XP : MonoBehaviour
{
    public int xp;

    public List<Habilidad> TierB; 
    public List<Habilidad> TierA; 
    public List<Habilidad> TierS; 
    public List<Habilidad> Upgrades;

    public Habilidad[] TodasHabilidades;

    public GameObject[] descripciones;

    public GameObject InterfazJugable;
    public GameObject LevelUp;

    public List<GameObject> BtnOpts;

    private bool upgrade;

    private int indice;

    private string nombreMejora;

    private GameManager instancia;

    private Personaje jugador;

    private List<Habilidad> Temp = new List<Habilidad>();
    private Habilidad[] TempMejoras;

    private Habilidad habilidad;

    private static readonly System.Random random = new System.Random();

    private void Awake()
    {
        instancia = GameManager.instancia;

        instancia.XP = this;

        jugador = instancia.jugador;

        TodasHabilidades = Resources.LoadAll<Habilidad>("");

        foreach(Habilidad habilidad in TodasHabilidades)
        {
            switch (habilidad.tier)
            {
                case TierHabilidad.TierB:

                    TierB.Add(habilidad);

                break;

                case TierHabilidad.TierA:

                    TierA.Add(habilidad);

                break;

                case TierHabilidad.TierS:

                    TierS.Add(habilidad);

                    break;

                case TierHabilidad.Upgrade:

                    Upgrades.Add(habilidad);

                break;
            }
        }

        TempMejoras = new Habilidad[Upgrades.Count];

        gameObject.SetActive(false);
    }

    public void ExperienciaEscena()
    {
        xp = instancia.escenaActual.xp;

        foreach(GameObject enemigo in instancia.ToListEnemigos())
        {
            xp += enemigo.GetComponent<InteractuarPersonajes>().personaje.nivel;
        }
    }
    public void ComprovarNivel()
    {
        jugador.experienciaActual -= jugador.requisitoNivel;

        jugador.requisitoNivel += 2;

        SubidaNivel();
    }


    public void SubidaNivel()
    {
        instancia.informacionDescripciones.bloqueoDescripcion = true;

        InterfazJugable.SetActive(false);
        LevelUp.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            //int rnd = random.Next(101);

            int rnd = 7;

            if (rnd > 94)
            {
                Temp = TierS;
            }
            else if(rnd > 69)
            {
                Temp = TierA;
            }
            else if(rnd > 34)
            {
                Temp = TierB;
            }
            else if (rnd > -1)
            {
                Upgrades.CopyTo(TempMejoras);

                upgrade = true;
            }

            if (upgrade)
            {
                foreach (Habilidad habi in jugador.habilidades)
                {
                    indice = (habi.nombre.LastIndexOf("+") + 1);

                    if (indice == 0) nombreMejora = habi.nombre + " +1";
                    else nombreMejora = habi.nombre.Substring(0, indice) + (int.Parse(habi.nombre.Substring(indice)) + 1).ToString();

                    for (int j = 0; j < TempMejoras.Length; j++)
                    {
                        if (TempMejoras[j].nombre == nombreMejora)
                        {
                            Temp.Add(TempMejoras[j]);
                        }
                    }
                }
            }
            else
            {
                foreach (Habilidad habi in jugador.habilidades)
                {
                    for (int j = 0; j < Temp.Count; j++)
                    {
                        if (Temp[j].nombre == habi.nombre)
                        {
                            Temp.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
            
            habilidad = Instantiate(Temp[random.Next(Temp.Count)]);

            descripciones[i].GetComponentInChildren<InformacionDescripciones>().MuestraInformacionHabilidad(habilidad);
                            
            BtnOpts[i].GetComponentInChildren<InteractuarLevelUp>().habilidad = habilidad;

            BtnOpts[i].GetComponentInChildren<InteractuarLevelUp>().esMejora = upgrade;

            if(TempMejoras != null) Array.Clear(TempMejoras, 0, TempMejoras.Length);

            Temp.Clear();

            upgrade = false;
        }
    }
}
