using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IBoton : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    new void OnPointerEnter(PointerEventData eventData);
    new void OnPointerExit(PointerEventData eventData);
    new void OnPointerClick(PointerEventData eventData);

    public void Desactivar();

    public void Activar();
}
