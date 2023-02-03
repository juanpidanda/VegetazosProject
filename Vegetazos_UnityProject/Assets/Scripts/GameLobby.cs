using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{

    public int currentPlayerId;

    public Sprite[] characterPortraits;
    public GameObject[] characterSlots;

    public void SetCharacterToCharacterSlot(int characterID)
    {
        characterSlots[currentPlayerId].GetComponent<CharacterSlotBehaviour>().SetCharacterPortrait(characterPortraits[characterID]);
    }
}
