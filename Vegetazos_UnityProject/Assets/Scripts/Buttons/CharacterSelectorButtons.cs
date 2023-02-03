using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorButtons : ButtonsBehaviours
{

    public GameLobby gameLobby;
    public void PlayerSelectorButtonPressed(int playerID)
    {
        gameLobby.currentPlayerId = playerID;
    }

    public void CharacterSelectedButtonPressed(int characterID)
    {
        gameLobby.SetCharacterToCharacterSlot(characterID);
    }
}
