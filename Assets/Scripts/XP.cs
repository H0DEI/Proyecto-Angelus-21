using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class XP : MonoBehaviour
{
    public int xp;

    public List<Habilidad> TierB; //35
    public List<Habilidad> TierA; //25
    public List<Habilidad> TierS; //5
    public List<Habilidad> Upgrades; //35

    public GameObject[] descripciones;

    public GameObject InterfazJugable;
    public GameObject LevelUp;

    private GameManager instancia;

    private Personaje jugador;

    private List<Habilidad> Temp;

    private Habilidad habilidad;

    private static readonly System.Random random = new System.Random();

    private void Start()
    {
        instancia = GameManager.instancia;

        instancia.XP = this;

        jugador = instancia.jugador;
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
        InterfazJugable.SetActive(false);
        LevelUp.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            int rnd = random.Next(101);
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
                Temp = Upgrades;
            }

            habilidad = Instantiate(Temp[random.Next(Temp.Count)]);

            descripciones[i].GetComponent<InformacionDescripciones>().MuestraInformacionHabilidad(habilidad);

            descripciones[i].GetComponent<InteractuarLevelUp>().habilidad = habilidad;
        }
    }
}
