using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlotBehaviour : MonoBehaviour
{
    public int selectedCharacterID;
    public Image characterPortrait;

    public PlayerData characterSlotData;
    private string slotPlayerName;
    private void Awake()
    {
        ErasePlayer();
        characterSlotData.EraseData();
    }
    public void SetCharacterPortrait(Sprite portraitSprite)
    {
        characterPortrait.sprite = portraitSprite;
    }

    public void SetPlayerName(string nameToSet)
    {
        slotPlayerName = nameToSet;
    }

    public string GetPlayerName()
    {
        return slotPlayerName;
    }

    public void ErasePlayer()
    {
        selectedCharacterID = -1;
        characterPortrait.gameObject.SetActive(false);
        slotPlayerName = null;
        gameObject.GetComponentInChildren<TMP_InputField>().text = "";
    }
    public void CreatePlayer(int playerID)
    {
        if(slotPlayerName == null)
        {
            characterSlotData.SetPlayerFighter("Player " + (playerID +1).ToString(), playerID, selectedCharacterID);
        }
        else
        {
            characterSlotData.SetPlayerFighter(slotPlayerName, playerID, selectedCharacterID);
        }
    }
}

[System.Serializable]
public class PlayerData
{
    [SerializeField]
    private string playerName;
    [SerializeField] 
    private int playerID;
    [SerializeField] 
    private int characterID;

    #region DATA MANAGEMENT FUNCTIONS
    public void EraseData()
    {
        playerName = "";
        playerID = -1;
        characterID = -1;
    }
    public void SetPlayerFighter(string nameToSet, int iDToSet, int characterIDToSet)
    {
        playerName = nameToSet;
        playerID = iDToSet;
        characterID = characterIDToSet;
    }
    #endregion

    #region GET DATA FUNCTIONS
    public string GetPlayerName()
    {
        return playerName;
    }
    public int GetPlayerID()
    {
        return playerID;
    }
    public int GetSelectedCharacterID()
    {
        return characterID;
    }
    #endregion 

    public bool IsFighterComplete()
    {
        if(playerName != null && playerID >= 0 && playerID < 4 && characterID >= 0)
        {
            //Debug.Log(playerName + " with ID: " + playerID.ToString() + ", playing as " + characterID + " ID character");
            return true;
        }
        else
        {
            return false;
        }
    }
}