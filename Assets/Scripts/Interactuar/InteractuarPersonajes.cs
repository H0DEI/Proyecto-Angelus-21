using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InteractuarPersonajes : MonoBehaviour
{
    public bool elegible;
    public bool vivo;

    public Personaje personaje;

    private RectTransform rectTransform;

    private SpriteRenderer spriteRenderer;

    private Color porDefecto;
    private Color rojo;

    private GameManager instancia;

    private void Start()
    {
        instancia = GameManager.instancia;
        vivo = true;

        ColorUtility.TryParseHtmlString("#FFFFFF", out porDefecto);
        ColorUtility.TryParseHtmlString("#E36D63", out rojo);

        spriteRenderer = GetComponent<SpriteRenderer>();

        rectTransform = GetComponent<RectTransform>();
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = rojo;

        instancia.informacionDescripciones.MuestraInformacionPersonaje(personaje);

        //if (instancia.mostrarIndicador)
        //{
        //    instancia.indicadorRaton.GetComponent<IndicadorRaton>().PersonajeOffset = rectTransform;
        //
        //    instancia.indicadorRaton.SetActive(true);
        //
        //    instancia.indicadorRaton.GetComponent<TextMeshProUGUI>().text = instancia.interactuarBotonHabilidad.cantidad.ToString();
        //}
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = porDefecto;

        instancia.informacionDescripciones.LimpiaInformacion();

        //if (instancia.mostrarIndicador) instancia.indicadorRaton.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (elegible && vivo) instancia.interactuarBotonHabilidad.ObjetivoSeleccionado(personaje);
    }
}
