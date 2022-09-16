using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public GameObject nameTextBox;

    public List<CharacterColor> characterColors;

    private TextMeshProUGUI name;

    private Image background;

    private GameManagerAbril instance;

    private Transform character;

    private void Start()
    {
        name = nameTextBox.GetComponent<TextMeshProUGUI>();

        background = GetComponent<Image>();
    }

    private void Update()
    {
        foreach (CharacterColor c in characterColors)
        {
            if (name.text == c.name)
            {
                background.color = c.color;
            }
        }
    }

    public void SpriteChange()
    {
        if (instance == null) instance = GameManagerAbril.instance;

        foreach (CharacterColor characterColor in characterColors)
        {
            character = instance.characters.transform.Find(characterColor.name);

            if (character != null)
            {
                if (GameManagerAbril.instance.name.GetComponent<TextMeshProUGUI>().text == characterColor.name) {
                GameObject go = GameManagerAbril.instance.Icon;
                go.GetComponent<Image>().sprite = characterColor.sprite;
                }
            }
        }

        character = null;
    }
}
