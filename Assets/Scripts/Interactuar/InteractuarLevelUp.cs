using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InteractuarLevelUp : MonoBehaviour, IBoton
{
    public Habilidad habilidad;

    public GameObject InterfazJugable;
    public GameObject LevelUp;

    private TextMeshProUGUI texto;

    private GameManager instancia;

    private void Start()
    {
        instancia = GameManager.instancia;

        texto = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InterfazJugable.SetActive(true);

        instancia.DesactivaBotonesInterfaz();
        instancia.DesactivaPersonajes();
        instancia.ActivaBotonesInterfaz(2);

        instancia.habilidadLevelUp = habilidad;

        LevelUp.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        texto.fontStyle = FontStyles.Normal;
    }

    public void Activar()
    {
        throw new System.NotImplementedException();
    }

    public void Desactivar()
    {
        throw new System.NotImplementedException();
    }
}
