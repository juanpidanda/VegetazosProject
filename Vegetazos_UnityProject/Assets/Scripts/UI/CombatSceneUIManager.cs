using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSceneUIManager : MonoBehaviour
{
    public CharacterPortraitsSprites playerPortraitsSprites;

    public GameObject[] fighterPortraits;
    private void Awake()
    {
        foreach(GameObject fighterPortrait in fighterPortraits)
        {
            fighterPortrait.SetActive(false);
        }
    }
    private void Start()
    {
        if(GameManager.gameManagerInstance != null)
        {
            SetPlayerFighterPortraits(GameManager.gameManagerInstance.playersData);
        }
    }
    public void SetPlayerFighterPortraits(PlayerData[] playersData)
    {

        for (int playerIndex = 0; playerIndex < playersData.Length; playerIndex++)
        {
            fighterPortraits[playerIndex].SetActive(true);
            FighterPortraitBehaviour portraitBehaviour = fighterPortraits[playerIndex].GetComponent<FighterPortraitBehaviour>();

            //Get Data
            int characterID = playersData[playerIndex].GetSelectedCharacterID();
            string playerName = playersData[playerIndex].GetPlayerName();
            Sprite characterPortraitSprite = GetCharacterSprite(characterID, playerIndex);
            
            /* POR SI DEJA DE FUNCIONAR LA FUNCION GETCHARACTERSPRITE
              Sprite characterPortraitSprite = null;

            switch (characterID)
            {
                case 0:
                    characterPortraitSprite = playerPortraitsSprites.nopalPlayerPortraits[playerIndex];
                    break;
                case 1:
                    characterPortraitSprite = playerPortraitsSprites.rabanoPlayerPortraits[playerIndex];
                    break;
                case 2:
                    characterPortraitSprite = playerPortraitsSprites.zanahoriaPlayerPortraits[playerIndex];
                    break;
                case 3:
                    characterPortraitSprite = playerPortraitsSprites.calabazaPlayerPortraits[playerIndex];
                    break;
                case 4:
                    characterPortraitSprite = playerPortraitsSprites.tomatePlayerPortraits[playerIndex];
                    break;
            }
            */

            if(characterPortraitSprite != null)
            {
                portraitBehaviour.SetFighterPortrait(characterPortraitSprite, playerName);

            }
        }
    }

    Sprite GetCharacterSprite(int characterID, int playerID)
    {
        switch (characterID)
        {
            case 0:
                return playerPortraitsSprites.nopalPlayerPortraits[playerID];
                break;
            case 1:
                return playerPortraitsSprites.rabanoPlayerPortraits[playerID];
                break;
            case 2:
                return playerPortraitsSprites.zanahoriaPlayerPortraits[playerID];
                break;
            case 3:
                return playerPortraitsSprites.calabazaPlayerPortraits[playerID];
                break;
            case 4:
                return playerPortraitsSprites.tomatePlayerPortraits[playerID];
                break;

        }
        return null;
    }
}
