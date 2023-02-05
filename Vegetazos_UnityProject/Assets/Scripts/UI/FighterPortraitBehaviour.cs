using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterPortraitBehaviour : MonoBehaviour
{
    public Image fighterPortrait;
    public TextMeshProUGUI fighterName;

    public void SetFighterPortrait(Sprite characterSprite, string playerName)
    {
        fighterPortrait.sprite = characterSprite;
        fighterName.text = "";
        fighterName.text = playerName;
    }
}
