using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotBehaviour : MonoBehaviour
{
    [SerializeField] private string playerName;
    public Image characterPortrait;

    public void SetCharacterPortrait(Sprite portraitSprite)
    {
        Debug.Log("Character Setted");
        characterPortrait.sprite = portraitSprite;
    }

    public void SetPlayerName(string nameToSet)
    {
        playerName = nameToSet;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void ErasePlayer()
    {
        characterPortrait.gameObject.SetActive(false);
        playerName = null;
        gameObject.GetComponentInChildren<TMP_InputField>().text = "";
    }
}
