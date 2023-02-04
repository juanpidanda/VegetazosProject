using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{

    public int currentPlayerID;

    public Sprite[] characterPortraits;
    public GameObject[] characterSlots;


    [Header("Character By Player Portraits")]
    public Sprite[] nopalPlayerPortraits;
    public Sprite[] rabanoPlayerPortraits;
    public Sprite[] zanahoriaPlayerPortraits;
    public Sprite[] calabazaPlayerPortraits;
    public Sprite[] tomatePlayerPortraits;

    private void Awake()
    {
        foreach(GameObject slot in characterSlots)
        {
            Image characterImage = slot.GetComponent<CharacterSlotBehaviour>().characterPortrait;
            characterImage.gameObject.SetActive(false);
        }
    }
    public void DeletePlayerData(int playerID)
    {
        characterSlots[playerID].GetComponent<CharacterSlotBehaviour>().ErasePlayer();
    }

    public void SetCharacterToCharacterSlot(int characterID)
    {
        CharacterSlotBehaviour currentSlot = characterSlots[currentPlayerID].GetComponent<CharacterSlotBehaviour>();
        if (!currentSlot.characterPortrait.gameObject.activeInHierarchy)
        {
            currentSlot.characterPortrait.gameObject.SetActive(true);
        }
        switch (characterID)
        {
            case 0:
                currentSlot.SetCharacterPortrait(nopalPlayerPortraits[currentPlayerID]);
                break;
            case 1:
                currentSlot.SetCharacterPortrait(rabanoPlayerPortraits[currentPlayerID]);
                break;
            case 2:
                currentSlot.SetCharacterPortrait(zanahoriaPlayerPortraits[currentPlayerID]);
                break;
            case 3:
                currentSlot.SetCharacterPortrait(calabazaPlayerPortraits[currentPlayerID]);
                break;
            case 4:
                currentSlot.SetCharacterPortrait(tomatePlayerPortraits[currentPlayerID]);
                break;
        }
    }
}
