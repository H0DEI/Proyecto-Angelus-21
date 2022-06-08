using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image bar;

    public float fill;

    public Personaje personaje;

    public float barraAncho;

    private void Start()
    {
        RectTransform rt = GetComponentInParent<Canvas>().GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(barraAncho, 0.1755f);
    }

    public void ActualizaBarraDeVida()
    {
        bar.fillAmount = (float)personaje.heridasActuales / personaje.heridasMaximas;
    }
}
