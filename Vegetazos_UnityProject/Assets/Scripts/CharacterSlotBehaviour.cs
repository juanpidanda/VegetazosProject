using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotBehaviour : MonoBehaviour
{
    public Image characterPortrait;

    public void SetCharacterPortrait(Sprite portraitSprite)
    {
        Debug.Log("Character Setted");
        characterPortrait.sprite = portraitSprite;
    }
}
