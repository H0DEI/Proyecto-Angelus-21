using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadCharacters : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.characters = gameObject;
    }
}
