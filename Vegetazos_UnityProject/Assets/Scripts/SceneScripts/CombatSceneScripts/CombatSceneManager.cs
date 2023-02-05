using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSceneManager : MonoBehaviour
{
    public Transform[] spawnPoints;
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

    private void InstantiatePlayersFighters()
    {
        PlayerData[] playersData = GameManager.gameManagerInstance.playersData;
        for (int playerIndex = 0; playerIndex < playersData.Length; playerIndex++)
        {
            PlayerData currentPlayer = playersData[playerIndex];
            characterAssigner.InstantiatePlayerAt(currentPlayer, spawnPoints[playerIndex]);
        }
    }
}
