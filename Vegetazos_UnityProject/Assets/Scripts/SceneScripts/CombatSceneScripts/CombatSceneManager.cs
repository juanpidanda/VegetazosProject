using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSceneManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public List<GameObject> playersInGame;

    public CharacterSprites characterIdleSprites;
    private CombatSceneUIManager uIManager;
    private CharacterAssigner characterAssigner;
    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("Manager/UI").GetComponent<CombatSceneUIManager>();
        characterAssigner = GameObject.FindGameObjectWithTag("Manager/Players").GetComponent<CharacterAssigner>();
    }
    private void Start()
    {
        if (GameManager.gameManagerInstance != null)
        {
            InstantiatePlayersFighters();
            uIManager.SetPlayerFighterPortraits(GameManager.gameManagerInstance.playersData);
        }

    }
    public void EditPlayerPortrait(GameObject playerClone)
    {
        int playerID = 0;
        foreach(GameObject player in playersInGame)
        {
            if(playerClone == player)
            {
                uIManager.EditLifeInPortrait(playerID, playerClone.GetComponent<PlayerFightingSystem>().GetLifepoints());
                Debug.Log("Es este mero we");
            }
            playerID++;
        }
    }
    private void InstantiatePlayersFighters()
    {
        PlayerData[] playersData = GameManager.gameManagerInstance.playersData;
        for (int playerIndex = 0; playerIndex < playersData.Length; playerIndex++)
        {
            PlayerData currentPlayer = playersData[playerIndex];
            GameObject fighter = characterAssigner.InstantiatePlayerAt(currentPlayer, spawnPoints[playerIndex]);

            fighter.GetComponent<PlayerFightingSystem>().SetInitialPosition(spawnPoints[playerIndex]);
            playersInGame.Add(fighter);
            switch (currentPlayer.GetSelectedCharacterID())
            {
                case 0:
                    fighter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterIdleSprites.nopalPlayerSprites[currentPlayer.GetPlayerID()];
                    break;
                case 1:
                    fighter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterIdleSprites.rabanoPlayerSprites[currentPlayer.GetPlayerID()];
                    break;
                case 2:
                    fighter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterIdleSprites.zanahoriaPlayerSprites[currentPlayer.GetPlayerID()];
                    break;
                case 3:
                    fighter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterIdleSprites.calabazaPlayerSprites[currentPlayer.GetPlayerID()];
                    break;
                case 4:
                    fighter.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterIdleSprites.tomatePlayerSprites[currentPlayer.GetPlayerID()];
                    break;
            }
        }
    }
}
