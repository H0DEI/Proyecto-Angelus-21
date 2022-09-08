using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public GameObject nameTextBox;

    public List<CharacterColor> characterColors;

    private TextMeshProUGUI name;

    private Image background;

    private void Start()
    {
        name = nameTextBox.GetComponent<TextMeshProUGUI>();

        background = GetComponent<Image>();
    }

    private void Update()
    {
        foreach(CharacterColor c in characterColors)
        {
            if (name.text == c.name)
            {
                background.color = c.color;
            }
        }
    }
}
