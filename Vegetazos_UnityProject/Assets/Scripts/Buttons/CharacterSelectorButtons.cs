using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorButtons : ButtonsBehaviours
{

    public GameLobby gameLobby;
    public void PlayerSelectorButtonPressed(int playerID)
    {
        gameLobby.currentPlayerID = playerID;
    }

    public void CharacterSelectedButtonPressed(int characterID)
    {
        gameLobby.SetCharacterToCharacterSlot(characterID);
    }

    public void ErasePlayerButtonPressed(int playerID)
    {
        gameLobby.DeletePlayerData(playerID);
    }
}
