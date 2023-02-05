using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour
{

    public int currentPlayerID;

    [Header("Game Lobby")]
    public Sprite[] characterPortraits;
    public GameObject[] characterSlots;
    public List<PlayerData> playersDataList;

    [Header("Character By Player Portraits")]
    public CharacterSprites playerPortraits;



    private void Awake()
    {
        foreach(GameObject slot in characterSlots)
        {
            Image characterImage = slot.GetComponent<CharacterSlotBehaviour>().characterPortrait;
            characterImage.gameObject.SetActive(false);
        }
    }

    public void SetFightersData()
    {
        playersDataList.Clear();
        for (int slotID = 0; slotID < characterSlots.Length; slotID++)
        {
            CharacterSlotBehaviour currentSlot = characterSlots[slotID].GetComponent<CharacterSlotBehaviour>();
            currentSlot.CreatePlayer(slotID);
            if (currentSlot.characterSlotData.IsFighterComplete())
            {
                PlayerData slotData = currentSlot.characterSlotData;
                playersDataList.Add(slotData);
            }
        }
        if(GameManager.gameManagerInstance != null)
        {
            GameManager.gameManagerInstance.SetPlayersData(playersDataList);
        }
    }
    public void DeletePlayerData(int playerID)
    {
        characterSlots[playerID].GetComponent<CharacterSlotBehaviour>().ErasePlayer();
    }

    public void SetCharacterToCharacterSlot(int characterID)
    {
        CharacterSlotBehaviour currentSlot = characterSlots[currentPlayerID].GetComponent<CharacterSlotBehaviour>();
        currentSlot.selectedCharacterID = characterID;
        if (!currentSlot.characterPortrait.gameObject.activeInHierarchy)
        {
            currentSlot.characterPortrait.gameObject.SetActive(true);
        }
        switch (characterID)
        {
            case 0:
                currentSlot.SetCharacterPortrait(playerPortraits.nopalPlayerSprites[currentPlayerID]);
                break;
            case 1:
                currentSlot.SetCharacterPortrait(playerPortraits.rabanoPlayerSprites[currentPlayerID]);
                break;
            case 2:
                currentSlot.SetCharacterPortrait(playerPortraits.zanahoriaPlayerSprites[currentPlayerID]);
                break;
            case 3:
                currentSlot.SetCharacterPortrait(playerPortraits.calabazaPlayerSprites[currentPlayerID]);
                break;
            case 4:
                currentSlot.SetCharacterPortrait(playerPortraits.tomatePlayerSprites[currentPlayerID]);
                break;
        }
    }
}

[System.Serializable]
public class CharacterSprites
{
    public Sprite[] nopalPlayerSprites;
    public Sprite[] rabanoPlayerSprites;
    public Sprite[] zanahoriaPlayerSprites;
    public Sprite[] calabazaPlayerSprites;
    public Sprite[] tomatePlayerSprites;

}