using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAssigner : MonoBehaviour
{
    //public int playerIndex = 0;
    [SerializeField] List<GameObject> characters = new List<GameObject>();
    PlayerInputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        //playerIndex = Random.Range(0, characters.Count);
        //inputManager.playerPrefab = characters[playerIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void AssignCharactersToPlayers()
    {
        playerIndex++;
        inputManager.playerPrefab = characters[playerIndex];
    }*/

    public GameObject InstantiatePlayerAt(PlayerData playerData, Transform spawnPoint)
    {
        return Instantiate(inputManager.playerPrefab = characters[playerData.GetSelectedCharacterID()], spawnPoint.position, Quaternion.identity, spawnPoint);
    }
}
