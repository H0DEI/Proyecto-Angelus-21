using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractuarBotonMenu : MonoBehaviour, IBoton
{
    public Escena escenaCargar;

    public Sprite spriteHover;

    private bool puedePresionarse;

    private Sprite spritePorDefecto;
    private Image image;

    private void Start()
    {
        puedePresionarse = true;

        image = GetComponent<Image>();

        spritePorDefecto = image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = spriteHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = spritePorDefecto;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (puedePresionarse) {
            GameManager.instance.copiasEscenas.Clear();

            GameManager.instance.CompruebaYCargaEscenas(escenaCargar);
        }
    }

    public void Desactivar()
    {
        puedePresionarse = false;
    }

    public void Activar()
    {
        puedePresionarse = true;
    }
}
